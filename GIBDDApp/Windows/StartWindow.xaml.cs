using GIBDDApp.Utils;
using GIBDDApp.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GIBDDApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        System.Windows.Threading.DispatcherTimer logInAccessTimer = new System.Windows.Threading.DispatcherTimer();

        public StartWindow()
        {
            InitializeComponent();
            txtLogin.Text = "ramil";
            txtPassword.Password = "ramil";

            logInAccessTimer.Interval = getTimeFromFile();
            logInAccessTimer.Tick += LogInAccessTimer_Tick;
            logInAccessTimer.Start();
        }

        private TimeSpan getTimeFromFile()
        {
            string timetxt = File.ReadAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Timer", "clock.txt"));
            if (timetxt.Length == 0)
                return SessionContext.TimerInterval;
            else
            {
                var now = DateTime.Now;
                var previous = DateTime.Parse(timetxt);
                loginBtn.IsEnabled = false;
                TimeSpan duration = now.Subtract(previous);
                TimeSpan time = new TimeSpan(0, 1, 0);
                time = time.Subtract(duration);
                if (time <= new TimeSpan(0))
                    return SessionContext.TimerInterval;
                else
                    return time;
            }
        }

        private void LogInAccessTimer_Tick(object sender, EventArgs e)
        {
            loginBtn.IsEnabled = true;
            SessionContext.SetTimer();
            logInAccessTimer.Stop();
        }
        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (SessionContext.Attempts >= 3)
            {
                loginBtn.IsEnabled = false;
                logInAccessTimer.Interval = new TimeSpan(0,1,0);
                File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Timer", "clock.txt"), DateTime.Now.ToString());               
                logInAccessTimer.Start();
                return;
            }
            File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Timer", "clock.txt"), "");

            string result = LoginMethod(txtLogin.Text.Replace(" ",""), txtPassword.Password.Replace(" ", ""));
            if (result == $"Добро пожаловать, {txtLogin.Text}!")
            {
                if (SessionContext.CurrentUser.Role == "Инспектор")
                {
                    var driversWindow = new DriversMainWindow();
                    this.Close();
                    driversWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Произошла ошибка с определением роли пользователя", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                txtLogin.Text = "";
                txtPassword.Password = "";
            }
            else
            {
                SessionContext.Attempts++;
                MessageBox.Show(result + $"\nОсталось попыток: {3-SessionContext.Attempts}", "Результат", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private string LoginMethod(string login, string password)
        {
            if (login.Length == 0 || password.Length == 0)
                return "Не все поля заполнены!";
            using (var db = new EntityModel())
            {
                Users user = (from u in db.Users where u.Login == login select u).FirstOrDefault();
                if (user == null)
                    return "Пользователя с таким логином не существует!";
                if (user.Password != password)
                    return "Неверный пароль!";
                SessionContext.CurrentUser = user;
            }
            return $"Добро пожаловать, {login}!";
        }
    }
}
