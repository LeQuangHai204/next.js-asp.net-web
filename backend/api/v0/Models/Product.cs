using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Api.Model
{
    [Table("sanpham")]
    public class Product : IDbEntity
    {
        [Key]
        [Required]
        [Range(0, int.MaxValue)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("SanphamID", TypeName = "int")]
        public int? Id { get; set; }

        [ForeignKey(nameof(Provider))]
        [Required]
        [Range(0, int.MaxValue)]
        [Column("CungcapID", TypeName = "int")]
        public int? ProviderId { get; set; }
        public Provider? Provider { get; set; }

        [ForeignKey(nameof(ProductType))]
        [Range(0, int.MaxValue)]
        [Column("DanhmucID", TypeName = "int")]
        public int? ProductTypeId { get; set; }
        public ProductType? ProductType { get; set; }

        [MaxLength(255)]
        [Column("TenSanpham", TypeName = "varchar(255)")]
        public string? Name { get; set; }

        [MaxLength(255)]
        [Column("Donvi", TypeName = "varchar(255)")]
        public string? Unit { get; set; }

        [Precision(13, 2)]
        [Column("Gia", TypeName = "decimal(13,2)")]
        public decimal? Price { get; set; }
    }
}
