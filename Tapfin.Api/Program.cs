using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using Tapfin.Api.Filters;
using Tapfin.Api.Helpers.AutoMapperProfiles;
using Tapfin.Api.Models;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Persistence.Repositories;
using Tapfin.Api.Services.Contracts;
using Tapfin.Api.Services.ServiceLogic;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ReturnResultFilter());
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddDbContext<TapfinDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("TFConnectionString")));

/*---------- Add Identity ------------*/
builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<TapfinDbContext>();

/*---------- JWT Settings----------*/
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("Key").Value))
    };
});


/*---------- Add AutoMapper Service ----------*/
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

/*------ Persitence Interface Dependency Injection --------*/
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

/*------ Services Dependency Injection --------*/
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<IAccountsServices, AccountsServices>();
builder.Services.AddScoped<ITokenServices, TokenServices>();
builder.Services.AddScoped<IRegionServices, RegionServices>();
builder.Services.AddScoped<ICountryServices, CountryServices>();
builder.Services.AddScoped<IClientServices, ClientServices>();
builder.Services.AddScoped<IClientRevenueServices, ClientRevenueServices>();
builder.Services.AddScoped<IVendorServices, VendorServices>();
builder.Services.AddScoped<IJobServices, JobServices>();
builder.Services.AddScoped<IJobOrderServices, JobOrderServices>();
builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();
builder.Services.AddScoped<IServiceTypesServices, ServiceTypeServices>();
builder.Services.AddScoped<IAllocatedAtClientServices, AllocatedAtClientServices>();
builder.Services.AddScoped<IVendorBillingServices, VendorBillingServices>();
builder.Services.AddScoped<IWorkerServices, WorkerServices>();
builder.Services.AddScoped<IWorkerEquipmentCostServices, WorkerEquipmentCostServices>();
builder.Services.AddScoped<IWorkerEvaluationServices, WorkerEvaluationServices>();
builder.Services.AddScoped<IWorkerBackgroundEvaluationServices, WorkerBackgroundEvaluationServices>();
builder.Services.AddScoped<IWorkerExtensionServices, WorkerExtensionServices>();
builder.Services.AddScoped<IWorkerPayrollServices, WorkerPayrollServices>();
builder.Services.AddScoped<IWorkerBIServices, WorkerBIServices>();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(builder =>
{
    builder
          .WithOrigins("https://localhost:44358", "http://10.194.1.155:8073", "http://10.194.1.155:8074", "https://localhost:4200", "http://localhost:4200", "http://localhost")
          .SetIsOriginAllowedToAllowWildcardSubdomains()
          .AllowAnyHeader()
          .AllowCredentials()
          .WithMethods("GET", "PUT", "POST", "DELETE", "OPTIONS")
          .SetPreflightMaxAge(TimeSpan.FromSeconds(3600));

});

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.Use(async (context, next) =>
{
    try
    {
        await next(context);
    }
    catch (Exception ex)
    {
        JObject dTO = new JObject();
        dTO["statusCode"] = Convert.ToInt32(System.Net.HttpStatusCode.InternalServerError);
        dTO["isSuccess"] = false;
        dTO["returnMessage"] = ex.Message;

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(dTO.ToString());
    }
});

app.MapControllers();

app.Run();
