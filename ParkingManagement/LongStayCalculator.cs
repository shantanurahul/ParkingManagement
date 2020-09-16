using System;

namespace ParkingManagement
{
    public class LongStayCalculator
    {
        private const decimal PerDayParkingFee = 7.50m;

        public decimal ParkingCharge(DateTime parkingDateTime, DateTime exitDateTime)
        {
            if (!(parkingDateTime > exitDateTime))
            {
                var chargeableDays = GetChargeableDays(parkingDateTime, exitDateTime);
                var totalCharge = Convert.ToDecimal(chargeableDays) * PerDayParkingFee;
                return decimal.Round(totalCharge, 2);
            }

            throw new ArgumentException("Entry Date cannot be greater than Exit date");
        }

        private double GetChargeableDays(DateTime parkingDateTime, DateTime exitDateTime)
        {
            // Car is Parked for over a day
            if (exitDateTime.Date > parkingDateTime.Date)
            {
                // Rounding up to whole days adding a day
                return Math.Ceiling((exitDateTime - parkingDateTime).TotalDays + 1d);
            }
            else
            {
                return 1d;
            }
        }
    }
}