using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Model
{
    [Table("cungcap")]
    public class Provider : IDbEntity
    {
        [Key]
        [Required]
        [Range(0, int.MaxValue)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CungcapID", TypeName = "int")]
        public int? Id { get; set; }

        [MaxLength(255)]
        [Column("Tendaydu", TypeName = "varchar(255)")]
        public string? FullName { get; set; }

        [MaxLength(255)]
        [Column("TenLienhe", TypeName = "varchar(255)")]
        public string? NickName { get; set; }

        [MaxLength(255)]
        [Column("Diachi", TypeName = "varchar(255)")]
        public string? Address { get; set; }

        [MaxLength(255)]
        [Column("Thanhpho", TypeName = "varchar(255)")]
        public string? City { get; set; }

        [MaxLength(255)]
        [Column("MaBuudien", TypeName = "varchar(255)")]
        public string? PostalCode { get; set; }

        [MaxLength(255)]
        [Column("Quocgia", TypeName = "varchar(255)")]
        public string? Country { get; set; }

        [Phone]
        [MaxLength(255)]
        [Column("Dienthoai", TypeName = "varchar(255)")]
        public string? PhoneNo { get; set; }
    }
}

