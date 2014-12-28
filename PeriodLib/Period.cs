using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodLib
{
    /// <summary>
    /// This class represents Period, i.e., something that has start and end time
    /// </summary>
    [DebuggerDisplay("{Start} - {End}")]
    public sealed class Period : IEquatable<Period>, PeriodLib.IPeriod
    {
        /// <summary>
        /// Create new Period
        /// </summary>
        /// <param name="start">Period's start time</param>
        /// <param name="end">Period's end time</param>
        public Period(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create new period
        /// </summary>
        /// <param name="other">Other period-like object</param>
        /// <exception cref="ArgumentNullException">Thrown when the argument is null</exception>
        public Period(IPeriod other)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create new period. Start and end will be equal to DateTime.Min
        /// </summary>
        public Period()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets today interval
        /// </summary>
        public static Period ThisDay
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets this week interval
        /// </summary>
        public static Period ThisWeek
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets this month interval
        /// </summary>
        public static Period ThisMonth
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets this year interval
        /// </summary>
        public static Period ThisYear
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Period's start
        /// </summary>
        public DateTime Start { get; private set; }

        /// <summary>
        /// Periods' end
        /// </summary>
        public DateTime End { get; private set; }

        /// <summary>
        /// Date of period's start
        /// </summary>
        public DateTime StartDate
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Date of period's end
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets the length of the period
        /// </summary>
        public TimeSpan Length
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Checks if time is overlapping with this period, i.e. if it is in this period
        /// </summary>
        /// <param name="other">The time argument</param>
        /// <returns>True if the time is inside the interval. Otherwise - false</returns>
        public bool IsOverlappingWith(DateTime time)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if this period is overlapping with another period
        /// </summary>
        /// <param name="other">The other period</param>
        /// <returns>True if the two periods overlap. Otherwise - false</returns>
        /// <exception cref="ArgumentNullException">Thrown if the argument is null</exception>
        public bool IsOverlappingWith(Period other)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if this period is overlapping with collection of periods
        /// </summary>
        /// <param name="periods">Collection periods</param>
        /// <returns>True if the period overlaps with any other period. Otherwise - false</returns>
        /// <exception cref="ArgumentNullException">Thrown if the argument is null</exception>
        public bool IsOverlappingWith(IEnumerable<Period> periods)
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
        /// Period's hash function
        /// </summary>
        /// <returns>Period's hash</returns>
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns text representation of the period
        /// </summary>
        /// <returns>Text representation of the period</returns>
        public override string ToString()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns text representation of the period's start and end dates
        /// </summary>
        /// <returns>Text representation of the period's start and end dates</returns>
        public string ToShortDateString()
        {
            throw new NotImplementedException();
        }
    }
}
