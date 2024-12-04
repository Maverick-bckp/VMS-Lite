namespace Tapfin.Api.Models.DTO
{
    public class WorkerBI
    {

    }

    public class WorkerBIDetailsRequestDto
    {
        public int? CountryId { get; set; } = null;
        public int? ClientId { get; set; } = null;
        public int? VendorId { get; set; } = null;
    }

    public class GetWorkerCountPerDepartmentSPDto
    {
        public int? WorkerCount { get; set; }
        public string? DepartmentName { get; set; }
    }
}
