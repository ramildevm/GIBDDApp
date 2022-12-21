using GIBDDApp.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GIBDDApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        static public System.Windows.Threading.DispatcherTimer sessionTimer = new System.Windows.Threading.DispatcherTimer();
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            sessionTimer.Interval = new TimeSpan(0, 1, 0);
            sessionTimer.Tick += SessionTimer_Tick;
        }

        private void SessionTimer_Tick(object sender, EventArgs e)
        {
            SessionContext.SetDefaults();
            var newWindow = new StartWindow();
            newWindow.Show();
            for (int intCounter = App.Current.Windows.Count - 2; intCounter >= 0; intCounter--)
                App.Current.Windows[intCounter].Close();
            sessionTimer.Stop();
        }

        public static void RestartTimer()
        {
            sessionTimer.Stop();
            sessionTimer.Interval = new TimeSpan(0, 1, 0);
            sessionTimer.Start();
        }
    }
}
