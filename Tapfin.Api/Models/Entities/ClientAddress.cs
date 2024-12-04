namespace Tapfin.Api.Models.Entities
{
    public class ClientAddress
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string? Location { get; set; }

        public int ZipCode { get; set; }

        public string? Street { get; set; }

        public int? Number { get; set; }

        public string? Neighbourhood { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string? Complement { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public bool? IsActive { get; set; }

        public virtual ClientDetail Client { get; set; } = null!;
    }
}
