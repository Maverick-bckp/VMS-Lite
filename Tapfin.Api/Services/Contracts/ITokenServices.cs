using Tapfin.Api.Models.Entities;

namespace Tapfin.Api.Services.Contracts
{
    public interface ITokenServices
    {
        string CreateToken(User user, IList<string> roles);
    }
}
