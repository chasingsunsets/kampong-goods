using System.ComponentModel.DataAnnotations;

namespace kampong_goods.Models
{
	public class StaffInfo
	{
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [MaxLength((450))]
        public string Id { get; set; } = string.Empty;

        [Required, RegularExpression(@"^[STFG]\d{7}[A-Z]$", ErrorMessage = "Invalid NRIC."), MaxLength(9)]
        public string NRIC { get; set; } = string.Empty;


    }
}


