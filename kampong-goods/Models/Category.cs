using System.ComponentModel.DataAnnotations;

namespace kampong_goods.Models
{
    public class Category
    {
        [Required, MinLength(2), MaxLength(8)]
        public string CategoryId { get; set; } = string.Empty;
        [Required, MaxLength(30)]
        public string CategoryName { get; set; } = string.Empty;

        public List<Product>? Products { get; set; }
    }
}
