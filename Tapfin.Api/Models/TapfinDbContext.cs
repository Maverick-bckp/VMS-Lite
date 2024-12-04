using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Tapfin.Api.Configurations;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Models.ViewModels;

namespace Tapfin.Api.Models;

public partial class TapfinDbContext : IdentityDbContext<User, Role, string>
{

    public TapfinDbContext(DbContextOptions<TapfinDbContext> options)
        : base(options)
    {
    }

    //public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    //public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<User> AspNetUsers { get; set; }

    //public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }    

    //public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<ClientDetail> ClientDetails { get; set; }

    public virtual DbSet<ClientAddress> ClientAddress { get; set; }

    public virtual DbSet<ClientRevenue> ClientRevenue { get; set; }

    public virtual DbSet<Country> Country { get; set; }

    public virtual DbSet<JobDetail> JobDetails { get; set; }

    public virtual DbSet<JobStatusType> JobStatusType { get; set; }

    public virtual DbSet<JobOrder> JobOrders { get; set; }

    public virtual DbSet<JobOrderStatusType> JobOrderStatusType { get; set; }

    public virtual DbSet<Region> Region { get; set; }

    public virtual DbSet<VendorDetail> VendorDetails { get; set; }

    public virtual DbSet<VendorBilling> VendorBilling { get; set; }

    public virtual DbSet<VendorAddress> VendorAddress { get; set; }

    public virtual DbSet<AllocatedAtClient> AllocatedAtClient { get; set; }

    public virtual DbSet<ServiceType> ServiceType { get; set; }

    public virtual DbSet<Department> Department { get; set; }

    public virtual DbSet<WorkerDetail> WorkerDetail { get; set; }

    public virtual DbSet<WorkerStatusTypes> WorkerStatusType { get; set; }

    public virtual DbSet<WorkerContractStatusTypes> WorkerContractStatusType { get; set; }

    public virtual DbSet<WorkerEquipmentCost> WorkerEquipmentCost { get; set; }

    public virtual DbSet<WorkerEvaluation> WorkerEvaluation { get; set; }

    public virtual DbSet<WorkerBackgroundEvaluation> WorkerBackgroundEvaluation { get; set; }

    public virtual DbSet<WorkerBckgEvalValidationTypes> ValidationTypes { get; set; }

    public virtual DbSet<WorkerExtension> WorkerExtension { get; set; }

    public virtual DbSet<WorkerExtensionStatusTypes> WorkerExtensionStatusTypes { get; set; }

    public virtual DbSet<WorkerPayroll> WorkerPayroll { get; set; }

    public virtual DbSet<GetWorkerCountPerDepartment> GetWorkerCountPerDepartment { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new RoleConfiguration());


        modelBuilder.Entity<User>().Property(u => u.SId).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        modelBuilder.Entity<ClientDetail>(entity =>
        {
            entity.ToTable("client_details");

            entity.HasIndex(e => e.CountryId, "IX_client_details_CountryId");

            entity.Property(e => e.ClientCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            //entity.Property(e => e.ClientName)
            //    .HasMaxLength(256)
            //    .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Country).WithMany(p => p.ClientDetails)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_client_country");
        });

        modelBuilder.Entity<ClientAddress>(entity =>
        {
            entity.ToTable("client_address");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<ClientRevenue>(entity =>
        {
            entity.ToTable("client_revenue");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("country");

            entity.HasIndex(e => e.RegionId, "IX_country_RegionId");

            entity.Property(e => e.CountryCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CountryName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Region).WithMany(p => p.Countries)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_country_region");
        });

        modelBuilder.Entity<JobDetail>(entity =>
        {
            entity.ToTable("job_details");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.JobCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.JobDesc)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.JobLocation)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.JobStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.JobTitle)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Remarks)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Client).WithMany(p => p.JobDetails)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_job_clientID");

            entity.HasOne(d => d.Country).WithMany(p => p.JobDetails)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_job_countryID");

            entity.HasOne(d => d.JobStatusType).WithMany(p => p.JobDetails)
                .HasForeignKey(d => d.JobStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_job_jobStatus");

            entity.HasOne(d => d.AllocatedAtClient).WithMany(p => p.JobDetails)
                .HasForeignKey(d => d.AllocatedAtClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_job_allocatedAtClientID");

            entity.HasOne(d => d.ServiceType).WithMany(p => p.JobDetails)
                .HasForeignKey(d => d.ServiceTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_job_serviceTypeID");

            entity.HasOne(d => d.Department).WithMany(p => p.JobDetails)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_job_departmentID");
        });

        modelBuilder.Entity<JobStatusType>(entity =>
        {
            entity.ToTable("job_statusType");

            entity.Property(e => e.StatusTypeDesc)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.ToTable("region");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.RegionCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RegionName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VendorDetail>(entity =>
        {
            entity.ToTable("vendor_details");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.VendorCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            //entity.Property(e => e.VendorName)
            //    .HasMaxLength(256)
            //    .IsUnicode(false);

            entity.HasOne(d => d.Country).WithMany(p => p.VendorDetails)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_vendor_countryID");

        });

        modelBuilder.Entity<VendorBilling>(entity =>
        {
            entity.ToTable("vendor_billing");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<VendorAddress>(entity =>
        {
            entity.ToTable("vendor_address");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<JobOrder>(entity =>
        {
            entity.ToTable("job_orders");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.JobOrderStatusType).WithMany(p => p.JobOrder)
                .HasForeignKey(d => d.JobOrderStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_job_jobOrderStatus");
        });

        modelBuilder.Entity<JobOrderStatusType>(entity =>
        {
            entity.ToTable("job_order_statusType");
        });

        modelBuilder.Entity<ServiceType>(entity =>
        {
            entity.ToTable("service_type");
        });

        modelBuilder.Entity<AllocatedAtClient>(entity =>
        {
            entity.ToTable("allocatedAtClient");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("department");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<WorkerDetail>(entity =>
        {
            entity.ToTable("worker_details");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<WorkerStatusTypes>(entity =>
        {
            entity.ToTable("worker_statusTypes");
        });

        modelBuilder.Entity<WorkerContractStatusTypes>(entity =>
        {
            entity.ToTable("worker_contract_statusTypes");
        });

        modelBuilder.Entity<WorkerEquipmentCost>(entity =>
        {
            entity.ToTable("worker_equipment_cost");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<WorkerEvaluation>(entity =>
        {
            entity.ToTable("worker_evaluation");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<WorkerBackgroundEvaluation>(entity =>
        {
            entity.ToTable("worker_background_evaluation");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<WorkerBckgEvalValidationTypes>(entity =>
        {
            entity.ToTable("worker_bckg_eval_validationTypes");
        });

        modelBuilder.Entity<WorkerExtension>(entity =>
        {
            entity.ToTable("worker_extension");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<WorkerExtensionStatusTypes>(entity =>
        {
            entity.ToTable("worker_extension_statusTypes");
        });

        modelBuilder.Entity<WorkerPayroll>(entity =>
        {
            entity.ToTable("worker_payroll");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<GetWorkerCountPerDepartment>().HasNoKey();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
