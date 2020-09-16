using ParkingManagement.Extension;
using System;

namespace ParkingManagement
{
    public class ChargeableTimeCalculator
    {
        private static readonly TimeSpan startClock = new TimeSpan(8, 0, 0);
        private static readonly TimeSpan endClock = new TimeSpan(18, 0, 0);
        private const int FullDayParkingInMins = 10 * 60;

        public double GetchargeableMins(DateTime entryTime, DateTime exitTime)
        {
            double totalChargeableMins = 0d;

            // Car is Parked for over a day
            if (exitTime.Date > entryTime.Date)
            {
                double chargeableTimeEntryDay = GetChargeableTimeEntryDay(entryTime);
                double chargeableTimeForEntireDays = GetChargeableTimeForFullDays(entryTime, exitTime);
                double chargeableTimeExitDay = GetChargeableTimeExitDay(exitTime);

                totalChargeableMins = chargeableTimeEntryDay + chargeableTimeForEntireDays + chargeableTimeExitDay;
            }
            else
            {
                TimeSpan currentEntryTime = entryTime.TimeOfDay < startClock ? startClock : entryTime.TimeOfDay;
                TimeSpan currentExitTime = exitTime.TimeOfDay > endClock ? endClock : exitTime.TimeOfDay;
                totalChargeableMins = currentExitTime < currentEntryTime ? 0d : (currentExitTime - currentEntryTime).TotalMinutes;
            }

            return totalChargeableMins;
        }

        private double GetChargeableTimeEntryDay(DateTime entryTime)
        {
            if (entryTime.IsFreeParkingHoursForParkDate())
            {
                return 0d;
            }
            else
            {
                //Since Parked before startClock,for more than a day so full day fee
                return entryTime.TimeOfDay < startClock ? FullDayParkingInMins : (endClock - entryTime.TimeOfDay).TotalMinutes;
            }
        }

        private double GetChargeableTimeExitDay(DateTime exitTime)
        {
            if (exitTime.IsFreeParkingHoursForLeaveDate())
            {
                return 0d;
            }
            else
            {
                //Since Exited after endClock for overnight parking so full day fee
                return exitTime.TimeOfDay > endClock ? FullDayParkingInMins : (exitTime.TimeOfDay - startClock).TotalMinutes;
            }
        }

        private double GetChargeableTimeForFullDays(DateTime entryTime, DateTime exitTime)
        {
            int days = GetChrgeableMiddleDays(entryTime, exitTime);
            return days * FullDayParkingInMins;
        }

        public int GetChrgeableMiddleDays(DateTime from, DateTime end)
        {
            int totalDays = 0;
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