using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodLib
{

    /// <summary>
    /// Contains extension method for IEnumerable of IPeriod objects
    /// </summary>
    public static class IPeriodEnumerableExtensions
    {
        /// <summary>
        /// Converts IEnumerable of IPeriods to PeriodCollection
        /// </summary>
        /// <param name="periods"></param>
        /// <returns></returns>
        public static PeriodCollection ToCollection(this IEnumerable<IPeriod> periods)
        {
            throw new NotImplementedException();
        }
    }
}
