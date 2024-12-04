using System.ComponentModel.DataAnnotations;

namespace Tapfin.Api.Models.DTO
{
    public class ClientDetailsDto
    {

    }

    public class GetClientDtoResponse
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string ClientCode { get; set; } = null!;
        public string TIN { get; set; }
        public string BusinessName { get; set; }
        public string TradeName { get; set; }
        public DateTime DateOfEstablishment { get; set; }
        public string StateRegistration { get; set; }
        public string MunicipalRegistration { get; set; }
        public string Site { get; set; }
        public DateTime ClientSince { get; set; }
        public DateTime ClosureDate { get; set; }
        public string Observations { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public bool? IsActive { get; set; }
        public ClientRegionDto Region { get; set; }
        public ClientCountryDto Country { get; set; }
        public List<GetClientAddressDtoResponse> ClientAddress { get; set; }
        public List<GetClientUsersDtoResponse> User { get; set; }
        public List<GetClientDetailsRevenueDtoResponse> ClientRevenue { get; set; }
    }

    public class ClientRegionDto
    {
        public int Id { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
    }

    public class GetClientAddressDtoResponse
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

    public class GetClientDetailsRevenueDtoResponse
    {
        public int Id { get; set; }
        public double ServiceFee { get; set; }
        public string BusinessTerms { get; set; }
        public DateTime Date { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class GetClientUsersDtoResponse
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

    public class ClientCountryDto
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }

    public class CreateClientDtoRequest
    {
        [Required]
        public int CountryId { get; set; }
        [Required]
        public string ClientCode { get; set; }
        [Required]
        public string TIN { get; set; }
        [Required]
        public string? BusinessName { get; set; }
        public string? TradeName { get; set; }
        public DateTime? DateOfEstablishment { get; set; }
        public string? StateRegistration { get; set; }
        public string? MunicipalRegistration { get; set; }
        [Required]
        public string Site { get; set; }
        [Required]
        public DateTime ClientSince { get; set; }
        [Required]
        public DateTime ClosureDate { get; set; }
        public string? Observations { get; set; }
        [Required]
        public List<CreateClientAddressDto> Address { get; set; } = null!;
    }

    public class CreateClientAddressDto
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

    

    public class UpdateClientDtoRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ClientCode { get; set; }
        [Required]
        public string TIN { get; set; }
        [Required]
        public string BusinessName { get; set; }
        public string? TradeName { get; set; }
        public DateTime? DateOfEstablishment { get; set; }
        public string? StateRegistration { get; set; }
        public string? MunicipalRegistration { get; set; }
        public string? Site { get; set; }
        [Required]
        public DateTime ClientSince { get; set; }
        [Required]
        public DateTime ClosureDate { get; set; }
        public string? Observations { get; set; }
        //[Required]
        public List<UpdateClientDetailsAddressDto> Address { get; set; } = null!;
    }

    public class UpdateClientDetailsAddressDto
    {
        public int? Id { get; set; }
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
