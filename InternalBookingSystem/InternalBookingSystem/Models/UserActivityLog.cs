using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InternalBookingSystem.Models
{
    public class UserActivityLog
    {
        [Key]
        public int Id { get; set; }
        public string UserImployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeeEmail { get; set; }

        public string Action { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
