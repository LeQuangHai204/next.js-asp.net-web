using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class CustomerCreateDto : IEntityDto<Customer>
    {
        [Required]
        [StringLength(255)]
        public required string FullName { get; set; }

        [StringLength(255)]
        public string? NickName { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }

        [StringLength(255)]
        public string? City { get; set; }

        [StringLength(255)]
        public string? PostalCode { get; set; }

        [StringLength(255)]
        public string? Country { get; set; }
    }
}