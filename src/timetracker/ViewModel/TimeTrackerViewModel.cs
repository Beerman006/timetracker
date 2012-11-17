using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beerman006.TimeTracker.ViewModel
{
    public class TimeTrackerViewModel : ViewModelBase
    {
        #region Constructor
        /// <summary>
        /// Constructs a new <see cref="TimeTrackerViewModel"/>.
        /// </summary>
        public TimeTrackerViewModel()
        {
            TimeEntry = new TimeEntryViewModel(DateTime.Now);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the current <see cref="TimeEntryViewModel"/>.
        /// </summary>
        public TimeEntryViewModel TimeEntry { get; private set; }
        #endregion
    }
}
