using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace kampong_goods.Models
{
    public class FAQCategory
    {
        [Required]
        public int Id { get; set; }
        [Display(Name = "Abridge")]
        public string FAQCatId { get; set; }
        [Required, MaxLength(30)]
        public string FAQCategoryName { get; set; } = string.Empty;
        public string FAQCatDescription { get; set; } = string.Empty;

    }
}
