﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Beerman006.TimeTracker.ViewModel;

namespace Beerman006.TimeTracker.Presentation
{
    /// <summary>
    /// Interaction logic for TimeEntry.xaml
    /// </summary>
    public partial class TimeEntry : UserControl
    {
        public TimeEntry()
        {
            InitializeComponent();
            InputBindings.Add(new InputBinding(TimeTrackerCommands.AddNewTimeEntryCommand, new KeyGesture(Key.Enter)));
        }
    }
}
