using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InternalBookingSystem.Models
{
    public class Booking
    {
        [Key]
        [Required]
        public int Id { get; set; }

        //Resource foreign key 
        public int ResourcedId { get; set; }

        public Resource Resource { get; set; }

        [DisplayName("Start Time")]
        [DataType(DataType.Date)]
        public DateTime StartTime { get; set; }

        [DisplayName("End Time")]
        [DataType(DataType.Date)]
        public DateTime EndTime { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "No Numbers allowed")]
        [DisplayName("Booked By")]
        public string BookedBy { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "No Numbers allowed")]
        public string Purpose { get; set; }


    }
}
