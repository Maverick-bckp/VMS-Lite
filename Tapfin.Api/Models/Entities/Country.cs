using System;
using System.Collections.Generic;

namespace Tapfin.Api.Models.Entities;

public partial class Country
{
    public int Id { get; set; }

    public int RegionId { get; set; }

    public string CountryCode { get; set; } = null!;

    public string CountryName { get; set; } = null!;

    public string Currency { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<ClientDetail> ClientDetails { get; set; } = new List<ClientDetail>();

    public virtual ICollection<JobDetail> JobDetails { get; set; } = new List<JobDetail>();

    public virtual Region Region { get; set; } = null!;

    public virtual ICollection<VendorDetail> VendorDetails { get; set; } = new List<VendorDetail>();
}
