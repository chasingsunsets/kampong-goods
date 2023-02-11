using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace kampong_goods.Models
{
    public class ResetPassword
    {

        [Required, RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required, RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*\\W)(?!.* ).{5,}$", ErrorMessage = "Password must contain at least one digit, one lowercase, one uppercase, one special character,no space, and a minimum of 5 characters.")]

        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;
    }
}
