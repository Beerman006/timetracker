using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition;

namespace Beerman006.TimeTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public const string MainWindowKey = "MainWindow";

        [Import(MainWindowKey, typeof(Window))]
        public Window AppWindow { get; set; }

        private void InitializeMEF()
        {
            CompositionContainer container = new CompositionContainer(new AssemblyCatalog(GetType().Assembly));
            CompositionBatch batch = new CompositionBatch();
            batch.AddExportedValue(container);
            container.Compose(batch);
            container.SatisfyImportsOnce(this);
        }

        private void OnApplicationStartup(object sender, StartupEventArgs e)
        {
            InitializeMEF();
            MainWindow = AppWindow;
            AppWindow.Show();
        }
    }
}
