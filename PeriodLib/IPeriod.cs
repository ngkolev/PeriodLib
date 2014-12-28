using System;
namespace PeriodLib
{
  
    /// <summary>
    /// This interface represents Period, i.e., something that has start and end time
    /// </summary>
    public interface IPeriod
    {
        /// <summary>
        /// Period's start
        /// </summary>
        DateTime End { get; }

        /// <summary>
        /// Period's end
        /// </summary>
        DateTime Start { get; }
    }
}
