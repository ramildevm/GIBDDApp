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
    /// Логика взаимодействия для DTPMainWindow.xaml
    /// </summary>
    public partial class DTPMainWindow : Window
    {
        public DTPMainWindow()
        {
            InitializeComponent();
            LoadData();
        }
        private List<DTP> griditems;

        private void LoadData()
        {

            using (var db = new EntityModel())
            {
                griditems = db.DTP.ToList();
                dgridDTP.ItemsSource = griditems;
            }
            dgridDTP.CanUserAddRows = false;
        }

        private void RowChangeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RowDeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonLicence_Click(object sender, RoutedEventArgs e)
        {
            new LicenceMainWindow().Show();
            this.Close();
        }

        private void ButtonDrivers_Click(object sender, RoutedEventArgs e)
        {
            new DriversMainWindow(0).Show();
            this.Close();
        }

        private void ButtonFines_Click(object sender, RoutedEventArgs e)
        {
            new FinesMainWindow().Show();
            this.Close();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            App.RestartTimer();
        }
    }
}
