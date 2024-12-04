using System;
using System.Collections.Generic;

namespace Tapfin.Api.Models.Entities;

public partial class JobStatusType
{
    public int Id { get; set; }

    public string StatusTypeDesc { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual ICollection<JobDetail> JobDetails { get; set; } = new List<JobDetail>();
}
