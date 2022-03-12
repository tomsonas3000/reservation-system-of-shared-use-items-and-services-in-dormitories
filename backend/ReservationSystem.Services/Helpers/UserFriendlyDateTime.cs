using System;

namespace ReservationSystem.Services.Helpers
{
    public static class UserFriendlyDateTime
    {
        public static string GetUserFriendlyDateTime(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd hh:mm");
        }
    }
}