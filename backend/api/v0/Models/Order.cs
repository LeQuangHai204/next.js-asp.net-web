using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Model
{
    [Table("donhang")]
    public class Order : IDbEntity
    {
        [Key]
        [Required]
        [Range(0, int.MaxValue)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("DonhangID", TypeName = "int")]
        public int? Id { get; set; }

        [ForeignKey(nameof(Customer))]
        [Range(0, int.MaxValue)]
        [Column("KhachhangID", TypeName = "int")]
        public int? customerId { get; set; }
        public Customer? Customer { get; set; }

        [ForeignKey(nameof(Staff))]
        [Range(0, int.MaxValue)]
        [Column("NhanvienID", TypeName = "int")]
        public int? staffId { get; set; }
        public Staff? Staff { get; set; }

        [Column("Ngaydathang", TypeName = "date")]
        public DateTime date { get; set; }

        [ForeignKey(nameof(Shipper))]
        [Range(0, int.MaxValue)]
        [Column("ShipperID", TypeName = "int")]
        public int? shipperId { get; set; }
        public Shipper? Shipper { get; set; }
    }
}