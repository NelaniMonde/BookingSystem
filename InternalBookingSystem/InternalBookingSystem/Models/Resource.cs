using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InternalBookingSystem.Models
{
    public class Resource
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Please enter a name, No Numbers allowed")]
        public string Name { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Please enter a name, No Numbers allowed")]
        public string Description { get; set; }

        public string Location { get; set; }

        [Range(0, 1200)]
        public int Capacity { get; set; }

        [DisplayName("Is Available")]
        public bool IsAvailable { get; set; }

        public  ICollection<Booking> BookingsList { get; set; }    
    }
}
