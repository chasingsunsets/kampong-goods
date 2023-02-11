using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace kampong_goods.Models
{

    public class Product
    {
        public string ProductId { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Category")]
        public string CategoryId { get; set; } = string.Empty;
        public Category? Category { get; set; }

        [Required, MaxLength(1000)]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Condition")]
        public string ConditionId { get; set; } = string.Empty;
        public Condition? Condition { get; set; }

        [Range(0, 10)]
        [Column(TypeName = "decimal(7,2)")]
        public decimal Price { get; set; }

        [MaxLength(50)]
        public string? ImageURL { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime productCreatedTime { get; set; }

        public Product()
        {
            productCreatedTime = DateTime.Now;
            Guid myuuid = Guid.NewGuid();
            ProductId = myuuid.ToString();
            Status = "Available";
        }
    }
}
