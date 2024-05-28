using System.ComponentModel.DataAnnotations;

namespace Api
{
    public interface IDbEntity : IEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }
}