using Microsoft.AspNetCore.Identity;

namespace NZWalksAPI.IRepository
{
    public interface ITokenRepositoty
    {
       string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
