using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace kampong_goods.Models
{
    public class FAQ
    {
        public string FAQId { get; set; } 

        //QnA
        [Required, MaxLength(100)]
        public string Question { get; set; }
        [Required, MaxLength(10000)]
        public string Answer { get; set; }
        [Display(Name = "Reference")]
        public string URL { get; set; }

        //Category
        [Display(Name = "Category")]
        public FAQCategory? FAQCategory { get; set; }
        [Display(Name = "Category Abb.")]
        public string FAQCatId { get; set; }
        [Required]

        //User information
        public string Creator { get; set; }
        [Required]
        public string Editor {get; set;}

        //Date
        [Required]
        public string Date_Created { get; set; }
        public string Date_Modified { get; set; }
        
        //status
        public bool Publish { get; set; }
        public int ClickTime { get; set; }
    }
}
