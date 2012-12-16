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
        #region Fields
        /// <summary>
        /// The current <see cref="TimedDay"/>.
        /// </summary>
        private TimedDay _currentDay;

        /// <summary>
        /// The current date.
        /// </summary>
        private DateTime _currentDate;

        /// <summary>
        /// The view model for time entry.
        /// </summary>
        private TimeEntryViewModel _timeEntryViewModel;
        #endregion

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
            TimeEntryViewModel = new TimeEntryViewModel(clientManager, CurrentDate);
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
        public DateTime CurrentDate 
        {
            get { return _currentDate; }
            private set
            {
                SetProperty("CurrentDate", ref _currentDate, value);
                CurrentDay = TimeEntryManager[CurrentDate];
                TimeEntryViewModel = new TimeEntryViewModel(ClientManager, CurrentDate);
            }
        }

        /// <summary>
        /// Gets all the <see cref="TimeEntryViewModel"/>s that have been added for today.
        /// </summary>
        public IEnumerable<TimeEntry> TimeEntries { get { return CurrentDay.TimeEntries; } }

        /// <summary>
        /// Gets the <see cref="TimedDay"/> for the current day.
        /// </summary>
        public TimedDay CurrentDay 
        {
            get { return _currentDay; }
            set { SetProperty("CurrentDay", ref _currentDay, value); }
        }

        /// <summary>
        /// Gets the current <see cref="TimeEntryViewModel"/>.
        /// </summary>
        public TimeEntryViewModel TimeEntryViewModel 
        {
            get { return _timeEntryViewModel; }
            private set
            {
                SetProperty("TimeEntryViewModel", ref _timeEntryViewModel, value);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Called when we are instructed to add a new time entry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAddNewTimeEntryExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            CurrentDay.AddTimeEntry(TimeEntryViewModel.TimeEntry);
        }

        /// <summary>
        /// Called when we are instructed to go to the previous day.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGoToPreviousDayExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            CurrentDate = CurrentDate.Subtract(TimeSpan.FromDays(1));
        }

        /// <summary>
        /// Called when we are instructed to go to the next day.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGoToNextDayExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            CurrentDate = CurrentDate.AddDays(1);
        }

        /// <summary>
        /// Determines whether or not we can add a new time entry.  We can add a new time 
        /// entry if the requisite fields have all been filled out.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCanExecuteAddNewTimeEntry(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty(TimeEntryViewModel.ClientName) &&
                           !string.IsNullOrEmpty(TimeEntryViewModel.WorkType) &&
                           TimeEntryViewModel.TotalTime > TimeSpan.FromMinutes(1d);
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
                    new CommandBinding(TimeTrackerCommands.AddNewTimeEntryCommand, OnAddNewTimeEntryExecuted, OnCanExecuteAddNewTimeEntry),
                    new CommandBinding(TimeTrackerCommands.GoToPreviousDayCommand, OnGoToPreviousDayExecuted),
                    new CommandBinding(TimeTrackerCommands.GoToNextDayCommand, OnGoToNextDayExecuted)
                });
            }
        }
        #endregion
    }
}
