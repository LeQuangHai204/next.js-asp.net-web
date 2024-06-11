using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class LoginDto : IEntityDto<AppUser>
    {
        [Required]
        [MaxLength(255)]
        public required string UserName { get; set; }

        [Required]
        [MaxLength(255)]
        [MinLength(6)]
        public required string Password { get; set; }
    }
}