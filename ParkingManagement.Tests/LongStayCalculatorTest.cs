using NUnit.Framework;
using System;


namespace ParkingManagement.Tests
{
    [TestFixture]
    public class LongStayCalculatorTest
    {
        private LongStayCalculator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new LongStayCalculator();
        }

        [TestCase("09/07/2017 07:50:00 ", "09/09/2017 05:20:00 ", 22.50)]
        [TestCase("09/07/2017 07:50:00 ", "09/07/2017 07:50:01 ", 7.50)]
        public void GetChrgeableMidDaysTest(DateTime entryDate, DateTime exitDate, decimal expected)
        {
            decimal res = _sut.ParkingCharge(entryDate, exitDate);
            Assert.AreEqual(expected, res);
        }
    }
}
