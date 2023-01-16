using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kampong_goods.Models
{
    public class StaffInfo
    {
        [Key]
        public int ID { get; set; }

/*        [Required, Column(TypeName = "nvarchar(450)")]
        public string Id { get; set; } = string.Empty;*/


        [Required, RegularExpression(@"^[STFG]\d{7}[A-Z]$", ErrorMessage = "Invalid NRIC."), MaxLength(9)]
        public string NRIC { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public virtual AppUser? User { get; set; }
        [Required]
        public string UserId { get; set; }


    }
}


