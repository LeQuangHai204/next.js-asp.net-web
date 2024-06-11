using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public sealed class CustomerBasicInfoDto : BasicInfoDto<Customer>
    {
        [Key]
        [Required]
        [RegularExpression("[1-9][0-9]*")]
        [Range(0, int.MaxValue)]
        public int? Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string? FullName { get; set; }

        [MaxLength(255)]
        public string? City { get; set; }

        [MaxLength(255)]
        public string? NickName { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }

        [MaxLength(255)]
        public string? PostalCode { get; set; }

        [MaxLength(255)]
        public string? Country { get; set; }
    }
}