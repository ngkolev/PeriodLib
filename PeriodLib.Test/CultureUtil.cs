using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PeriodLib.Test
{
    internal static class CultureUtil
    {
        public static void EnsureCulture()
        {
            var culture = CultureInfo.GetCultureInfo("bg");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}
