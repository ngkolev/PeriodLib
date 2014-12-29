using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeriodLib.Fakes;
using System;
using System.Collections.Generic;
using System.Fakes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodLib.Test
{
    [TestClass]
    public class DateTimeExtensionsTest
    {
        [TestMethod]
        public void GetDayPeriod_returns_correct_day_interval()
        {
            var time = new DateTime(2014, 12, 28, 17, 16, 30);
            var start = new DateTime(2014, 12, 28);
            var end = new DateTime(2014, 12, 29);
            var period = time.GetDayPeriod();

            Assert.AreEqual(start, period.Start);
            Assert.AreEqual(end, period.End);
            Assert.IsNull(period.Data);
        }

        [TestMethod]
        public void GetWeekPeriod_returns_correct_week_interval()
        {
            var time = new DateTime(2014, 12, 26, 17, 16, 30);
            var start = new DateTime(2014, 12, 22);
            var end = new DateTime(2014, 12, 29);
            var period = time.GetWeekPeriod();

            Assert.AreEqual(start, period.Start);
            Assert.AreEqual(end, period.End);
        }

        [TestMethod]
        public void GetMonthPeriod_returns_correct_month_interval()
        {
            var time = new DateTime(2014, 12, 28, 17, 16, 30);
            var start = new DateTime(2014, 12, 1);
            var end = new DateTime(2015, 1, 1);
            var period = time.GetMonthPeriod();

            Assert.AreEqual(start, period.Start);
            Assert.AreEqual(end, period.End);
        }

        [TestMethod]
        public void GetYearPeriodreturns_correct_year_interval()
        {
            var time = new DateTime(2014, 12, 28, 17, 16, 30);
            var start = new DateTime(2014, 1, 1);
            var end = new DateTime(2015, 1, 1);
            var period = time.GetYearPeriod();

            Assert.AreEqual(start, period.Start);
            Assert.AreEqual(end, period.End);
        }
    }
}
