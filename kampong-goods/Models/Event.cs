using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kampong_goods.Models
{
    public class Event
    {
        [Display(Name = "Event Id")]
        public string EventId { get; set; }

        [Required, MinLength(3, ErrorMessage = "Enter at least 3 characters."), MaxLength(30)]
        [Display(Name = "Event Name")]
        public string EventName { get; set; } = string.Empty;

        [Required, MaxLength(500)]
        [Display(Name = "Description")]
        public string EventDesc { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Type of event")]
        public string EventType { get; set; } = string.Empty;

        [Display(Name = "Event Start")]
        public DateTime EventStart { get; set; } = DateTime.Today;

        [Display(Name = "Event End")]
        public DateTime EventEnd { get; set; } = DateTime.Today;

        [Required, MinLength(3)]
        [Display(Name = "Address")]
        public string EventLocation { get; set; } = string.Empty;

        [DataType(DataType.PostalCode)]
        [Required, MaxLength(6, ErrorMessage = "Please enter a valid ZIP Code")]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Suitable for")]
        public string EventSuitability { get; set; } = string.Empty;

        [Required, MinLength(3, ErrorMessage = "Enter at least 3 characters.")]
        [Display(Name = "Organisers")]
        public string EventOrganiser { get; set; } = string.Empty;
    }
}
