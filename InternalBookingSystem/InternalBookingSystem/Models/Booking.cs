using System.ComponentModel.DataAnnotations;

namespace InternalBookingSystem.Models
{
    public class Booking
    {
        [Required]
        public int Id { get; set; }

        public int ResourcedId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string BookedBy { get; set; }

        public string Purpose { get; set; }


    }
}
