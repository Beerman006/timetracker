using System;
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
using System.ComponentModel.Composition;
using Beerman006.TimeTracker.ViewModel;

namespace Beerman006.TimeTracker.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Export(App.MainWindowKey, typeof(Window))]
    public partial class MainWindow : Window
    {
        [ImportingConstructor]
        public MainWindow(CurrentDayTimeTracker viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            ViewModel.AddCommandBindings(this);
        }

        #region Properties
        /// <summary>
        /// Gets the <see cref="ViewModelBase"/> for this visual.
        /// </summary>
        public ViewModelBase ViewModel
        {
            get { return DataContext as ViewModelBase; }
        }
        #endregion
    }
}
