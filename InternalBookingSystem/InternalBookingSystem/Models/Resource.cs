using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InternalBookingSystem.Models
{
    public class Resource
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public int Capacity { get; set; }

        [DisplayName("Is Available")]
        public bool IsAvailable { get; set; }

        public  ICollection<Booking> BookingsList { get; set; }    
    }
}
