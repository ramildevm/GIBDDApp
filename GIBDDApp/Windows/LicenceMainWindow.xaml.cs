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
    /// Логика взаимодействия для LicenceMainWindow.xaml
    /// </summary>
    public partial class LicenceMainWindow : Window
    {
        private List<LicenceFullInfo> griditems;
        public LicenceMainWindow()
        {
            InitializeComponent();
            LoadDriversData();
        }
        class LicenceFullInfo
        {
            private readonly Licences licence;
            private readonly Colors color;
            private readonly EngineTypes engine;

            public LicenceFullInfo(Licences licence, Colors color, EngineTypes engine)
            {
                this.licence = licence;
                this.color = color;
                this.engine = engine;
            }

            public Licences Licence { get { return licence; } }
            public Colors Color { get { return color; } }
            public EngineTypes Engine { get { return engine; } }
            public int Btn1 { get; set; } = 0;
            public int Btn2 { get; set; } = 0;
            public int Btn3 { get; set; } = 0;
            public string StatusText { get; set; }
        }

        private void LoadDriversData()
        {
            griditems = new List<LicenceFullInfo>();
            using (var db = new EntityModel())
            {
                var list = db.Licences.ToList();
                foreach (var l in list)
                {
                    var color = db.Colors.FirstOrDefault(v => v.ColorId == l.Color);
                    var engine = db.EngineTypes.FirstOrDefault(v => v.Id==l.EngineType);
                    var status = db.LicenseStatus.FirstOrDefault(v => v.LicenceId==l.DriverId);
                    var item = new LicenceFullInfo(l, color, engine);
                    item.StatusText = status.Status;
                    switch (status.Status)
                    {
                        case "изъят":
                            item.Btn1 = 1;
                            break;
                        case "приостановлен":
                            item.Btn1 = 1;
                            break;
                        case "утратил силу":
                            item.Btn2 = 1;
                            break;
                        case "активен":
                            item.Btn3 = 1;
                            break;
                    }
                    griditems.Add(item);
                }
                dgridLicence.ItemsSource = griditems;
            }
            dgridLicence.CanUserAddRows = false;
        }

        private void RowChangeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new LicenceEditWindow(1).ShowDialog();
            LoadDriversData();
            this.Show();
        }

        private void RowDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    var item = row.Item as LicenceFullInfo;
                    griditems.Remove(item);
                    dgridLicence.Items.Refresh();
                }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new LicenceEditWindow(0).ShowDialog();
            LoadDriversData();
            this.Show();
        }
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            App.RestartTimer();
        }

        private void ButtonDrivers_Click(object sender, RoutedEventArgs e)
        {
            new DriversMainWindow(0).Show();
            this.Close();
        }

        private void ButtonFines_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonDTP_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RowStatusHistory_Click(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    var item = row.Item as LicenceFullInfo;
                    new StatusHistoryWindow(item.Licence).Show();
                }
        }
    }
}
