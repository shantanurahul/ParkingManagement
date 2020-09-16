using System;

namespace ParkingManagement.Extension
{
    public static class DateTimeExtensions
    {
        private static readonly TimeSpan startClock = new TimeSpan(8, 0, 0);
        private static readonly TimeSpan endClock = new TimeSpan(18, 0, 0);

        public static bool IsWeekend(this DateTime dateTime)
        {
            return (dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday);
        }

        public static bool IsFreeParkingHoursForParkDate(this DateTime dateTime)
        {
            return dateTime.TimeOfDay > endClock || IsWeekend(dateTime);
        }

        public static bool IsFreeParkingHoursForLeaveDate(this DateTime dateTime)
        {
            return dateTime.TimeOfDay < startClock || IsWeekend(dateTime);
        }
    }
}