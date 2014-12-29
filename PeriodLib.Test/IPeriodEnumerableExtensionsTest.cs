using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeriodLib.Fakes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodLib.Test
{
    [TestClass]
    public class IPeriodEnumerableExtensionsTest
    {
        [TestMethod]
        public void ToCollection_converts_corretly_IEnumerable_of_periods_to_PeriodCollection()
        {
            var start0 = new DateTime(2014, 12, 28);
            var end0 = new DateTime(2014, 12, 29);
            var start1 = new DateTime(2013, 11, 23);
            var end1 = new DateTime(2013, 12, 1);

            var items = new[] 
            { 
                new StubIPeriod{ StartGet = () => start0, EndGet = () => end0 },
                new StubIPeriod{ StartGet = () => start1, EndGet = () => end1 },
            };

            var collection = items.ToCollection().ToArray();

            Assert.AreEqual(start0, collection[0].Start);
            Assert.AreEqual(end0, collection[0].End);
            Assert.IsNull(collection[0].Data);
            Assert.AreEqual(start1, collection[1].Start);
            Assert.AreEqual(end1, collection[1].End);
            Assert.IsNull(collection[1].Data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToCollection_throws_an_exception_if_the_argument_is_null()
        {
            IEnumerable<IPeriod> items = null;
            items.ToCollection();
        }

        [TestMethod]
        public void It_should_return_the_same_reference_if_the_IPeriodEnumerable_is_already_a_PeriodCollection_object()
        {
            var start0 = new DateTime(2014, 12, 28);
            var end0 = new DateTime(2014, 12, 29);
            var start1 = new DateTime(2013, 11, 23);
            var end1 = new DateTime(2012, 12, 1);

            var items = new[] 
            { 
                new StubIPeriod{ StartGet = () => start0, EndGet = () => end0 },
                new StubIPeriod{ StartGet = () => start1, EndGet = () => end1 },
            };

            var collection0 = new PeriodCollection();
            var collection1 = collection0.ToCollection();

            Assert.ReferenceEquals(collection0, collection1);
        }
    }
}
