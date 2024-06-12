namespace Api.Model.Dtos.Auth
{
    public class JwtTokenDto : IEntityDto<AppUser>
    {
        public string? Payload { get; set; }
    }
}