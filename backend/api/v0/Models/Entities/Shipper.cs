using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("giaohang")]
    public class Shipper : IDbEntity
    {
        [Key]
        [Required]
        [Column("ShipperID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(255)]
        [Column("Hoten", TypeName = "varchar(255)")]
        public string? Name { get; set; }

        [StringLength(255)]
        [Column("Sodienthoai", TypeName = "varchar(255)")]
        public string? PhoneNo { get; set; }
    }
}
