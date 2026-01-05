using InternalBookingSystem.Data;
using InternalBookingSystem.Models;

namespace InternalBookingSystem.UserActivityClasses
{
    public class UserActivityLogger : IUserActivityLogger
    {
        private readonly ApplicationDbContext _appContext;

        public UserActivityLogger(ApplicationDbContext appContext)
        {
            _appContext = appContext;
        }
        public void LogUserActivity(string userImployeeId, string action, 
            string name, string email)
        {
            var log = new UserActivityLog
            {
                UserImployeeId = userImployeeId,
                Action = action,
                Timestamp = DateTime.UtcNow,
                EmployeeName = name,
                EmployeeEmail = email

            }; 

            _appContext.UserActivityLogs.Add(log);
            _appContext.SaveChanges();


        }
    }
}
