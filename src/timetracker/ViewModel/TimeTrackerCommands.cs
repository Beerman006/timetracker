using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Beerman006.TimeTracker.ViewModel
{
    public static class TimeTrackerCommands
    {
        /// <summary>
        /// A command for adding new time entries.
        /// </summary>
        public static readonly RoutedUICommand AddNewTimeEntryCommand =
            new RoutedUICommand(
                "Add Time Entry",
                "TimeTrackerCommands.AddNewTimeEntryCommand",
                typeof(TimeTrackerCommands));
    }
}
