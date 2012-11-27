using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Collections.ObjectModel;

namespace Beerman006.TimeTracker.Modeling
{
    /// <summary>
    /// An <see cref="ITimeEntryManager"/> that persists it's time entries to xml.
    /// </summary>
    [Export(typeof(ITimeEntryManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class XMLTimeEntryManager : ITimeEntryManager
    {
        #region Fields
        /// <summary>
        /// Gets all of the <see cref="TimedDay"/>s we know about.
        /// </summary>
        private readonly Collection<TimedDay> _days = new Collection<TimedDay>();
        #endregion

        #region Properties
        /// <summary>
        /// Gets the <see cref="TimedDay"/>s that have been loaded.
        /// </summary>
        public IEnumerable<TimedDay> Days
        {
            get { return _days; }
        }

        /// <summary>
        /// Gets a <see cref="TimedDay"/> for a specific date.
        /// </summary>
        /// <param name="date">The date of the requested <see cref="TimedDay"/>.</param>
        /// <returns>The requested <see cref="TimedDay"/>.</returns>
        public TimedDay this[DateTime date]
        {
            get 
            {
                var day = Days.FirstOrDefault(d => d.Day == date);
                if (day == null)
                {
                    day = new TimedDay(date);
                    _days.Add(day);
                }
                return day;
            }
        }
        #endregion
    }
}
