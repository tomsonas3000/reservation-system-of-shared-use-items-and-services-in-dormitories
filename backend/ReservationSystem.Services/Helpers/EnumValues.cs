using System;
using System.Collections.Generic;
using System.Linq;

namespace ReservationSystem.Services.Helpers
{
    public static class EnumValues
    {
        public static IEnumerable<T> GetValues<T>() {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}