/*using System.ComponentModel.DataAnnotations;

namespace kampong_goods.Models
{
    public class EditProfile
    {
        [Required, MaxLength(20)]
        public string Username { get; set; } = string.Empty;

        [Required, MaxLength(30)]
        [Display(Name = "First Name")]
        public string FName { get; set; } = string.Empty;

        [Required, MaxLength(30)]
        [Display(Name = "Last Name")]
        public string LName { get; set; } = string.Empty;

        [Required, RegularExpression(@"^[89][0-9]{7}$", ErrorMessage = "Invalid Phone Number"), MaxLength(8)]
        [Display(Name = "Phone Number")]
        public string PhoneNo { get; set; } = string.Empty;

        [Required, RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

    }
}
*/