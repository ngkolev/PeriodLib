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
    public class IPeriodExtensionsTest
    {
        [TestMethod]
        public void It_should_map_IPeriod_to_period()
        {
            var start = new DateTime(2014, 12, 28, 17, 16, 30);
            var end = new DateTime(2015, 11, 29, 18, 14, 12);

            var iPeriod = new StubIPeriod
            {
                StartGet = () => start,
                EndGet = () => end,
            };

            var period = iPeriod.GetPeriod();

            Assert.AreEqual(start, period.Start);
            Assert.AreEqual(end, period.End);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void It_should_throw_an_exception_if_argument_is_null()
        {
            StubIPeriod iPeriod = null;
            iPeriod.GetPeriod();
        }

        [TestMethod]
        public void It_should_return_the_same_reference_if_the_IPeriod_is_already_a_Period_object()
        {
            var start = new DateTime(2014, 12, 28, 17, 16, 30);
            var end = new DateTime(2015, 11, 29, 18, 14, 12);

            var iPeriod = new Period(start, end);
            var period = iPeriod.GetPeriod();

            Assert.ReferenceEquals(iPeriod, period);
        }
    }
}
