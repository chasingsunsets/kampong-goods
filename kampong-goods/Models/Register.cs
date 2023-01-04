using System.ComponentModel.DataAnnotations;

namespace kampong_goods.Models
{
    public class Register
    {

/*        [ScaffoldColumn(false)]
        public int ID { get; set; }
        [Required, MaxLength(20)]*/
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

        [Required, RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;


        /*        [Required]
                public Boolean IsStaff { get; set; } = false;
        */



        /*  andrea*/
        public float WalletAmt { get; set; } = 0;

        /*  zhiyi*/
        public string GrpName { get; set; } = string.Empty;


    }
}
