using System;
namespace PeriodLib
{
    public interface IPeriod
    {
        DateTime End { get; }
        DateTime Start { get; }
    }
}
