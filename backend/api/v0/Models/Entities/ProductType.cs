using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("danhmuc")]
    public class ProductType : IDbEntity
    {
        [Key]
        [Required]
        [Range(0, int.MaxValue)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("DanhmucID", TypeName = "int")]
        public int? Id { get; set; }

        [MaxLength(255)]
        [Column("TenDanhMuc", TypeName = "varchar(255)")]
        public string? Name { get; set; }

        [MaxLength(255)]
        [Column("MoTa", TypeName = "varchar(255)")]
        public string? Description { get; set; }
    }
}
