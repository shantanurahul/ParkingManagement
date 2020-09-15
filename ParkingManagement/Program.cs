using System;

namespace ParkingManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            new ShortStayCalculator().ParkingCharge(new DateTime(), new DateTime());
            Console.WriteLine("Hello World!");
        }
    }
}
