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
    public class PeriodCollectionTest
    {
        private DateTime Start0 { get; set; }
        private DateTime End0 { get; set; }
        private DateTime Start1 { get; set; }
        private DateTime End1 { get; set; }
        private PeriodCollection Collection { get; set; }

        private DateTime OtherStart0 { get; set; }
        private DateTime OtherEnd0 { get; set; }
        private DateTime OtherStart1 { get; set; }
        private DateTime OtherEnd1 { get; set; }
        private PeriodCollection OtherCollection { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            // Sample set 1
            Start0 = new DateTime(2014, 12, 28);
            End0 = new DateTime(2014, 12, 29);
            Start1 = new DateTime(2015, 11, 23);
            End1 = new DateTime(2016, 12, 1);
            var items = new[] 
            { 
                new StubIPeriod{ StartGet = () => Start0, EndGet = () => End0 },
                new StubIPeriod{ StartGet = () => Start1, EndGet = () => End1 },
            };

            Collection = new PeriodCollection(items);

            // Sample set 2
            OtherStart0 = new DateTime(2014, 11, 23);
            OtherEnd0 = new DateTime(2015, 2, 4);
            OtherStart1 = new DateTime(2013, 12, 31);
            OtherEnd1 = new DateTime(2014, 1, 1);
            var otherItems = new[] 
            { 
                new StubIPeriod{ StartGet = () => OtherStart0, EndGet = () => OtherEnd0 },
                new StubIPeriod{ StartGet = () => OtherStart1, EndGet = () => OtherEnd1 },
            };

            OtherCollection = new PeriodCollection(otherItems);
        }

        [TestMethod]
        public void Can_create_PeriodCollection_using_copy_constructor()
        {
            var collection = Collection.ToArray();

            Assert.AreEqual(collection[0].Start, Start0);
            Assert.AreEqual(collection[0].End, End0);
            Assert.AreEqual(collection[1].Start, Start1);
            Assert.AreEqual(collection[1].End, End1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Copy_constructor_throws_an_exception_if_the_argument_is_null()
        {
            new PeriodCollection((IEnumerable<IPeriod>)null);
        }

        [TestMethod]
        public void Can_create_PeriodCollection_using_generic_constructor()
        {
            var collection = new PeriodCollection();
            Assert.AreEqual(collection.Count(), 0);
        }

        [TestMethod]
        public void IsEmpty_returns_true_if_the_collection_is_empty()
        {
            var collection = new PeriodCollection();
            Assert.IsTrue(collection.IsEmpty);
        }

        [TestMethod]
        public void IsEmpty_returns_false_if_the_collection_is_not_empty()
        {
            Assert.IsFalse(Collection.IsEmpty);
        }

        [TestMethod]
        public void Length_returns_the_sum_of_the_lengths_of_the_periods()
        {
            Assert.AreEqual(Collection.Length, (End0 - Start0) + (End1 - Start1));
        }

        [TestMethod]
        public void Length_returns_zero_timespan_if_empty_period_collection()
        {
            var collection = new PeriodCollection();
            Assert.AreEqual(collection.Length, TimeSpan.Zero);
        }

        [TestMethod]
        public void GetSet_returns_set_of_periods()
        {
            var period = new StubIPeriod
            {
                StartGet = () => Start0,
                EndGet = () => End0
            };

            var items = new[]
            { 
                period, 
                period,
            };

            var collection = new PeriodCollection(items);
            var result = collection.GetSet();

            Assert.AreEqual(result.Count(), 1);
            Assert.AreEqual(result.Single().Start, Start0);
            Assert.AreEqual(result.Single().End, End0);
        }

        [TestMethod]
        public void IsOverlappingWith_returns_true_if_the_periods_are_overlapping()
        {
            Assert.IsTrue(Collection.IsOverlappingWith(OtherCollection));
        }

        [TestMethod]
        public void IsOverlappingWith_returns_false_if_the_periods_are_not_overlapping()
        {
            var otherStart0 = new DateTime(2001, 11, 23);
            var otherEnd0 = new DateTime(2002, 2, 4);
            var otherStart1 = new DateTime(2003, 12, 31);
            var otherEnd1 = new DateTime(2004, 1, 1);
            var otherItems = new[] 
            { 
                new StubIPeriod{ StartGet = () => otherStart0, EndGet = () => otherEnd0 },
                new StubIPeriod{ StartGet = () => otherStart1, EndGet = () => otherEnd1 },
            };

            var otherCollection = new PeriodCollection(otherItems);

            Assert.IsFalse(Collection.IsOverlappingWith(otherCollection));
        }

        [TestMethod]
        public void IsOverlappingWith_returns_false_if_the_argument_collection_is_empty()
        {
            var emptyCollection = new PeriodCollection();
            Assert.IsFalse(Collection.IsOverlappingWith(emptyCollection));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsOverlappingWith_throws_an_exception_if_the_argument_is_null()
        {
            Collection.IsOverlappingWith((IEnumerable<IPeriod>)null);
        }

        [TestMethod]
        public void GetDifference_get_the_difference_between_two_PeriodCollections()
        {
            // 0
            var collection0 = new PeriodCollection(new[] 
            { 
                 new StubIPeriod{ StartGet = () => new DateTime(2001, 12, 28), EndGet = () => new DateTime(2024, 12, 28) },
            });

            Assert.AreEqual(Collection.GetDifference(collection0).Count(), 0);

            // 1
            var collection1 = new PeriodCollection(new[] 
            { 
                 new StubIPeriod{ StartGet = () => Start0, EndGet = () => End0 },
            });

            var difference1 = Collection.GetDifference(collection1);
            Assert.AreEqual(difference1.Count(), 1);
            Assert.AreEqual(difference1.Single().Start, Start1);
            Assert.AreEqual(difference1.Single().End, End1);

            // 2
            var collection2 = new PeriodCollection(new[] 
            { 
                 new StubIPeriod{ StartGet = () => new DateTime(2015, 11, 24), EndGet = () => new DateTime(2015, 11, 25) },
            });

            var difference2 = Collection.GetDifference(collection2);
            Assert.AreEqual(difference2.Count(), 3);
            Assert.IsTrue(difference2.Any(p => p.Start == Start0 && p.End == End0));
            Assert.IsTrue(difference2.Any(p => p.Start == Start1 && p.End == new DateTime(2015, 11, 24)));
            Assert.IsTrue(difference2.Any(p => p.Start == new DateTime(2015, 11, 25) && p.End == End1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetDifference_throws_an_exception_if_the_argument_is_null()
        {
            Collection.GetDifference((IEnumerable<IPeriod>)null);
        }


        [TestMethod]
        public void GetIntersection_get_the_intersection_between_two_PeriodCollections()
        {
            // 0
            var collection0 = new PeriodCollection(new[] 
            { 
                 new StubIPeriod{ StartGet = () => new DateTime(2015, 11, 24), EndGet = () => new DateTime(2015, 11, 25) },
            });

            var intersection0 = Collection.GetIntersection(collection0);
            Assert.AreEqual(intersection0.Count(), 1);
            Assert.AreEqual(intersection0.Single().Start, new DateTime(2015, 11, 24));
            Assert.AreEqual(intersection0.Single().End, new DateTime(2015, 11, 25));

            // 1
            var collection1 = new PeriodCollection(new[] 
            { 
                 new StubIPeriod{ StartGet = () => new DateTime(2001, 11, 24), EndGet = () => new DateTime(2021, 11, 25) },
            });
            var intersection1 = Collection.GetIntersection(collection1);
            Assert.AreEqual(intersection1.Count(), 2);
            Assert.IsTrue(intersection1.Any(p => p.Start == Start0 && p.End == End0));
            Assert.IsTrue(intersection1.Any(p => p.Start == Start1 && p.End == End1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetIntersection_throws_an_exception_if_the_argument_is_null()
        {
            Collection.GetIntersection((IEnumerable<IPeriod>)null);
        }

        [TestMethod]
        public void Equals_returns_false_if_the_other_object_is_not_a_period_collection()
        {
            Assert.IsFalse(Collection.Equals(new object()));
        }

        [TestMethod]
        public void Equals_returns_false_if_the_other_object_is_null()
        {
            Assert.IsFalse(Collection.Equals((object)null));
        }

        [TestMethod]
        public void Equals_period_collection_returns_true_for_equal_period_collections()
        {
            var collection0 = new PeriodCollection(Collection.ToArray());
            var collection1 = new PeriodCollection(Collection.ToArray());

            Assert.IsTrue(collection0.Equals(collection1));
        }

        [TestMethod]
        public void Equals_period_collection_returns_false_for_not_equal_period_collections()
        {
            Assert.IsFalse(Collection.Equals(new PeriodCollection()));
            Assert.IsFalse(Collection.Equals(OtherCollection));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Equals_period_collection_throws_an_exception_if_the_argument_is_null()
        {
            Collection.Equals(null);
        }
        [TestMethod]
        public void Equals_operator_returns_true_if_the_period_collections_are_equal()
        {
            var collection0 = new PeriodCollection(Collection.ToArray());
            var collection1 = new PeriodCollection(Collection.ToArray());
            Assert.IsTrue(collection0 == collection1);
        }

        [TestMethod]
        public void Equals_operator_returns_false_if_the_period_collections_are_not_equal()
        {
            Assert.IsFalse(Collection == OtherCollection);
        }

        [TestMethod]
        public void Not_equal_operator_returns_false_if_the_period_collections_are_equal()
        {
            var collection0 = new PeriodCollection(Collection.ToArray());
            var collection1 = new PeriodCollection(Collection.ToArray());
            Assert.IsFalse(collection0 != collection1);
        }

        [TestMethod]
        public void Not_equal_operator_returns_true_if_the_period_collections_are_not_equal()
        {
            Assert.IsTrue(Collection != OtherCollection);
        }

        [TestMethod]
        public void GetHashCode_returns_same_value_for_period_collections_that_are_equal()
        {
            var collection0 = new PeriodCollection(Collection.ToArray());
            var collection1 = new PeriodCollection(Collection.ToArray());

            Assert.AreEqual(collection0.GetHashCode(), collection1.GetHashCode());
        }

        [TestMethod]
        public void ToString_returns_correctly_formated_period_collection()
        {
            CultureUtil.EnsureCulture();
            var collectionString = Collection.ToString();

            Assert.AreEqual(collectionString, "28.12.2014 г. 00:00 ч. - 29.12.2014 г. 00:00 ч., 23.11.2015 г. 00:00 ч. - 1.12.2016 г. 00:00 ч.");
        }
    }
}
