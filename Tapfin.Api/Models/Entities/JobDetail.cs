using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tapfin.Api.Models.Entities;

public partial class JobDetail
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public int CountryId { get; set; }

    public int? ServiceTypeId { get; set; }

    public int? DepartmentId { get; set; }

    public int? AllocatedAtClientId { get; set; }

    public string JobCode { get; set; } = null!;

    public string? JobTitle { get; set; }

    public string? JobDesc { get; set; }

    public string JobLocation { get; set; } = null!;

    public int NoOfPosition { get; set; }

    public int? YearsOfExperience { get; set; }

    public double? ExpectedSalary { get; set; }

    public string? Remarks { get; set; }

    public int JobStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }

    public bool? IsActive { get; set; }

    public virtual ClientDetail Client { get; set; } = null!;

    [ForeignKey("Id")]
    public virtual Country Country { get; set; } = null!;

    [ForeignKey("Id")]
    public virtual JobStatusType JobStatusType { get; set; } = null!;
    [ForeignKey("Id")]
    public virtual AllocatedAtClient AllocatedAtClient { get; set; } = null!;
    [ForeignKey("Id")]
    public virtual ServiceType ServiceType { get; set; } = null!;
    [ForeignKey("Id")]
    public virtual Department Department { get; set; } = null!;

    public virtual List<WorkerDetail> WorkerDetail { get; set; } = new List<WorkerDetail>();
}