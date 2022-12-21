using GIBDDApp.Utils;
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
    /// Логика взаимодействия для DriversMainWindow.xaml
    /// </summary>
    public partial class DriversMainWindow : Window
    {
        private List<Drivers> griditems;

        public DriversMainWindow()
        {
            InitializeComponent();
            LoadDriversdata();
            App.RestartTimer();
        }

        private void LoadDriversdata()
        {
            using(var db = new EntityModel())
            {
                griditems = db.Drivers.ToList();
                foreach(var d in griditems)
                {
                    d.Photo = "pack://application:,,,/Resources/Drivers/" + d.Photo;
                }
                dgridDrivers.ItemsSource = griditems;
            }
            dgridDrivers.CanUserAddRows = false;
        }

        private void RowChangeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RowDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    var item = row.Item as Drivers;
                    griditems.Remove(item);
                    dgridDrivers.Items.Refresh();
                }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            new DriversEditWindow(0).Show();
            this.Close();
        }
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            App.RestartTimer();
        }
    }
}
