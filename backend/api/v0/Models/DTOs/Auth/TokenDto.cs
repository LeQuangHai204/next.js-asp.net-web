using Api.Models;

namespace Api
{
    public class JwtTokenDto : IEntityDto<AppUser>
    {
        public string? Payload { get; set; }
    }
}