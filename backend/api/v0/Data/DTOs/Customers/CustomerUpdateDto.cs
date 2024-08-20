using System.ComponentModel.DataAnnotations;

namespace Api.Model.Dto
{
    public sealed class CustomerUpdateDto : UpdateDto<Customer>
    {
        [MaxLength(255)]
        public string? FullName { get; set; }

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