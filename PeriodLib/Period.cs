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
    public sealed class Period : IEquatable<IPeriod>, PeriodLib.IPeriod
    {
        /// <summary>
        /// Create new Period
        /// </summary>
        /// <param name="start">Period's start time</param>
        /// <param name="end">Period's end time</param>
        /// <param name="data">Optional data object associated with the time period</param>
        public Period(DateTime start, DateTime end, object data = null)
        {
            SetPeriod(start, end, data);
        }

        /// <summary>
        /// Create new period
        /// </summary>
        /// <param name="other">Other period-like object</param>
        /// <exception cref="ArgumentNullException">Thrown when the argument is null</exception>
        public Period(IPeriod other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            var data = (other is Period) ? ((Period)other).Data : null;

            SetPeriod(other.Start, other.End, data);
        }

        /// <summary>
        /// Create new period. Start and end will be equal to DateTime.Min
        /// </summary>
        public Period()
        {
        }

        /// <summary>
        /// Gets today interval
        /// </summary>
        public static Period ThisDay
        {
            get
            {
                return DateTime.Now.GetDayPeriod();
            }
        }

        /// <summary>
        /// Gets this week interval
        /// </summary>
        public static Period ThisWeek
        {
            get
            {
                return DateTime.Now.GetWeekPeriod();
            }
        }

        /// <summary>
        /// Gets this month interval
        /// </summary>
        public static Period ThisMonth
        {
            get
            {
                return DateTime.Now.GetMonthPeriod();
            }
        }

        /// <summary>
        /// Gets this year interval
        /// </summary>
        public static Period ThisYear
        {
            get
            {
                return DateTime.Now.GetYearPeriod();
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
        /// Optional data object associated with the time period
        /// </summary>
        public object Data { get; private set; }

        /// <summary>
        /// Date of period's start
        /// </summary>
        public DateTime StartDate
        {
            get
            {
                return Start.Date;
            }
        }

        /// <summary>
        /// Date of period's end
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                return End.Date;
            }
        }

        /// <summary>
        /// Gets the length of the period
        /// </summary>
        public TimeSpan Length
        {
            get
            {
                return End - Start;
            }
        }

        public static bool operator ==(Period left, Period right)
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

        public static bool operator !=(Period left, Period right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Checks if time is overlapping with this period, i.e. if it is in this period
        /// </summary>
        /// <param name="other">The time argument</param>
        /// <returns>True if the time is inside the interval. Otherwise - false</returns>
        public bool IsOverlappingWith(DateTime time)
        {
            return Start <= time && time < End;
        }

        /// <summary>
        /// Checks if this period is overlapping with another period
        /// </summary>
        /// <param name="other">The other period</param>
        /// <returns>True if the two periods overlap. Otherwise - false</returns>
        /// <exception cref="ArgumentNullException">Thrown if the argument is null</exception>
        public bool IsOverlappingWith(IPeriod other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            return other.Start < End && Start < other.End;
        }

        /// <summary>
        /// Checks if this period is overlapping with collection of periods
        /// </summary>
        /// <param name="periods">Collection periods</param>
        /// <returns>True if the period overlaps with any other period. Otherwise - false</returns>
        /// <exception cref="ArgumentNullException">Thrown if the argument is null</exception>
        public bool IsOverlappingWith(IEnumerable<IPeriod> periods)
        {
            return periods.Any(p => p.GetPeriod().IsOverlappingWith(this));
        }

        /// <summary>
        /// Gets intersection between period and collection of periods
        /// </summary>
        /// <param name="periods">Collection of periods which to be used for intersection</param>
        /// <returns>Returns intersection between period and collection of periods</returns>
        public PeriodCollection GetIntersection(IEnumerable<IPeriod> periods)
        {
            var resultItems = new List<Period>();

            var intervalsList = periods
                        .Where(x => this.Start <= x.End && x.Start <= this.End)
                        .OrderBy(x => x.Start)
                        .ToList();

            if (intervalsList.Count() > 0)
            {
                var itemStartTime = this.Start;
                var itemEndTime = this.End;

                for (int i = 0; i < intervalsList.Count; i++)
                {
                    Period itemToAdd;
                    var overlayOvertime = intervalsList[i];

                    if (i < intervalsList.Count - 1)
                    {
                        var nextOvertime = intervalsList[i + 1];

                        itemToAdd = new Period(
                            Max(overlayOvertime.Start, itemStartTime),
                            Min(nextOvertime.Start, overlayOvertime.End),
                            this.Data);

                        itemStartTime = nextOvertime.Start;
                    }
                    else
                    {
                        itemToAdd = new Period(
                            Max(overlayOvertime.Start, itemStartTime),
                            Min(itemEndTime, overlayOvertime.End),
                            this.Data);
                    }

                    resultItems.Add(itemToAdd);
                }
            }

            return new PeriodCollection(resultItems);
        }

        /// <summary>
        /// Gets difference between period and collection of periods
        /// </summary>
        /// <param name="periods">Collection of periods which to be used for difference</param>
        /// <returns>Returns difference between period and collection of periods</returns>
        public PeriodCollection GetDifference(IEnumerable<IPeriod> periods)
        {
            var resultItems = new List<Period>();
            var itemTimeStart = this.Start;

            var overlappingIntervals = periods
                    .Where(x => this.Start <= x.End && x.Start <= this.End)
                    .OrderBy(x => x.Start)
                    .ToList();

            if (overlappingIntervals.Count() > 0)
            {
                var itemStartTime = this.Start;
                var itemEndTime = this.End;

                for (int i = 0; i < overlappingIntervals.Count; i++)
                {
                    var interval = overlappingIntervals[i];

                    if (itemStartTime < interval.Start)
                    {
                        var intervalToAdd = new Period(itemStartTime, interval.Start, this.Data);
                        resultItems.Add(intervalToAdd);
                    }

                    itemStartTime = interval.End;
                }

                if (itemStartTime < itemEndTime)
                {
                    var intervalToAdd = new Period(itemStartTime, itemEndTime, this.Data);
                    resultItems.Add(intervalToAdd);
                }
            }
            else
            {
                resultItems.Add(this);
            }

            return new PeriodCollection(resultItems);
        }

        /// <summary>
        /// Checks if two periods are equal
        /// </summary>
        /// <param name="obj">The other period</param>
        /// <returns>True if the periods are equal</returns>
        public override bool Equals(object obj)
        {
            var other = obj as Period;

            return (other != null) ? Equals(other) : false;
        }

        /// <summary>
        /// Checks if two periods are equal
        /// </summary>
        /// <param name="other">The other period</param>
        /// <returns>True if the periods are equal</returns>
        /// <exception cref="ArgumentNullException">Thrown if the argument is null</exception>
        public bool Equals(IPeriod other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            if (Start != other.Start || End != other.End)
            {
                return false;
            }

            return !(other is Period) || Data == ((Period)other).Data;
        }

        /// <summary>
        /// Period's hash function
        /// </summary>
        /// <returns>Period's hash</returns>
        public override int GetHashCode()
        {
            var result = Start.GetHashCode() ^ End.GetHashCode();
            if (Data !=null)
            {
                result ^= Data.GetHashCode();
            }

            return result;
        }

        /// <summary>
        /// Returns text representation of the period
        /// </summary>
        /// <returns>Text representation of the period</returns>
        public override string ToString()
        {
            return String.Format("{0} {1} - {2} {3}",
                Start.ToShortDateString(),
                Start.ToShortTimeString(),
                End.ToShortDateString(),
                End.ToShortTimeString());
        }

        /// <summary>
        /// Returns text representation of the period
        /// </summary>
        /// <param name="format">DateTime format</param>
        /// <returns>Text representation of the period</returns>
        public string ToString(string format)
        {
            return String.Format("{0} - {1}", Start.ToString(format), End.ToString(format));
        }

        /// <summary>
        /// Returns text representation of the period's start and end dates
        /// </summary>
        /// <returns>Text representation of the period's start and end dates</returns>
        public string ToShortDateString()
        {
            return String.Format("{0} - {1}",
                Start.ToShortDateString(),
                End.ToShortDateString());
        }

        private void SetPeriod(DateTime start, DateTime end, object data)
        {
            if (end < start)
            {
                throw new ArgumentException("Start time should be less or equal to the end time");
            }

            Start = start;
            End = end;
            Data = data;
        }

        private static DateTime Max(DateTime a, DateTime b)
        {
            return a < b ? b : a;
        }

        private static DateTime Min(DateTime a, DateTime b)
        {
            return a < b ? a : b;
        }
    }
}
