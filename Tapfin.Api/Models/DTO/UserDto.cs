namespace Tapfin.Api.Models.DTO
{
    public class UserDto
    {
    }

    public class UserBasicdetailsDto
    {
        public int SId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? UserCustomId { get; set; }
        public string? Email { get; set; }
    }
}
