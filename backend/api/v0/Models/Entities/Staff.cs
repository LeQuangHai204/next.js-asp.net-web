using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("nhanvien")]
    public class Staff : IDbEntity
    {
        [Key]
        [Required]
        [Column("NhanviennID", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(255)]
        [Column("Ten", TypeName = "varchar(255)")]
        public string? LastName { get; set; }

        [StringLength(255)]
        [Column("Ho", TypeName = "varchar(255)")]
        public string? SurName { get; set; }

        [Column("NgaySinh", TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(255)]
        [Column("Anh", TypeName = "varchar(255)")]
        public string? Image { get; set; }

        [Column("Ghichu", TypeName = "text")]
        public string? Notes { get; set; }
    }
}
