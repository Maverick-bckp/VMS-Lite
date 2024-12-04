namespace Tapfin.Api.Models.DTO
{
    public class ServiceTypeDto
    {

    }

    public class GetServiceTypeDtoResponse
    {
        public int Id { get; set; }
        public string ServiceTypeDesc { get; set; } = null!;
    }
}
