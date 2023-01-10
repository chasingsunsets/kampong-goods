using System.ComponentModel.DataAnnotations;

namespace kampong_goods.Models
{
    public class AddStaff
    {
        [RegularExpression(@"^[STFG]\d{7}[A-Z]$", ErrorMessage = "Invalid NRIC."), MaxLength(9)]
        public string NRIC { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string Username { get; set; } = string.Empty;

        [Required, MaxLength(30)]
        [Display(Name = "First Name")]
        public string FName { get; set; } = string.Empty;

        [Required, MaxLength(30)]
        [Display(Name = "Last Name")]
        public string LName { get; set; } = string.Empty;


        [Required, RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*\\W)(?!.* ).{5,}$", ErrorMessage = "Password must contain at least one digit, one lowercase, one uppercase, one special character,no space, and a minimum of 5 characters.")]

        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
        //

        [Required, RegularExpression(@"^[89][0-9]{7}$", ErrorMessage = "Invalid Phone Number"), MaxLength(8)]
        [Display(Name = "Phone Number")]
        public string PhoneNo { get; set; } = string.Empty;





    }
}
