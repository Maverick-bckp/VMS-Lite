using System;
using System.Collections.Generic;

namespace Tapfin.Api.Models.Entities;

public partial class Region
{
    public int Id { get; set; }

    public string RegionCode { get; set; } = null!;

    public string RegionName { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Country> Countries { get; set; } = new List<Country>();
}
