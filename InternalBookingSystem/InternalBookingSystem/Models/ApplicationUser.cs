using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InternalBookingSystem.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public int Name { get; set; }

        public string? Position { get; set; }

        public string? EmployeeNumber { get; set; }

        public  string? EmplyeePhoneNumber { get; set; }

        public string? Email { get; set; }
    }
}
