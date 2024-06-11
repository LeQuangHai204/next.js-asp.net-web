using Microsoft.AspNetCore.Identity;

namespace Api
{
    public interface IJwtService
    {
        string GenerateToken(IdentityUser user);
    }
}