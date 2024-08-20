using System.ComponentModel.DataAnnotations;

namespace Api.Model.Dto
{
    public sealed class CustomerQueryDto : EntityQueryDto<Customer>
    {
        [MaxLength(255)]
        public string? FullName { get; set; } = null;

        [MaxLength(255)]
        public string? City { get; set; } = null;

        [MaxLength(255)]
        public string? Country { get; set; } = null;
    }
}