using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Beerman006.TimeTracker.Modeling;
using System.Windows.Input;

namespace Beerman006.TimeTracker.ViewModel
{
    public class TimeEntryViewModel : ViewModelBase
    {
        #region Fields
        /// <summary>
        /// The charge code for the time entry.
        /// </summary>
        private string _chargeCode;

        /// <summary>
        /// The current <see cref="Client"/>.
        /// </summary>
        private Client _client;

        /// <summary>
        /// The work type for the client.
        /// </summary>
        private string _workType;

        /// <summary>
        /// A description of the time entry.
        /// </summary>
        private string _description;

        /// <summary>
        /// The name of the client.  This is not meant to be used in any way but 
        /// for updates pushed down from the UI.  
        /// </summary>
        private string _clientName;

        /// <summary>
        /// The time the chargable time began.
        /// </summary>
        private DateTime _startTime;

        /// <summary>
        /// The time the chargable time ended.
        /// </summary>
        private DateTime _endTime;

        /// <summary>
        /// The total amount of time that was charged.
        /// </summary>
        private TimeSpan _totalTime;

        /// <summary>
        /// The current <see cref="IClientManager"/>.
        /// </summary>
        private readonly IClientManager _clientManager;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new <see cref="TimeEntryViewModel"/>.
        /// </summary>
        /// <param name="today">The <see cref="DateTime"/> corresponding to this time entry.</param>
        public TimeEntryViewModel(IClientManager clientManager, DateTime today)
        {
            _clientManager = clientManager;
            CurrentDate = new DateTime(today.Year, today.Month, today.Day);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets and sets the charge code for the time entry.
        /// </summary>
        public string ChargeCode
        {
            get { return _chargeCode; }
            set { SetProperty(GetPropertyName(() => ChargeCode), ref _chargeCode, value); }
        }

        /// <summary>
        /// Gets and sets the current <see cref="Client"/>.
        /// </summary>
        public Client Client
        {
            get { return _client; }
            set 
            {
                if (value == null)
                {
                    var client = new Client(ClientName);
                    ClientManager.AddClient(client);
                    value = client;
                }

                SetProperty(GetPropertyName(() => Client), ref _client, value);
                ChargeCode = Client.GetChargeCodeFromWorkType(WorkType);
            }
        }

        /// <summary>
        /// Gets and sets the name of the client.  This is not meant to be used in 
        /// any way but for updates pushed down from the UI.  
        /// </summary>
        public string ClientName
        {
            get { return _clientName; }
            set { _clientName = value; }
        }

        /// <summary>
        /// Gets and sets the work type for the client.
        /// </summary>
        public string WorkType
        {
            get { return _workType; }
            set 
            {   
                SetProperty(GetPropertyName(() => WorkType), ref _workType, value);
                ChargeCode = Client.GetChargeCodeFromWorkType(WorkType);
            }
        }

        /// <summary>
        /// Gets and sets the description of the time entry.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { SetProperty(GetPropertyName(() => Description), ref _description, value); }
        }

        /// <summary>
        /// Gets the time the chargeable time entry began.
        /// </summary>
        public DateTime StartTime
        {
            get { return _startTime; }
            set 
            {
                if (!TimeIsUnset(value))
                {
                    value = new DateTime(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day, value.Hour, value.Minute, value.Second);
                    SetProperty(GetPropertyName(() => StartTime), ref _startTime, value);
                    CalculateTotalTime();
                }
            }
        }

        /// <summary>
        /// Gets the time the chargeable time entry ended.
        /// </summary>
        public DateTime EndTime
        {
            get { return _endTime; }
            set 
            {
                if (!TimeIsUnset(value))
                {
                    value = new DateTime(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day, value.Hour, value.Minute, value.Second);
                    SetProperty(GetPropertyName(() => EndTime), ref _endTime, value);
                    CalculateTotalTime();
                }
            }
        }

        /// <summary>
        /// Gets the total amount of time to be charged.
        /// </summary>
        public TimeSpan TotalTime
        {
            get { return _totalTime; }
            set { SetProperty(GetPropertyName(() => TotalTime), ref _totalTime, value); }
        }

        /// <summary>
        /// Gets and sets the date of this time entry.
        /// </summary>
        public DateTime CurrentDate { get; private set; }

        /// <summary>
        /// Gets the <see cref="IClientManager"/>.
        /// </summary>
        public IClientManager ClientManager { get { return _clientManager; } }

        /// <summary>
        /// Gets the clients to whom time can be charged.
        /// </summary>
        public IEnumerable<Client> Clients { get { return ClientManager.Clients; } }

        /// <summary>
        /// Gets a <see cref="TimeEntry"/> that corresponds to this view model.
        /// </summary>
        public TimeEntry TimeEntry
        {
            get
            {
                return new TimeEntry()
                {
                    Client = ClientManager[ClientName],
                    WorkType = WorkType,
                    Description = Description,
                    StartTime = StartTime,
                    EndTime = EndTime,
                    TotalTime = TotalTime,
                    ChargeCode = ChargeCode,
                    Date = CurrentDate
                };
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Udpates the <see cref="TotalTime"/> property based on the <see cref="StartTime"/> and <see cref="EndTime"/> properties.
        /// </summary>
        private void CalculateTotalTime()
        {
            if (TimeIsUnset(StartTime) || TimeIsUnset(EndTime))
            {
                return;
            }
            TotalTime = EndTime - StartTime;
        }

        /// <summary>
        /// Gets whether or not the given <see cref="DateTime"/> is a default time.
        /// </summary>
        /// <param name="time">The <see cref="DateTime"/> to check.</param>
        /// <returns><c>true</c> if the given time is default - or unset.</returns>
        private bool TimeIsUnset(DateTime time)
        {
            return time == DateTime.MinValue;
        }
        #endregion
    }
}
