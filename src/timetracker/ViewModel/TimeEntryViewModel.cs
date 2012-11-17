﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Beerman006.TimeTracker.Modeling;

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
        /// The available clients.
        /// </summary>
        private readonly List<Client> _clients = new List<Client>();
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new <see cref="TimeEntryViewModel"/>.
        /// </summary>
        /// <param name="today">The <see cref="DateTime"/> corresponding to this time entry.</param>
        public TimeEntryViewModel(DateTime today)
        {
            CurrentDate = new DateTime(today.Year, today.Month, today.Day);

            // TODO: this is all dummied for now...
            var client = new Client("Foo");
            client.AddWorkType("State", "Foo1");
            Clients.Add(client);

            client = new Client("Bar", "Bar1");
            client.AddWorkType("Federal");
            client.AddWorkType("State", "Bar2");
            Clients.Add(client);
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
                SetProperty(GetPropertyName(() => Client), ref _client, value);
                ChargeCode = Client.GetChargeCodeFromWorkType(WorkType);
            }
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
                value = new DateTime(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day, value.Hour, value.Minute, value.Second);
                SetProperty(GetPropertyName(() => StartTime), ref _startTime, value);
                CalculateTotalTime();
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
                value = new DateTime(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day, value.Hour, value.Minute, value.Second);
                SetProperty(GetPropertyName(() => EndTime), ref _endTime, value);
                CalculateTotalTime();
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

        // TODO: get this list of clients from some higher level modeling object.
        /// <summary>
        /// Gets the clients to whom time can be charged.
        /// </summary>
        public ICollection<Client> Clients { get { return _clients; } }
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
            return time == default(DateTime);
        }
        #endregion
    }
}