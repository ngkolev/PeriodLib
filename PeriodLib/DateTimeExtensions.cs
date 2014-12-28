using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodLib
{
    /// <summary>
    /// This class contains <see cref="DateTime"/> extension methods
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Gets day period by <see cref="DateTime"/> object
        /// </summary>
        /// <param name="time">Time</param>
        /// <returns>Day period</returns>
        public static Period GetDayPeriod(this DateTime time)
        {
            var start = time.Date;
            var end = start.AddDays(1);

            return new Period(start, end);
        }

        /// <summary>
        /// Gets week period by <see cref="DateTime"/> object
        /// </summary>
        /// <param name="time">Time</param>
        /// <returns>Week period</returns>
        public static Period GetWeekPeriod(this DateTime time)
        {
            var date = time.Date;

            var difference = date.DayOfWeek - DayOfWeek.Monday;
            if (difference < 0) difference += 7;
            var startTime = date.AddDays(-difference).Date;

            difference = DayOfWeek.Sunday - date.DayOfWeek;
            if (difference < 0) difference += 7;
            var endTime = date.AddDays(difference).Date;

            var correctedEndTime = endTime.AddDays(1);

            return new Period(startTime, correctedEndTime);
        }

        /// <summary>
        /// Gets month period by <see cref="DateTime"/> object
        /// </summary>
        /// <param name="time">Time</param>
        /// <returns>Month period</returns>
        public static Period GetMonthPeriod(this DateTime time)
        {
            var startTime = new DateTime(time.Year, time.Month, 1);
            var endTime = startTime.AddMonths(1);
            return new Period(startTime, endTime);
        }

        /// <summary>
        /// Gets year period by <see cref="DateTime"/> object
        /// </summary>
        /// <param name="time">Time</param>
        /// <returns>Year period</returns>
        public static Period GetYearPeriod(this DateTime time)
        {
            var startTime = new DateTime(time.Year, 1, 1);
            var endTime = startTime.AddYears(1);
            return new Period(startTime, endTime);
        }
    }
}
