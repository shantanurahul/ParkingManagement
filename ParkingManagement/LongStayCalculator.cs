using System;

namespace ParkingManagement
{
    public class LongStayCalculator
    {
        private const decimal PerDayParkingFee = 7.50m;

        public decimal ParkingCharge(DateTime entryTime, DateTime exitTime)
        {
            if (!(entryTime > exitTime))
            {
                double chargeableDays = GetChargeableDays(entryTime, exitTime);
                decimal totalCharge = Convert.ToDecimal(chargeableDays) * PerDayParkingFee;
                return decimal.Round(totalCharge, 2);
            }

            throw new ArgumentException("Entry Date cannot be greater than Exit date");
        }

        private double GetChargeableDays(DateTime entryTime, DateTime exitTime)
        {
            // Car is Parked for over a day
            if (exitTime.Date > entryTime.Date)
            {
                // Rounding up to whole days adding a day
                return Math.Ceiling((exitTime - entryTime).TotalDays + 1d);
            }
            else
            {
                return 1d;
            }
        }
    }
}