using System.ComponentModel.DataAnnotations;

namespace Tapfin.Api.Models.DTO
{
    public class VendorBillingDto
    {
    }

    public class GetVendorBillingDtoResponse
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public double ServiceFee { get; set; }
        public string CommercialTerms { get; set; }
        public DateTime Date { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public GetVendorBillingVendorDetailsDtoResponse VendorDetail { get; set; }
    }

    public class GetVendorBillingVendorDetailsDtoResponse
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string VendorCode { get; set; } = null!;
        public DateTime? StartOfServiceProvision { get; set; }
        public DateTime? EndOfServiceProvision { get; set; }
        public string? ContractNo { get; set; }
        public string? ServiceDescription { get; set; }
        public string? NationalRegistryOfLegalEntities { get; set; }
        public string? BusinessName { get; set; }
        public string? TradeName { get; set; }
        public DateTime? DateOfEstablishment { get; set; }
        public string? StateRegistration { get; set; }
        public string? MunicipalRegistration { get; set; }
        public string? Site { get; set; }
        public DateTime? ClientSince { get; set; }
        public DateTime? ClosureDate { get; set; }
        public string? Observations { get; set; }
        public string? DefaultVendor { get; set; }
    }

    public class CreateVendorBillingDtoRequest
    {
        [Required]
        public int VendorId { get; set; }
        [Required]
        public double ServiceFee { get; set; }
        [Required]
        public string CommercialTerms { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }

    public class UpdateVendorBillingDtoRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public double ServiceFee { get; set; }
        [Required]
        public string CommercialTerms { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
