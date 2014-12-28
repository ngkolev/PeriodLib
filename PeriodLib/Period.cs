using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodLib
{
    [DebuggerDisplay("{Start} - {End}")]
    public sealed class Period : IEquatable<Period>, PeriodLib.IPeriod
    {
        public Period(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public Period(IPeriod other)
        {
            throw new NotImplementedException();
        }

        public Period()
        {
            throw new NotImplementedException();
        }

        public static Period ThisDay
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public static Period ThisWeek
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public static Period ThisMonth
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public static Period ThisYear
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public DateTime StartDate
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public DateTime EndDate
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public TimeSpan Length
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsOverlappingWith(Period other)
        {
            throw new NotImplementedException();
        }

        public bool IsOverlappingWith(IEnumerable<Period> periods)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public bool Equals(Period other)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public string ToShortDateString()
        {
            throw new NotImplementedException();
        }
    }
}
