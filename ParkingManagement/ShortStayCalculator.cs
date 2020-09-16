using System;

namespace ParkingManagement
{
    public class ShortStayCalculator
    {
        private const decimal PerMinCharge = (decimal) (1.10 / 60);

        //todo DI this
        private readonly ChargeableTimeCalculator _chargeableTimeCalculator = new ChargeableTimeCalculator();

        public decimal ParkingCharge(DateTime parkingDateTime, DateTime exitDateTime)
        {
            if (!(parkingDateTime > exitDateTime))
            {
                var chargeableMinutes = _chargeableTimeCalculator.GetChargeableMinutes(parkingDateTime, exitDateTime);
                var totalCharge = Convert.ToDecimal(chargeableMinutes) * PerMinCharge;
                return decimal.Round(totalCharge, 2);
            }

            throw new ArgumentException("Entry Date cannot be greater than Exit date");
        }
    }
}