using System;
using ParkingManagement.Extension;

namespace ParkingManagement
{
    public class ChargeableTimeCalculator
    {
        private static readonly TimeSpan StartClock = new TimeSpan(8, 0, 0);
        private static readonly TimeSpan EndClock = new TimeSpan(18, 0, 0);
        private const int FullDayParkingInMinutes = 10 * 60;

        public double GetChargeableMinutes(DateTime entryTime, DateTime exitTime)
        {
            double totalChargeableMinutes;

            // Car is Parked for over a day
            if (exitTime.Date > entryTime.Date)
            {
                var chargeableTimeEntryDay = GetChargeableTimeEntryDay(entryTime);
                var chargeableTimeForEntireDays = GetChargeableTimeForFullDays(entryTime, exitTime);
                var chargeableTimeExitDay = GetChargeableTimeExitDay(exitTime);

                totalChargeableMinutes = chargeableTimeEntryDay + chargeableTimeForEntireDays + chargeableTimeExitDay;
            }
            else
            {
                var currentEntryTime = entryTime.TimeOfDay < StartClock ? StartClock : entryTime.TimeOfDay;
                var currentExitTime = exitTime.TimeOfDay > EndClock ? EndClock : exitTime.TimeOfDay;
                totalChargeableMinutes = currentExitTime < currentEntryTime ? 0d : (currentExitTime - currentEntryTime).TotalMinutes;
            }

            return totalChargeableMinutes;
        }

        public double GetChargeableTimeEntryDay(DateTime entryTime)
        {
            if (entryTime.IsFreeParkingHoursForParkDate())
            {
                return 0d;
            }
            else
            {
                //Since Parked before startClock,for more than a day so full day fee
                return entryTime.TimeOfDay < StartClock ? FullDayParkingInMinutes : (EndClock - entryTime.TimeOfDay).TotalMinutes;
            }
        }

        public double GetChargeableTimeExitDay(DateTime exitTime)
        {
            if (exitTime.IsFreeParkingHoursForLeaveDate())
            {
                return 0d;
            }
            else
            {
                //Since Exited after endClock for overnight parking so full day fee
                return exitTime.TimeOfDay > EndClock ? FullDayParkingInMinutes : (exitTime.TimeOfDay - StartClock).TotalMinutes;
            }
        }

        public double GetChargeableTimeForFullDays(DateTime entryTime, DateTime exitTime)
        {
            var days = GetChargeableMiddleDays(entryTime, exitTime);
            return days * FullDayParkingInMinutes;
        }

        public int GetChargeableMiddleDays(DateTime from, DateTime end)
        {
            var totalDays = 0;
            //We need the middle non-weekend Full days between Parked Day and Exit Day
            for (var date = from.AddDays(1); date < end.AddDays(-1); date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                    totalDays++;
            }

            return totalDays;
        }
    }
}