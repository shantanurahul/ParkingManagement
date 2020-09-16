using System;

namespace ParkingManagement.Extension
{
    public static class DateTimeExtensions
    {
        private static readonly TimeSpan StartClock = new TimeSpan(8, 0, 0);
        private static readonly TimeSpan EndClock = new TimeSpan(18, 0, 0);

        public static bool IsWeekend(this DateTime dateTime)
        {
            return (dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday);
        }

        public static bool IsFreeParkingHoursForParkDate(this DateTime dateTime)
        {
            return dateTime.TimeOfDay > EndClock || IsWeekend(dateTime);
        }

        public static bool IsFreeParkingHoursForLeaveDate(this DateTime dateTime)
        {
            return dateTime.TimeOfDay < StartClock || IsWeekend(dateTime);
        }
    }
}