using NUnit.Framework;
using System;

namespace ParkingManagement.Tests
{
    [TestFixture]
    public class ChargeableTimeCalculatorTest
    {
        private ChargeableTimeCalculator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new ChargeableTimeCalculator();
        }

        [TestCase("09/07/2017 16:50:00", "09/09/2017 19:15:00", 1)]
        [TestCase("09/07/2017 16:50:00", "09/09/2017 19:15:00", 1)]
        [TestCase("09/15/2020 16:50:00", "09/20/2020 19:15:00", 3)]
        [TestCase("09/15/2020 16:50:00", "09/15/2020 19:16:00", 0)]
        [TestCase("09/15/2020 16:50:00", "09/16/2020 19:16:00", 0)]
        [TestCase("09/15/2020 16:50:00", "09/17/2020 19:16:00", 1)]
        public void GetChrgeableMidDaysTest(DateTime entryDate, DateTime exitDate, int expected)
        {
            int res = _sut.GetChrgeableMiddleDays(entryDate, exitDate);
            Assert.AreEqual(expected, res);
        }
    }
}