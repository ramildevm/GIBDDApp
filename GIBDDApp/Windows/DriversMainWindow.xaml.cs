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
        private List<DriversInfo> griditems;
        private bool isPick;

        class DriversInfo
        {
            private readonly Drivers drivers;

            public DriversInfo(Drivers drivers)
            {
                this.drivers = drivers;
            }
            public string ButtonText { get; set; } = "Изменить";
            public Visibility DeleteButtonVisibility { get; set; } = Visibility.Visible;
            public Drivers Drivers { get { return drivers; } }
        }
        public DriversMainWindow(int mode)
        {
            InitializeComponent();
            this.isPick = (mode == 1) ? true : false;
            SetOptions();
            LoadDriversData();
            App.RestartTimer();
        }

        private void SetOptions()
        {
            dgridDrivers.CanUserAddRows = false;
            if (isPick)
                btnDTP.Visibility = btnFines.Visibility = btnLicence.Visibility = Visibility.Hidden;
        }

        private void LoadDriversData()
        {
            griditems = new List<DriversInfo>();
            using (var db = new EntityModel())
            {
                var list = db.Drivers.ToList();
                foreach(var d in list)
                {
                    d.Photo = System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources\\Drivers\\" + d.Photo;
                    var item = new DriversInfo(d);
                    if (!isPick)
                        griditems.Add(item);
                    else
                    {
                        item.ButtonText = "Выбрать";
                        item.DeleteButtonVisibility = Visibility.Collapsed;
                        if(db.Licences.Find(d.Id)==null)
                            griditems.Add(item);
                    }
                }
                dgridDrivers.ItemsSource = griditems;
            }
            dgridDrivers.Items.Refresh();

        }

        private void RowChangeButton_Click(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    var item = row.Item as DriversInfo;
                    using (var db = new EntityModel())
                        if (!isPick)
                            SessionContext.CurrentDriver = db.Drivers.Find(item.Drivers.Id);
                        else
                            SessionContext.CurrentLicenceDriver = db.Drivers.Find(item.Drivers.Id);
                }
            if (isPick)
            {
                SessionContext.CurrentDriver = null;
                this.Close();
                return;
            }
            this.Hide();
            new DriversEditWindow(1).ShowDialog();
            LoadDriversData();
            this.Show();
        }

        private void RowDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    var item = row.Item as DriversInfo;
                    using(var db = new EntityModel())
                    {
                        var driver = db.Drivers.Find(item.Drivers.Id);
                        db.Entry(driver).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                    }
                    griditems.Remove(item);
                    dgridDrivers.Items.Refresh();
                }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            SessionContext.CurrentDriver = new Drivers();
            this.Hide();
            new DriversEditWindow(0).ShowDialog();
            LoadDriversData();
            this.Show();
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

        private void ButtonFines_Click(object sender, RoutedEventArgs e)
        {
            new FinesMainWindow().Show();
            this.Close();
        }

        private void ButtonDTP_Click(object sender, RoutedEventArgs e)
        {
            new DTPMainWindow().Show();
            this.Close();
        }
    }
}
