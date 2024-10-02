using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Api.Model
{
    [Table("donhangchitiet")]
    public class OrderProduct : IDbEntity
    {
        [Key]
        [Required]
        [Column("DonhangChitietID", TypeName = "int")]
        [Range(0, int.MaxValue)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [ForeignKey(nameof(OrderId))]
        [Required]
        [Column("DonhangID", TypeName = "int")]
        [Range(0, int.MaxValue)]
        public int? OrderId { get; set; }
        public Order? Order { get; set; }

        [ForeignKey(nameof(ProductId))]
        [Required]
        [Column("SanphamID", TypeName = "int")]
        [Range(0, int.MaxValue)]
        public int? ProductId { get; set; }
        public Product? Product { get; set; }

        [Column("Soluong", TypeName = "int")]
        [Range(0, int.MaxValue)]
        public int? Count { get; set; }
    }
}

