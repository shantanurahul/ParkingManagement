using System;

namespace ParkingManagement
{
    internal class Program
    {
        private static void Main()
        {
            new ShortStayCalculator().ParkingCharge(new DateTime(), new DateTime());
            Console.WriteLine("Hello World!");
        }
    }
}