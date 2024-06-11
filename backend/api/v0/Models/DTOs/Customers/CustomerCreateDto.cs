using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public sealed class CustomerCreateDto : CreateDto<Customer>
    {
        [Required]
        [MaxLength(255)]
        public required string FullName { get; set; }

        [MaxLength(255)]
        public string? NickName { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }

        [MaxLength(255)]
        public string? City { get; set; }

        [MaxLength(255)]
        public string? PostalCode { get; set; }

        [MaxLength(255)]
        public string? Country { get; set; }
    }
}