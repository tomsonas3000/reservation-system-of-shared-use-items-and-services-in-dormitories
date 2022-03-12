using ReservationSystem.DataAccess.Enums;

namespace ReservationSystem.Services.Helpers
{
    public static class UserFriendlyServiceType
    {
        public static string GetUserFriendlyServiceType(this ServiceType type)
        {
            switch (type)
            {
                case ServiceType.Basketball:
                    return "Basketball";
                case ServiceType.Football:
                    return "Football";
                case ServiceType.Shower:
                    return "Shower";
                case ServiceType.Volleyball:
                    return "Volleyball";
                case ServiceType.DryingMachine:
                    return "Drying machine";
                case ServiceType.PingPong:
                    return "Ping pong table";
                case ServiceType.VacuumCleaner:
                    return "Vacuum cleaner";
                case ServiceType.WashingMachine:
                    return "Washing machine";
                default:
                    return string.Empty;
            }
        }
    }
}