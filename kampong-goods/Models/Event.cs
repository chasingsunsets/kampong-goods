using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

namespace kampong_goods.Models
{
    public class Event
    {
        [MaxLength(1000)]
        [Display(Name = "Event ID")]
        public string EventId { get; set; } = string.Empty;

        [Required, MinLength(3, ErrorMessage = "Enter at least 3 characters."), MaxLength(30)]
        [Display(Name = "Event Name")]
        public string EventName { get; set; } = string.Empty;

        [Required, MaxLength(500)]
        [Display(Name = "Description")]
        public string EventDesc { get; set; } = string.Empty;

        [Required, MaxLength(1)]
        [Display(Name = "Type of event")]
        public string EventType { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Event Date")]
        //[Column(TypeName = "date")]
        public DateTime EventDate { get; set; } = new DateTime(2024, 1, 1, 1, 1, 1);

        [DataType(DataType.Time)]
        [Display(Name = "Event Time")]
        //[Column(TypeName = "time")]
        public DateTime EventTime { get; set; } = new DateTime(2024, 1, 1, 1, 1, 1);

        [Required, MinLength(3)]
        [Display(Name = "Address")]
        public string EventLocation { get; set; } = string.Empty;

        [DataType(DataType.PostalCode)]
        [Required, MaxLength(6, ErrorMessage = "Please enter a valid ZIP Code")]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; } = string.Empty;

        [Required, MaxLength(1)]
        [Display(Name = "Suitable for")]
        public string EventSuitability { get; set; } = string.Empty;

        [Required, MinLength(3, ErrorMessage = "Enter at least 3 characters.")]
        [Display(Name = "Organisers")]
        public string EventOrganiser { get; set; } = string.Empty;
    }
}
