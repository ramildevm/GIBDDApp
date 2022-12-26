using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace GIBDDApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для StatusHistoryWindow.xaml
    /// </summary>
    public partial class StatusHistoryWindow : Window
    {
        private readonly Licences licences;

        public StatusHistoryWindow(Licences licences)
        {
            InitializeComponent();
            this.licences = licences;
            LoadData();
        }

        private void LoadData()
        {
            List<LicenseStatus> list;
            using(var db = new EntityModel())
            {
                list = db.LicenseStatus.Where(v=>v.LicenceId==licences.DriverId).OrderByDescending(v => v.Id).ToList();
                dgridStatus.ItemsSource = list;
                dgridStatus.CanUserAddRows = false;
            }
            dgridStatus.Items.Refresh();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            App.RestartTimer();            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = statusBox.SelectedItem as TextBlock;
            using(var db = new EntityModel())
            {
                var status = new LicenseStatus()
                {
                    Status = item.Text,
                    Date = DateTime.Now,
                    LicenceId = licences.DriverId
                };
                db.LicenseStatus.Add(status);
                db.SaveChanges();
            }
            LoadData();
        }
    }
}
