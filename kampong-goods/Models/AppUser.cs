﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace kampong_goods.Models
{
    public class AppUser : IdentityUser
    {
        // for chat message function: constructor
        public AppUser()
        {
            Messages = new HashSet<Message>();
        }

        // for message: 1 to many relationship
        public virtual ICollection<Message> Messages { get; set; }

        /*      [RegularExpression(@"^[STFG]\d{7}[A-Z]$", ErrorMessage = "Invalid NRIC."), MaxLength(9)]
                public string NRIC { get; set; } = string.Empty;*/

        [Required, MaxLength(30)]
        [Display(Name = "First Name")]
        public string FName { get; set; } = string.Empty;

        [Required, MaxLength(30)]
        [Display(Name = "Last Name")]
        public string LName { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        /*  andrea*/
        public float WalletAmt { get; set; } = 0;

        /*  zhiyi*/
        public string GrpName { get; set; } = string.Empty;
    }
}
