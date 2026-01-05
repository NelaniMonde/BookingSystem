namespace InternalBookingSystem.UserActivityClasses
{
    public interface IUserActivityLogger
    {
        void LogUserActivity(string userId, string action, string name, 
            string email);
    }
}
