﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace kampong_goods.Models
{
    public class FAQ
    {
        public string FAQId { get; set; } 

        [Required, MaxLength(1000)]
        public string Question { get; set; }
        [Required, MaxLength(1000)]
        public string Answer { get; set; }
        [Display(Name = "Category")]
        public string FAQCategory { get; set; }
        [Required]
        public string Creator { get; set; }
        [Required]
        public string Editor {get; set;}
        [Required]
        public string Date_Created { get; set; }
        public string Date_Modified { get; set; }
        public string URL { get; set; }
    }
}
