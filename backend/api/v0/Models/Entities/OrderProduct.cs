using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("donhangchitiet")]
    public class OrderProduct : IDbEntity
    {
        [Key]
        [Required]
        [Column("DonhangChitietID", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("DonhangID", TypeName = "int")]
        public int? OrderId { get; set; }

        [Column("SanphamID", TypeName = "int")]
        public int? ProductId { get; set; }

        [Column("Soluong", TypeName = "int")]
        public int? Count { get; set; }

        [ForeignKey("DonhangID")]
        public Order? Order { get; set; }

        [ForeignKey("SanphamID")]
        public Product? Product { get; set; }
    }
}
