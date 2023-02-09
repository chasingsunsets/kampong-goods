using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace kampong_goods.Models
{
    public class AddRequest
    {


        [Required, MinLength(3)]
        [Display(Name = "Request Title")]
        public string ReqTitle { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Category")]
        public string CategoryId { get; set; } = string.Empty;
        public Category? Category { get; set; }

        [Required, MaxLength(1000)]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Required, ]
        [Display(Name = "Budget")]
        public decimal Budget { get; set; }

        public string Status { get; set; } = "Available";

    }
}
