using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("sanpham")]
    public class Product : IDbEntity
    {
        [Key]
        [Required]
        [Column("SanphamID", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(255)]
        [Column("TenSanpham", TypeName = "varchar(255)")]
        public string? Name { get; set; }

        [Column("CungcapID", TypeName = "int")]
        public int? ProviderId { get; set; }

        [Column("DanhmucID", TypeName = "int")]
        public int? ProductTypeId { get; set; }

        [StringLength(255)]
        [Column("Donvi", TypeName = "varchar(255)")]
        public string? Unit { get; set; }

        [Column("Gia", TypeName = "decimal(13,2)")]
        public decimal? Price { get; set; }

        [ForeignKey("CungcapID")]
        public Provider? Provider { get; set; }

        [ForeignKey("DanhmucID")]
        public ProductType? ProductType { get; set; }
    }
}
