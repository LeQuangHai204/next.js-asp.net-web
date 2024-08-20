namespace Api.Model.Dto.Auth
{
    public class JwtTokenDto : IEntityDto<AppUser>
    {
        public string? Payload { get; set; }
    }
}