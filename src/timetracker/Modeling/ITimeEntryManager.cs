using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beerman006.TimeTracker.Modeling
{
    /// <summary>
    /// Manages time entries.
    /// </summary>
    public interface ITimeEntryManager
    {
        #region Properties
        /// <summary>
        /// The days for which we have time entries.
        /// </summary>
        IEnumerable<TimedDay> Days { get; }

        /// <summary>
        /// Gets the <see cref="TimeEntry"/>s for a specific date.
        /// </summary>
        /// <param name="date">The date of the requested <see cref="TimeEntry"/>s.</param>
        /// <returns>The <see cref="TimeEntry"/>s corresponding to the given date.</returns>
        TimedDay this[DateTime date] { get; }
        #endregion
    }
}
