using System;
using System.Collections.Generic;

namespace Tapfin.Api.Models.Entities;

public partial class ClientDetail
{
    public int Id { get; set; }

    public int CountryId { get; set; }

    public string ClientCode { get; set; } = null!;

    public string TIN { get; set; } 

    public string BusinessName { get; set; } 

    public string? TradeName { get; set; }

    public DateTime? DateOfEstablishment { get; set; }

    public string? StateRegistration { get; set; }

    public string? MunicipalRegistration { get; set; }

    public string Site { get; set; }

    public DateTime ClientSince { get; set; } 

    public DateTime ClosureDate { get; set; }

    public string? Observations { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }

    public bool? IsActive { get; set; }

    public virtual Country Country { get; set; }

    public virtual ICollection<ClientRevenue>? ClientRevenue { get; set; }

    public virtual ICollection<User>? AspNetUsers { get; set; } = new List<User>();

    public virtual ICollection<ClientAddress> ClientAddress { get; set; } = new List<ClientAddress>();

    public virtual ICollection<JobDetail> JobDetails { get; set; } = new List<JobDetail>();
}
