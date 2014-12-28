using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodLib
{
    /// <summary>
    /// This class contains <see cref="IPeriod"/> extension methods
    /// </summary>
    public static class IPeriodExtensions
    {
        /// <summary>
        /// Converts period-like object to Period
        /// </summary>
        /// <param name="period">Object that implement's <see cref="IPeriod"/> interface</param>
        /// <returns>Period object</returns>
        public static Period GetPeriod(this IPeriod period)
        {
            var result = period as Period;
            if (result == null)
            {
                result = new Period(period);
            }

            return result;
        }
    }
}
