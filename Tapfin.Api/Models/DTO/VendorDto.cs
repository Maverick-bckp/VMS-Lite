using System.ComponentModel.DataAnnotations;

namespace Tapfin.Api.Models.DTO
{
    public class VendorDto
    {
    }

    public class GetVendorDetailsDtoResponse
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string VendorCode { get; set; } = null!;
        public DateTime StartOfServiceProvision { get; set; }
        public DateTime EndOfServiceProvision { get; set; }
        public string ContractNo { get; set; }
        public string ServiceDescription { get; set; }
        public string NationalRegistryOfLegalEntities { get; set; }
        public string BusinessName { get; set; }
        public string TradeName { get; set; }
        public DateTime DateOfEstablishment { get; set; }
        public string StateRegistration { get; set; }
        public string MunicipalRegistration { get; set; }
        public string Site { get; set; }
        public DateTime ClientSince { get; set; }
        public DateTime ClosureDate { get; set; }
        public string Observations { get; set; }
        public string DefaultVendor { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public bool? IsActive { get; set; }
        public VendorDetailsRegionDto Region { get; set; }
        public VendorDetailsCountryDto Country { get; set; }
        public List<GetVendorAddressDtoResponse> VendorAddress { get; set; }
        public List<GetVendorUsersDtoResponse> User { get; set; }
        public List<GetVendorDetailsBillingDtoResponse> VendorBilling { get; set; }
    }

    public class VendorDetailsRegionDto
    {
        public int Id { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
    }

    public class VendorDetailsCountryDto
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }

    public class GetVendorAddressDtoResponse
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public int ZipCode { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Neighbourhood { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Complement { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class GetVendorUsersDtoResponse
    {
        public int SId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? UserCustomId { get; set; }
        public string? Email { get; set; }
        public int CountryId { get; set; }
        public int? ClientId { get; set; }
        public int? VendorId { get; set; }
        public int? DepartmentId { get; set; }
        public string? Telephone { get; set; }
        public string? Cellphone { get; set; }
        public string? Observation { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class GetVendorDetailsBillingDtoResponse
    {
        public int Id { get; set; }
        public double ServiceFee { get; set; }
        public string CommercialTerms { get; set; }
        public DateTime Date { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class CreateVendorDtoRequest
    {
        [Required]
        public int CountryId { get; set; }
        [Required]
        public string VendorCode { get; set; }
        [Required]
        public DateTime? StartOfServiceProvision { get; set; }
        [Required]
        public DateTime? EndOfServiceProvision { get; set; }
        [Required]
        public string? ContractNo { get; set; }
        [Required]
        public string? ServiceDescription { get; set; }
        public string? NationalRegistryOfLegalEntities { get; set; }
        [Required]
        public string? BusinessName { get; set; }
        public string? TradeName { get; set; }
        public DateTime? DateOfEstablishment { get; set; }
        public string? StateRegistration { get; set; }
        public string? MunicipalRegistration { get; set; }
        public string? Site { get; set; }
        [Required]
        public DateTime? ClientSince { get; set; }
        [Required]
        public DateTime? ClosureDate { get; set; }
        public string? Observations { get; set; }
        public string? DefaultVendor { get; set; }
        [Required]
        public List<CreateVendorAddressDto> Address { get; set; } = null!;
    }

    public class CreateVendorAddressDto
    {
        [Required]
        public string Location { get; set; }
        [Required]
        public int ZipCode { get; set; }
        public string? Street { get; set; }
        public int? Number { get; set; }
        public string? Neighbourhood { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }
        public string? Complement { get; set; }
    }

    public class UpdateVendorDetailsDtoRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string VendorCode { get; set; }
        [Required]
        public DateTime? StartOfServiceProvision { get; set; }
        [Required]
        public DateTime? EndOfServiceProvision { get; set; }
        [Required]
        public string? ContractNo { get; set; }
        [Required]
        public string? ServiceDescription { get; set; }
        public string? NationalRegistryOfLegalEntities { get; set; }
        [Required]
        public string? BusinessName { get; set; }
        public string? TradeName { get; set; }
        public DateTime? DateOfEstablishment { get; set; }
        public string? StateRegistration { get; set; }
        public string? MunicipalRegistration { get; set; }
        public string? Site { get; set; }
        [Required]
        public DateTime? ClientSince { get; set; }
        [Required]
        public DateTime? ClosureDate { get; set; }
        public string? Observations { get; set; }
        public string? DefaultVendor { get; set; }
        [Required]
        public List<UpdateVendorDetailsAddressDto> Address { get; set; } = null!;
    }

    public class UpdateVendorDetailsAddressDto
    {
        public int Id { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public int ZipCode { get; set; }
        public string? Street { get; set; }
        public int? Number { get; set; }
        public string? Neighbourhood { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }
        public string? Complement { get; set; }
    }
}
