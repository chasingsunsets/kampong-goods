using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace kampong_goods.Models
{
    public class FAQCategory
    {
        public int Id { get; set; }
        [Required]
        public string FAQCategoryName { get; set; } = string.Empty;
        public string FAQCatDescription { get; set; } = string.Empty;

    }
}
