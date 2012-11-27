using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Beerman006.TimeTracker.Modeling
{
    /// <summary>
    /// Represents a day for which time has been entered.
    /// </summary>
    public class TimedDay
    {
        #region Fields
        /// <summary>
        /// The day.
        /// </summary>
        private readonly DateTime _day;

        /// <summary>
        /// The time entries for the day.
        /// </summary>
        private readonly ObservableCollection<TimeEntry> _entries = new ObservableCollection<TimeEntry>();
        #endregion

        #region Constructor
        public TimedDay(DateTime today)
        {
            _day = today;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the <see cref="DateTime"/> for the day corresponding to this <see cref="TimedDay"/>.
        /// </summary>
        public DateTime Day { get { return _day; } }

        /// <summary>
        /// Gets all of the <see cref="TimeEntry"/>s that have been entered for this day.
        /// </summary>
        public ObservableCollection<TimeEntry> TimeEntries { get { return _entries; } }
        #endregion

        #region Methods
        /// <summary>
        /// Adds a time entry to the day.
        /// </summary>
        /// <param name="timeEntry">The <see cref="TimeEntry"/> to be added.</param>
        public void AddTimeEntry(TimeEntry timeEntry)
        {
            _entries.Add(timeEntry);
        }
        #endregion
    }
}
