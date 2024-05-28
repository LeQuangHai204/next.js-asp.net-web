using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class CustomerBasicInfoDto : IDbEntityDto<Customer>
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string? FullName { get; set; }

        [StringLength(255)]
        public string? City { get; set; }
    }
}