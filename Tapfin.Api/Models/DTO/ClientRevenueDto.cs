using System.ComponentModel.DataAnnotations;

namespace Tapfin.Api.Models.DTO
{
    public class ClientRevenueDto
    {
        public class GetClientRevenueDtoResponse
        {
            public int Id { get; set; }
            public int ClientId { get; set; }
            public double ServiceFee { get; set; }
            public string BusinessTerms { get; set; }
            public DateTime Date { get; set; }
            public DateTime? CreatedAt { get; set; }
            public string? CreatedBy { get; set; }
            public GetClientRevenueClientDetailsDtoResponse ClientDetail { get; set; }
        }

        public class GetClientRevenueClientDetailsDtoResponse
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
        }

        public class CreateClientRevenueDtoRequest
        {
            [Required]
            public int ClientId { get; set; }
            [Required]
            public double ServiceFee { get; set; }
            [Required]
            public string BusinessTerms { get; set; }
            [Required]
            public DateTime Date { get; set; }
        }

        public class UpdateClientRevenueDtoRequest
        {
            [Required]
            public int Id { get; set; }
            [Required]
            public double ServiceFee { get; set; }
            [Required]
            public string BusinessTerms { get; set; }
            [Required]
            public DateTime Date { get; set; }
        }
    }
}
