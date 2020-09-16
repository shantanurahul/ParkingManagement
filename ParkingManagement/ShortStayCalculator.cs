using System;

namespace ParkingManagement
{
    public class ShortStayCalculator
    {
        private const decimal PerMinCharge = (decimal)(1.10 / 60);
        private readonly ChargeableTimeCalculator chargeableTimeCalcuator = new ChargeableTimeCalculator();

        public decimal ParkingCharge(DateTime parkingDateTime, DateTime exitDateTime)
        {
            if (!(parkingDateTime > exitDateTime))
            {
                double chargeableMins = chargeableTimeCalcuator.GetchargeableMins(parkingDateTime, exitDateTime);
                decimal totalCharge = Convert.ToDecimal(chargeableMins) * PerMinCharge;
                return decimal.Round(totalCharge, 2);
            }

            throw new ArgumentException("Entry Date cannot be greater than Exit date");
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