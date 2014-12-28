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
    public sealed class PeriodCollection : IEnumerable<Period>, IEquatable<Period>
    {
        /// <summary>
        /// Creates new period collection
        /// </summary>
        /// <param name="period"></param>
        /// <exception cref="ArgumentNullException">Thrown if the argument is null</exception>
        public PeriodCollection(IEnumerable<IPeriod> period)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates empty Period collection
        /// </summary>
        public PeriodCollection()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// True if this collection is empty
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Sum of all periods' lengths
        /// </summary>
        public TimeSpan Length
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets PeriodCollection from current without period repetition
        /// </summary>
        /// <returns>Set of periods</returns>
        public PeriodCollection GetSet()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if one PeriodColleciton is overlapping with other PeriodCollection
        /// </summary>
        /// <param name="periods">List of periods</param>
        /// <returns>True if there is overlapping</returns>
        /// <exception cref="ArgumentNullException">Thrown if the argument is null</exception>
        public bool IsOverlappingWith(IEnumerable<IPeriod> periods)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the difference between this period collection and the argument
        /// </summary>
        /// <param name="periods">The period collection argument</param>
        /// <returns>The new collection</returns>
        /// <exception cref="ArgumentNullException">Thrown if the argument is null</exception>
        public PeriodCollection GetDifference(IEnumerable<IPeriod> periods)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the intersection between this period collection and the argument
        /// </summary>
        /// <param name="periods">The period collection argument</param>
        /// <returns>The new collection</returns>
        /// <exception cref="ArgumentNullException">Thrown if the argument is null</exception>
        public PeriodCollection GetIntersection(IEnumerable<IPeriod> periods)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if two periods are equal
        /// </summary>
        /// <param name="obj">The other period</param>
        /// <returns>True if the periods are equal</returns>
        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if two periods are equal
        /// </summary>
        /// <param name="other">The other period</param>
        /// <returns>True if the periods are equal</returns>
        /// <exception cref="ArgumentNullException">Thrown if the argument is null</exception>
        public bool Equals(Period other)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Period collection's hash function
        /// </summary>
        /// <returns>Period collection's hash</returns>
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns text representation of the period collection
        /// </summary>
        /// <returns>Text representation of the period collection</returns>
        public override string ToString()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns period collection enumerator
        /// </summary>
        /// <returns>Period collection enumerator</returns>
        public IEnumerator<Period> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
