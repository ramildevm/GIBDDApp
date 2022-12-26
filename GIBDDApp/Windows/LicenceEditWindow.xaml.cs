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
    /// Логика взаимодействия для LicenceEditWindow.xaml
    /// </summary>
    public partial class LicenceEditWindow : Window
    {
        Boolean isEdit = false;
        public LicenceEditWindow(int mode)
        {
            InitializeComponent();
            SessionContext.CurrentLicenceDriver = null;
            isEdit = mode == 1 ? true : false;
            LoadLicenceData();
        }
        private void LoadLicenceData()
        {
            SessionContext.CurrentLicence.ExpireDate = SessionContext.CurrentLicence.ExpireDate.Date;
            SessionContext.CurrentLicence.LicenceDate = SessionContext.CurrentLicence.LicenceDate.Date;
            this.DataContext = SessionContext.CurrentLicence;
            using (var db = new EntityModel()) {
                colorBox.ItemsSource = db.Colors.ToList();
                colorBox.SelectedIndex = 0;
                engineTypeBox.ItemsSource = db.EngineTypes.ToList();
                engineTypeBox.SelectedIndex = 0;
                
                if (isEdit)
                {
                    SessionContext.CurrentLicenceDriver = db.Drivers.Find(SessionContext.CurrentLicence.DriverId);
                    txtDriver.Text = db.Drivers.Find(SessionContext.CurrentLicence.DriverId).Name;
                    var colors = db.Colors.ToList();
                    colorBox.SelectedIndex = colors.IndexOf(colors.Where(v => v.ColorId == SessionContext.CurrentLicence.Color).FirstOrDefault());
                    var engines = db.EngineTypes.ToList();
                    engineTypeBox.SelectedIndex = engines.IndexOf(engines.Where(v => v.Id == SessionContext.CurrentLicence.EngineType).FirstOrDefault());
                }
            }

        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            foreach (var text in new string[] { txtDate.Text, txtExpiredDate.Text, txtSeries.Text, txtSeries.Text, txtVIN.Text, txtManufacturer.Text, txtModel.Text, txtWeight.Text, txtYear.Text, txtCategories.Text})
                if (String.IsNullOrEmpty(text))
                {
                    MessageBox.Show("Не все поля заполнены!");
                    return;
                }
            if (!ValidationUtils.IsTextAllowed(txtSeries.Text.Replace(" ", "")) || !ValidationUtils.IsTextAllowed(txtNumber.Text) || !ValidationUtils.IsTextAllowed(txtWeight.Text) || !ValidationUtils.IsTextAllowed(txtYear.Text))
            {
                MessageBox.Show("Введены некорректные данные!");
                return;
            }
            DateTime licenceDate;
            if (!DateTime.TryParse(txtDate.Text, out licenceDate))
            {
                MessageBox.Show("Введена некорректная дата!");
                return;
            }
            DateTime expiredDate;
            if (!DateTime.TryParse(txtExpiredDate.Text, out expiredDate))
            {
                MessageBox.Show("Введена некорректная дата!");
                return;
            }
            if (SessionContext.CurrentLicenceDriver == null)
            {
                MessageBox.Show("Выберите водителя!");
                return;
            }
            using (var db = new EntityModel())
            {
                if (isEdit)
                {
                    var licence = db.Licences.Find(SessionContext.CurrentLicence.DriverId);
                    licence.LicenceDate = licenceDate;
                    licence.ExpireDate = expiredDate;
                    licence.LicenceSeries = SessionContext.CurrentLicence.LicenceSeries;
                    licence.LicenceNumber = SessionContext.CurrentLicence.LicenceNumber;
                    licence.Categories = SessionContext.CurrentLicence.Categories;
                    licence.VIN = SessionContext.CurrentLicence.VIN;
                    licence.Manufacturer = SessionContext.CurrentLicence.Manufacturer;
                    licence.Model = SessionContext.CurrentLicence.Model;
                    licence.Year = SessionContext.CurrentLicence.Year;
                    licence.Weight = SessionContext.CurrentLicence.Weight;
                    licence.DriveType = SessionContext.CurrentLicence.DriveType;
                    licence.Color = (colorBox.SelectedItem as Colors).ColorId;
                    licence.EngineType = (engineTypeBox.SelectedItem as EngineTypes).Id;
                    licence.DriverId = SessionContext.CurrentLicenceDriver.Id;
                    db.Entry(licence).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    var licence = new Licences();
                    licence.LicenceDate = licenceDate;
                    licence.ExpireDate = expiredDate;
                    licence.LicenceSeries = SessionContext.CurrentLicence.LicenceSeries;
                    licence.LicenceNumber = SessionContext.CurrentLicence.LicenceNumber;
                    licence.Categories = SessionContext.CurrentLicence.Categories;
                    licence.VIN = SessionContext.CurrentLicence.VIN;
                    licence.Manufacturer = SessionContext.CurrentLicence.Manufacturer;
                    licence.Model = SessionContext.CurrentLicence.Model;
                    licence.Year = SessionContext.CurrentLicence.Year;
                    licence.Weight = SessionContext.CurrentLicence.Weight;
                    licence.DriveType = SessionContext.CurrentLicence.DriveType;
                    licence.Color = (colorBox.SelectedItem as Colors).ColorId;
                    licence.EngineType = (engineTypeBox.SelectedItem as EngineTypes).Id;
                    licence.DriverId = SessionContext.CurrentLicenceDriver.Id;
                    licence = db.Licences.Add(licence);
                    db.LicenseStatus.Add(new LicenseStatus() { LicenceId = licence.DriverId, Status = "активен", Date = DateTime.Now });
                }
                db.SaveChanges();
            }
            this.Close();
        }
        private void ButtonPickDriver_Click(object sender, RoutedEventArgs e)
        {
            new DriversMainWindow(1).ShowDialog();
            if(SessionContext.CurrentLicenceDriver!=null)
                txtDriver.Text = SessionContext.CurrentLicenceDriver.Name;
        }
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            App.RestartTimer();
        }
    }
}
