using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("danhmuc")]
    public class ProductType : IDbEntity
    {
        [Key]
        [Required]
        [Column("DanhmucID", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(255)]
        [Column("TenDanhMuc", TypeName = "varchar(255)")]
        public string? Name { get; set; }

        [StringLength(255)]
        [Column("MoTa", TypeName = "varchar(255)")]
        public string? Description { get; set; }
    }
}
