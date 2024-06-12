using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Model
{
    [Table("giaohang")]
    public class Shipper : IDbEntity
    {
        [Key]
        [Required]
        [Range(0, int.MaxValue)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ShipperID", TypeName = "int")]
        public int? Id { get; set; }

        [MaxLength(255)]
        [Column("Hoten", TypeName = "varchar(255)")]
        public string? Name { get; set; }

        [Phone]
        [MaxLength(255)]
        [Column("Sodienthoai", TypeName = "varchar(255)")]
        public string? PhoneNo { get; set; }
    }
}
