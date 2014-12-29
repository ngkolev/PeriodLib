using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodLib
{
    /// <summary>
    /// This class represents collection of periods
    /// </summary>
    public sealed class PeriodCollection : IEnumerable<IPeriod>, IEquatable<PeriodCollection>
    {
        /// <summary>
        /// Creates new period collection
        /// </summary>
        /// <param name="period"></param>
        /// <exception cref="ArgumentNullException">Thrown if the argument is null</exception>
        public PeriodCollection(IEnumerable<IPeriod> period)
        {
            if (period == null)
            {
                throw new ArgumentNullException("period");
            }

            Periods = period.Select(p => p.GetPeriod()).ToList();
        }

        /// <summary>
        /// Creates empty Period collection
        /// </summary>
        public PeriodCollection()
        {
            Periods = new List<Period>();
        }

        private List<Period> Periods { get; set; }

        /// <summary>
        /// True if this collection is empty
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return !Periods.Any();
            }
        }

        /// <summary>
        /// Sum of all periods' lengths
        /// </summary>
        public TimeSpan Length
        {
            get
            {
                var lengthInTicks = Periods.Sum(p => p.Length.Ticks);
                return new TimeSpan(lengthInTicks);
            }
        }

        public static bool operator ==(PeriodCollection left, PeriodCollection right)
        {
            if (Object.ReferenceEquals(left, right))
            {
                return true;
            }
            if (Object.ReferenceEquals(left, null) || Object.ReferenceEquals(right, null))
            {
                return false;
            }
            else
            {
                return left.Equals(right);
            }
        }

        public static bool operator !=(PeriodCollection left, PeriodCollection right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Gets PeriodCollection from current without period repetition
        /// </summary>
        /// <returns>Set of periods</returns>
        public PeriodCollection GetSet()
        {
            var items = this.Distinct();
            return new PeriodCollection(items);
        }

        /// <summary>
        /// Checks if one PeriodColleciton is overlapping with other PeriodCollection
        /// </summary>
        /// <param name="periods">List of periods</param>
        /// <returns>True if there is overlapping</returns>
        /// <exception cref="ArgumentNullException">Thrown if the argument is null</exception>
        public bool IsOverlappingWith(IEnumerable<IPeriod> periods)
        {
            return Periods.Any(p => p.IsOverlappingWith(periods));
        }

        /// <summary>
        /// Return the difference between this period collection and the argument
        /// </summary>
        /// <param name="periods">The period collection argument</param>
        /// <returns>The new collection</returns>
        /// <exception cref="ArgumentNullException">Thrown if the argument is null</exception>
        public PeriodCollection GetDifference(IEnumerable<IPeriod> periods)
        {
            if (periods == null)
            {
                throw new ArgumentNullException("periods");
            }

            var resultItems = this.Periods.SelectMany(p => p.GetDifference(periods));

            return new PeriodCollection(resultItems);
        }

        /// <summary>
        /// Return the intersection between this period collection and the argument
        /// </summary>
        /// <param name="periods">The period collection argument</param>
        /// <returns>The new collection</returns>
        /// <exception cref="ArgumentNullException">Thrown if the argument is null</exception>
        public PeriodCollection GetIntersection(IEnumerable<IPeriod> periods)
        {
            if (periods == null)
            {
                throw new ArgumentNullException("periods");
            }

            var resultItems = this.Periods.SelectMany(p => p.GetIntersection(periods));

            return new PeriodCollection(resultItems);
        }

        /// <summary>
        /// Checks if two periods are equal
        /// </summary>
        /// <param name="obj">The other period</param>
        /// <returns>True if the periods are equal</returns>
        public override bool Equals(object obj)
        {
            var other = obj as PeriodCollection;
            return (other != null) ? Equals(other) : false;
        }

        /// <summary>
        /// Checks if two periods are equal
        /// </summary>
        /// <param name="other">The other period</param>
        /// <returns>True if the periods are equal</returns>
        /// <exception cref="ArgumentNullException">Thrown if the argument is null</exception>
        public bool Equals(PeriodCollection other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            var groupsA = Periods.GroupBy(p => p).ToArray();
            var groupsB = other.Select(p => p.GetPeriod()).GroupBy(p => p).ToArray();

            if (groupsA.Length != groupsB.Length)
            {
                return false;
            }

            foreach (var a in groupsA)
            {
                var b = groupsB.SingleOrDefault(i => i.Key == a.Key);
                if (b == null || a.Count() != b.Count())
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Period collection's hash function
        /// </summary>
        /// <returns>Period collection's hash</returns>
        public override int GetHashCode()
        {
            int result = 0;

            foreach (var period in Periods)
            {
                result ^= period.GetHashCode();
            }

            return result;
        }

        /// <summary>
        /// Returns text representation of the period collection
        /// </summary>
        /// <returns>Text representation of the period collection</returns>
        public override string ToString()
        {
            return String.Join(", ", Periods);
        }

        /// <summary>
        /// Returns period collection enumerator
        /// </summary>
        /// <returns>Period collection enumerator</returns>
        public IEnumerator<IPeriod> GetEnumerator()
        {
            return Periods.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Periods.GetEnumerator();
        }
    }
}
