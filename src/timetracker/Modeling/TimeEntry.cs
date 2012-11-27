using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beerman006.TimeTracker.Modeling
{
    public class TimeEntry
    {
        #region Properties
        /// <summary>
        /// Gets and sets the client associated with this time entry.
        /// </summary>
        public Client Client { get; set; }

        /// <summary>
        /// Gets and sets the charge code associated with this time entry.
        /// </summary>
        public string ChargeCode { get; set; }

        /// <summary>
        /// Gets and sets the work type associated with this client.
        /// </summary>
        public string WorkType { get; set; }

        /// <summary>
        /// Gets and sets the description of this time entry.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets and sets the start time of this time entry.
        /// </summary>
        /// <remarks>
        /// This can be unused, and if is unused, will be equal to <see cref="DateTime.MinValue"/>.
        /// </remarks>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets and sets the end time of this time entry.
        /// </summary>
        /// <remarks>
        /// This can be unused, and if is unused, will be equal to <see cref="DateTime.MinValue"/>.
        /// </remarks>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Gets and sets the total time for this time entry.
        /// </summary>
        public TimeSpan TotalTime { get; set; }

        /// <summary>
        /// The date of this time entry.
        /// </summary>
        public DateTime Date { get; set; }
        #endregion

        #region Overrides
        /// <summary>
        /// Obtains a string representation of this <see cref="TimeEntry"/>.
        /// </summary>
        /// <returns>A string representation of this <see cref="TimeEntry"/>.</returns>
        public override string ToString()
        {
            return string.Format("{0}:{1} for {2:F1} hours", Client.Name, WorkType, TotalTime.TotalHours);
        }
        #endregion
    }
}
