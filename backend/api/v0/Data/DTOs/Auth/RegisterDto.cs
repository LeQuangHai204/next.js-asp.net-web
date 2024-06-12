using System.ComponentModel.DataAnnotations;

namespace Api.Model.Dtos.Auth
{
    public class RegisterDto : IEntityDto<AppUser>
    {
        public string[] Roles { get; set; } = { "user" };

        [Required]
        [MaxLength(255)]
        public required string UserName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MaxLength(255)]
        [MinLength(6)]
        public required string Password { get; set; }

    }
}