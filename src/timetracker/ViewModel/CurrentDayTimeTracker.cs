using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Beerman006.TimeTracker.Modeling;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Beerman006.TimeTracker.ViewModel
{   
    [Export]
    public class CurrentDayTimeTracker : ViewModelBase
    {
        #region Constructor
        /// <summary>
        /// Constructs a new <see cref="CurrentDayTimeTracker"/>.
        /// </summary>
        [ImportingConstructor]
        public CurrentDayTimeTracker(IClientManager clientManager, ITimeEntryManager timeEntryManager)
        {
            ClientManager = clientManager;
            TimeEntryManager = timeEntryManager;

            CurrentDate = DateTime.Now;
            CurrentTimeEntry = new TimeEntryViewModel(clientManager, CurrentDate);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the current <see cref="IClientManager"/>.
        /// </summary>
        public IClientManager ClientManager { get; private set; }

        /// <summary>
        /// Gets the current <see cref="ITimeEntryManager"/>.
        /// </summary>
        public ITimeEntryManager TimeEntryManager { get; private set; }

        /// <summary>
        /// Gets the current date.
        /// </summary>
        public DateTime CurrentDate { get; private set; }

        /// <summary>
        /// Gets all the <see cref="TimeEntry"/>s that have been added for today.
        /// </summary>
        public IEnumerable<TimeEntry> TimeEntries { get { return CurrentDay.TimeEntries; } }

        /// <summary>
        /// Gets the <see cref="TimedDay"/> for the current day.
        /// </summary>
        public TimedDay CurrentDay { get { return TimeEntryManager[CurrentDate]; } }

        /// <summary>
        /// Gets the current <see cref="TimeEntryViewModel"/>.
        /// </summary>
        public TimeEntryViewModel CurrentTimeEntry { get; private set; }
        #endregion

        #region Private Methods
        /// <summary>
        /// Called when we are instructed to add a new time entry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAddNewTimeEntryExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            CurrentDay.AddTimeEntry(CurrentTimeEntry.TimeEntry);
        }

        /// <summary>
        /// Determines whether or not we can add a new time entry.  We can add a new time 
        /// entry if the requisite fields have all been filled out.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCanExecuteAddNewTimeEntry(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty(CurrentTimeEntry.ClientName) &&
                           !string.IsNullOrEmpty(CurrentTimeEntry.WorkType) &&
                           CurrentTimeEntry.TotalTime > TimeSpan.FromMinutes(1d);
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Gets the command bindings this viewmodel exposes.
        /// </summary>
        protected override IEnumerable<CommandBinding> CommandBindings
        {
            get
            {
                return base.CommandBindings.Concat(new[]
                {
                    new CommandBinding(TimeTrackerCommands.AddNewTimeEntryCommand, OnAddNewTimeEntryExecuted, OnCanExecuteAddNewTimeEntry)
                });
            }
        }
        #endregion
    }
}
