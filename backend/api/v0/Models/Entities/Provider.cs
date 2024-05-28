using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("cungcap")]
    public class Provider : IDbEntity
    {
        [Key]
        [Required]
        [Column("CungcapID", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(255)]
        [Column("Tendaydu", TypeName = "varchar(255)")]
        public string? FullName { get; set; }

        [StringLength(255)]
        [Column("TenLienhe", TypeName = "varchar(255)")]
        public string? NickName { get; set; }

        [StringLength(255)]
        [Column("Diachi", TypeName = "varchar(255)")]
        public string? Address { get; set; }

        [StringLength(255)]
        [Column("Thanhpho", TypeName = "varchar(255)")]
        public string? City { get; set; }

        [StringLength(255)]
        [Column("MaBuudien", TypeName = "varchar(255)")]
        public string? PostalCode { get; set; }

        [StringLength(255)]
        [Column("Quocgia", TypeName = "varchar(255)")]
        public string? Country { get; set; }

        [StringLength(255)]
        [Column("Dienthoai", TypeName = "varchar(255)")]
        public string? PhoneNo { get; set; }
    }
}

