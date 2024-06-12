using System.ComponentModel.DataAnnotations;

namespace Api
{
    public interface IDbEntity<T> : IEntity
    {
        [Key]
        [Required]
        public T? Id { get; set; }
    }

    public interface IDbEntity : IDbEntity<int?>
    {

    }
}