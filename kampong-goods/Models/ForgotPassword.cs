using System.ComponentModel.DataAnnotations;

namespace kampong_goods.Models
{
    public class ForgotPassword
    {
        [Required, RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;


    }
}
