using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("donhang")]
    public class Order : IDbEntity
    {
        [Key]
        [Required]
        [Column("DonhangID", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("KhachhangID", TypeName = "int")]
        public required int customerId { get; set; }

        [Column("NhanvienID", TypeName = "int")]
        public int? staffId { get; set; }

        [Column("ShipperID", TypeName = "int")]
        public int? shipperId { get; set; }

        [Column("Ngaydathang", TypeName = "date")]
        public DateTime date { get; set; }
    }
}