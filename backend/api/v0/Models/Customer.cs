using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Model
{
    [Table("khachhang")]
    public class Customer : IDbEntity
    {
        [Key]
        [Required]
        [Range(0, int.MaxValue)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("KhachhangID", TypeName = "int")]
        public int? Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("HoTen", TypeName = "varchar(255)")]
        public required string FullName { get; set; }

        [MaxLength(255)]
        [Column("TenLienLac", TypeName = "varchar(255)")]
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
        [Column("QuocGia", TypeName = "varchar(255)")]
        public string? Country { get; set; }
    }
}