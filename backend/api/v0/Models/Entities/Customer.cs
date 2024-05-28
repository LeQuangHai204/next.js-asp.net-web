using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("khachhang")]
    public class Customer : IDbEntity
    {
        [Key]
        [Required]
        [Column("KhachhangID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Column("HoTen", TypeName = "varchar(255)")]
        public required string FullName { get; set; }

        [StringLength(255)]
        [Column("TenLienLac", TypeName = "varchar(255)")]
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
        [Column("QuocGia", TypeName = "varchar(255)")]
        public string? Country { get; set; }
    }
}