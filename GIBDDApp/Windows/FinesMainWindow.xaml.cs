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
    /// Логика взаимодействия для FinesMainWindow.xaml
    /// </summary>
    public partial class FinesMainWindow : Window
    {
        private List<Fines> griditems;

        public FinesMainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using(var db = new EntityModel()){
                griditems = db.Fines.ToList();
                dgridFines.ItemsSource = griditems;
            }
            dgridFines.CanUserAddRows = false;
        }

        private void RowChangeButton_Click(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    var item = row.Item as Fines;
                    var FW_element_CarNumber = dgridFines.Columns[1].GetCellContent(row);
                    var txtCarNumber = ((DataGridTemplateColumn)dgridFines.Columns[3]).CellTemplate.FindName("txtCarNumber", FW_element_CarNumber) as TextBox;

                }
        }

        private void RowDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    var item = row.Item as Fines;
                    griditems.Remove(item);
                    dgridFines.Items.Refresh();
                }
        }
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            App.RestartTimer();
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

        private void ButtonDTP_Click(object sender, RoutedEventArgs e)
        {
            new DTPMainWindow().Show();
            this.Close();
        }
    }
}
