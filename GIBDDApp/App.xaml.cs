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
        static public System.Windows.Threading.DispatcherTimer sessionTimer;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            SessionContext.CurrentTime = TimeSpan.FromMinutes(-1);
            sessionTimer = new System.Windows.Threading.DispatcherTimer(new TimeSpan(0, 0, 1), System.Windows.Threading.DispatcherPriority.Normal, delegate
            {
                if (SessionContext.CurrentTime == TimeSpan.FromMinutes(1))
                {
                    MessageBox.Show("Сеанс заверщится через минуту");
                }
                if (SessionContext.CurrentTime == TimeSpan.Zero)
                {
                    MessageBox.Show("Сеанс заверщился. Выполняется выход");
                    var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    SessionContext.SetDefaults();
                    SessionContext.SetTimer(1);
                    var newWindow = new StartWindow();
                    newWindow.Show();
                    window.Close();
                    sessionTimer.Stop();
                }
                SessionContext.CurrentTime = SessionContext.CurrentTime.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);
        }
    }
}
