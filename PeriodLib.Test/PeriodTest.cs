using System;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeriodLib.Fakes;
using System.Fakes;
using System.Collections.Generic;
using System.Threading;
using System.Globalization;

namespace PeriodLib.Test
{
    [TestClass]
    public class PeriodTest
    {
        [TestMethod]
        public void Can_be_created_using_start_and_end_time()
        {
            var start = new DateTime(2014, 12, 28, 17, 16, 30);
            var end = new DateTime(2015, 11, 29, 18, 14, 12);
            var period = new Period(start, end);

            Assert.AreEqual(period.Start, start);
            Assert.AreEqual(period.End, end);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Throws_an_exception_if_end_is_less_than_start()
        {
            var start = new DateTime(2015, 11, 29, 18, 14, 12);
            var end = new DateTime(2014, 12, 28, 17, 16, 30);
            new Period(start, end);
        }

        [TestMethod]
        public void Can_be_created_using_copy_constructor()
        {
            var start = new DateTime(2014, 12, 28, 17, 16, 30);
            var end = new DateTime(2015, 11, 29, 18, 14, 12);

            var iPeriod = new StubIPeriod
            {
                StartGet = () => start,
                EndGet = () => end,
            };

            var period = new Period(iPeriod);

            Assert.AreEqual(period.Start, start);
            Assert.AreEqual(period.End, end);
        }


        [TestMethod]
        public void Can_create_period_using_generic_constructor()
        {
            var period = new Period();
            Assert.AreEqual(period.Start, DateTime.MinValue);
            Assert.AreEqual(period.EndDate, DateTime.MinValue);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Copy_constructor_throws_exception_if_other_argument_is_null()
        {
            new Period(null);
        }

        [TestMethod]
        public void Returns_the_correct_ThisDay()
        {
            using (ShimsContext.Create())
            {
                var time = new DateTime(2014, 12, 28, 17, 16, 30);
                var start = new DateTime(2014, 12, 28);
                var end = new DateTime(2014, 12, 29);

                ShimDateTime.NowGet = () => time;

                var period = Period.ThisDay;

                Assert.AreEqual(period.Start, start);
                Assert.AreEqual(period.End, end);
            }
        }

        [TestMethod]
        public void Returns_the_correct_ThisWeek()
        {
            using (ShimsContext.Create())
            {
                var time = new DateTime(2014, 12, 26, 17, 16, 30);
                var start = new DateTime(2014, 12, 22);
                var end = new DateTime(2014, 12, 29);

                ShimDateTime.NowGet = () => time;

                var period = Period.ThisWeek;

                Assert.AreEqual(period.Start, start);
                Assert.AreEqual(period.End, end);
            }
        }

        [TestMethod]
        public void Returns_the_correct_ThisMonth()
        {
            using (ShimsContext.Create())
            {
                var time = new DateTime(2014, 12, 28, 17, 16, 30);
                var start = new DateTime(2014, 12, 1);
                var end = new DateTime(2015, 1, 1);

                ShimDateTime.NowGet = () => time;

                var period = Period.ThisMonth;

                Assert.AreEqual(period.Start, start);
                Assert.AreEqual(period.End, end);
            }
        }

        [TestMethod]
        public void Returns_the_correct_ThisYear()
        {
            using (ShimsContext.Create())
            {
                var time = new DateTime(2014, 12, 28, 17, 16, 30);
                var start = new DateTime(2014, 1, 1);
                var end = new DateTime(2015, 1, 1);

                ShimDateTime.NowGet = () => time;

                var period = Period.ThisYear;

                Assert.AreEqual(period.Start, start);
                Assert.AreEqual(period.End, end);
            }
        }

        [TestMethod]
        public void Returns_the_correct_StartDate()
        {
            var start = new DateTime(2014, 12, 28, 17, 16, 30);
            var end = new DateTime(2015, 11, 29, 18, 14, 12);
            var period = new Period(start, end);

            Assert.AreEqual(period.StartDate, new DateTime(2014, 12, 28));
        }

        [TestMethod]
        public void Returns_the_correct_EndDate()
        {
            var start = new DateTime(2014, 12, 28, 17, 16, 30);
            var end = new DateTime(2015, 11, 29, 18, 14, 12);
            var period = new Period(start, end);

            Assert.AreEqual(period.EndDate, new DateTime(2015, 11, 29));
        }

        [TestMethod]
        public void Length_returns_the_correct_length()
        {
            var start = new DateTime(2014, 12, 28, 17, 16, 30);
            var end = new DateTime(2015, 11, 29, 18, 14, 12);
            var period = new Period(start, end);

            Assert.AreEqual(period.Length, end - start);
        }

        [TestMethod]
        public void IsOverlappingWith_time_method_returns_true_if_the_periods_are_overlapping()
        {
            var start = new DateTime(2014, 12, 28, 17, 16, 30);
            var end = new DateTime(2015, 11, 29, 18, 14, 12);
            var period = new Period(start, end);

            Assert.IsTrue(period.IsOverlappingWith(new DateTime(2014, 12, 29)));
            Assert.IsTrue(period.IsOverlappingWith(start));
        }

        [TestMethod]
        public void IsOverlappingWith_time_method_returns_false_if_the_periods_are_not_overlapping()
        {
            var start = new DateTime(2014, 12, 28, 17, 16, 30);
            var end = new DateTime(2015, 11, 29, 18, 14, 12);
            var period = new Period(start, end);

            Assert.IsFalse(period.IsOverlappingWith(new DateTime(2016, 12, 29)));
            Assert.IsFalse(period.IsOverlappingWith(new DateTime(2012, 12, 29)));
            Assert.IsFalse(period.IsOverlappingWith(end));
        }

        [TestMethod]
        public void IsOverlappingWith_method_returns_true_if_the_periods_are_overlapping()
        {
            var period0 = new Period(new DateTime(2014, 12, 24), new DateTime(2015, 11, 27));
            var period1 = new Period(new DateTime(2014, 12, 23), new DateTime(2015, 11, 28));
            var period2 = new Period(new DateTime(2014, 12, 23), new DateTime(2015, 11, 25));
            var period3 = new Period(new DateTime(2014, 12, 25), new DateTime(2015, 11, 29));
            var period4 = new Period(new DateTime(2014, 12, 25), new DateTime(2015, 11, 26));

            Assert.IsTrue(period0.IsOverlappingWith(period0));
            Assert.IsTrue(period0.IsOverlappingWith(period1));
            Assert.IsTrue(period0.IsOverlappingWith(period2));
            Assert.IsTrue(period0.IsOverlappingWith(period3));
            Assert.IsTrue(period0.IsOverlappingWith(period4));
        }

        [TestMethod]
        public void IsOverlappingWith_method_returns_false_if_the_periods_are_not_overlapping()
        {
            var period0 = new Period(new DateTime(2014, 12, 24), new DateTime(2015, 11, 27));
            var period1 = new Period(new DateTime(2014, 11, 10), new DateTime(2015, 12, 23));
            var period2 = new Period(new DateTime(2014, 11, 10), new DateTime(2015, 12, 24));
            var period3 = new Period(new DateTime(2015, 11, 27), new DateTime(2015, 12, 1));
            var period4 = new Period(new DateTime(2015, 11, 28), new DateTime(2015, 12, 1));

            Assert.IsFalse(period0.IsOverlappingWith(period1));
            Assert.IsFalse(period0.IsOverlappingWith(period2));
            Assert.IsFalse(period0.IsOverlappingWith(period3));
            Assert.IsFalse(period0.IsOverlappingWith(period4));

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsOverlappingWith_method_throws_an_exception_if_the_argument_is_null()
        {
            var period = new Period(new DateTime(2014, 12, 24), new DateTime(2015, 11, 27));
            period.IsOverlappingWith((Period)null);
        }

        [TestMethod]
        public void IsOverlappingWith_periods_collection_method_returns_true_if_the_periods_are_overlapping()
        {
            var period = new Period(new DateTime(2014, 12, 24), new DateTime(2015, 11, 27));
            var collection = new[]
            {
              new Period(new DateTime(2014, 12, 23), new DateTime(2015, 11, 28)),
              new Period(new DateTime(2014, 11, 10), new DateTime(2015, 12, 24)),
            };

            Assert.IsTrue(period.IsOverlappingWith(collection));
        }

        [TestMethod]
        public void IsOverlappingWith_periods_collection_method_returns_false_if_the_periods_are_not_overlapping()
        {
            var period = new Period(new DateTime(2014, 12, 24), new DateTime(2015, 11, 27));
            var collection = new[] {
            
             new Period(new DateTime(2014, 11, 10), new DateTime(2015, 12, 23)),
             new Period(new DateTime(2014, 11, 10), new DateTime(2015, 12, 24)),
             new Period(new DateTime(2015, 11, 27), new DateTime(2015, 12, 1)),
             new Period(new DateTime(2015, 11, 28), new DateTime(2015, 12, 1)),
            };

            Assert.IsFalse(period.IsOverlappingWith(collection));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsOverlappingWith_periods_collection_method_throws_an_exception_if_the_argument_is_null()
        {
            var period = new Period(new DateTime(2014, 12, 24), new DateTime(2015, 11, 27));
            period.IsOverlappingWith((IEnumerable<IPeriod>)null);
        }

        [TestMethod]
        public void Equals_returns_false_if_the_other_object_is_not_period()
        {
            var period = new Period(new DateTime(2014, 12, 24), new DateTime(2015, 11, 27));
            Assert.IsFalse(period.Equals(new object()));
        }

        [TestMethod]
        public void Equals_returns_false_if_the_other_object_is_null()
        {
            var period = new Period(new DateTime(2014, 12, 24), new DateTime(2015, 11, 27));
            Assert.IsFalse(period.Equals((object)null));
        }

        [TestMethod]
        public void Equals_period_returns_true_for_equal_periods()
        {
            var period0 = new Period(new DateTime(2014, 12, 24), new DateTime(2015, 11, 27));
            var period1 = new Period(new DateTime(2014, 12, 24), new DateTime(2015, 11, 27));

            Assert.IsTrue(period0.Equals(period1));
        }

        [TestMethod]
        public void Equals_period_returns_false_for_not_equal_periods()
        {
            var period0 = new Period(new DateTime(2014, 12, 24), new DateTime(2015, 11, 27));
            var period1 = new Period(new DateTime(2014, 12, 23), new DateTime(2015, 11, 27));

            Assert.IsFalse(period0.Equals(period1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Equals_period_throws_an_exception_if_the_argument_is_null()
        {
            var period = new Period(new DateTime(2014, 12, 24), new DateTime(2015, 11, 27));
            period.Equals((IPeriod)null);
        }

        [TestMethod]
        public void Equals_operator_returns_true_if_the_periods_are_equal()
        {
            var period0 = new Period(new DateTime(2014, 12, 24), new DateTime(2015, 11, 27));
            var period1 = new Period(new DateTime(2014, 12, 24), new DateTime(2015, 11, 27));

            Assert.IsTrue(period0 == period1);
        }

        [TestMethod]
        public void Equals_operator_returns_false_if_the_periods_are_not_equal()
        {
            var period0 = new Period(new DateTime(2014, 12, 24), new DateTime(2015, 11, 27));
            var period1 = new Period(new DateTime(2014, 11, 24), new DateTime(2015, 11, 27));

            Assert.IsFalse(period0 == period1);
        }

        [TestMethod]
        public void Not_equal_operator_returns_false_if_the_periods_are_equal()
        {
            var period0 = new Period(new DateTime(2014, 12, 24), new DateTime(2015, 11, 27));
            var period1 = new Period(new DateTime(2014, 12, 24), new DateTime(2015, 11, 27));

            Assert.IsFalse(period0 != period1);
        }

        [TestMethod]
        public void Not_equal_operator_returns_true_if_the_periods_are_not_equal()
        {
            var period0 = new Period(new DateTime(2014, 12, 24), new DateTime(2015, 11, 27));
            var period1 = new Period(new DateTime(2014, 11, 24), new DateTime(2015, 11, 27));

            Assert.IsTrue(period0 != period1);
        }

        [TestMethod]
        public void GetHashCode_returns_same_value_for_periods_that_are_equal()
        {
            var period0 = new Period(new DateTime(2014, 12, 24), new DateTime(2015, 11, 27));
            var period1 = new Period(new DateTime(2014, 12, 24), new DateTime(2015, 11, 27));

            Assert.AreEqual(period0.GetHashCode(), period1.GetHashCode());
        }

        [TestMethod]
        public void ToString_returns_correctly_formated_period()
        {
            CultureUtil.EnsureCulture();
            var start = new DateTime(2014, 12, 28, 17, 16, 30);
            var end = new DateTime(2015, 11, 29, 18, 14, 12);
            var period = new Period(start, end);
            var periodString = period.ToString();

            Assert.AreEqual(periodString, "28.12.2014 17:16:30 - 29.11.2015 18:14:12");
        }

        [TestMethod]
        public void ToShortDateString_returns_correctly_formated_date_period()
        {
            CultureUtil.EnsureCulture();
            var start = new DateTime(2014, 12, 28, 17, 16, 30);
            var end = new DateTime(2015, 11, 29, 18, 14, 12);
            var period = new Period(start, end);
            var periodString = period.ToShortDateString();

            Assert.AreEqual(periodString, "28.12.2014 - 29.11.2015");
        }
    }
}
