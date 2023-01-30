using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace kampong_goods.Models
{
    public class Request
    {

        [Key]
        public int ID { get; set; }

        [Required, MinLength(3)]
        [Display(Name = "Request Title")]
        public string ReqTitle { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Category")]
        public string categoryId { get; set; } = string.Empty;

        [Required, MaxLength(1000)]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(7,2)")]
        public decimal Budget { get; set; }

        public string Status { get; set; } = "Available";

        [ForeignKey("UserId")]
        public virtual AppUser? User { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
