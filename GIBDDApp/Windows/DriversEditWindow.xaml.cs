using GIBDDApp.Utils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для DriversEditWindow.xaml
    /// </summary>
    public partial class DriversEditWindow : Window
    {
        Boolean isEdit = false;
        public DriversEditWindow()
        {
            InitializeComponent();
        }

        public DriversEditWindow(int mode)
        {
            isEdit = mode == 1 ? true : false;
            InitializeComponent();
            LoadDriverData();
        }

        private void LoadDriverData()
        {
            if(!isEdit)
                SessionContext.CurrentDriver = new Drivers();
            this.DataContext = SessionContext.CurrentDriver;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            foreach (var text in new string[] { txtName.Text, txtMiddleName.Text, txtNumber.Text, txtPhone.Text, txtPhoto.Text, txtPostCode.Text, txtSeries.Text, txtJob.Text, txtCompany.Text, txtAddress.Text, txtEmail.Text })
                if (String.IsNullOrEmpty(text))
                {
                    MessageBox.Show("Не все поля заполнены!");
                    return;
                }
            if (!ValidationUtils.IsTextAllowed(txtPostCode.Text)|| !ValidationUtils.IsTextAllowed(txtSeries.Text) || !ValidationUtils.IsTextAllowed(txtNumber.Text))
            {
                MessageBox.Show("Введены некорректные данные!");
                return;
            }
            using(var db = new EntityModel())
            {
                if (isEdit)
                {
                    var driver = db.Drivers.Find(SessionContext.CurrentDriver.Id);
                    driver.Name = SessionContext.CurrentDriver.Name;
                    driver.MiddleName = SessionContext.CurrentDriver.MiddleName;
                    driver.PassportSeries = SessionContext.CurrentDriver.PassportSeries;
                    driver.PassportNumber = SessionContext.CurrentDriver.PassportNumber;
                    driver.PostCode = SessionContext.CurrentDriver.PostCode;
                    driver.Address = SessionContext.CurrentDriver.Address;
                    driver.AddressLife = SessionContext.CurrentDriver.AddressLife;
                    driver.Company = SessionContext.CurrentDriver.Company;
                    driver.Jobname = SessionContext.CurrentDriver.Jobname;
                    driver.Phone = SessionContext.CurrentDriver.Phone;
                    driver.Photo = SessionContext.CurrentDriver.Photo;
                    driver.Description = SessionContext.CurrentDriver.Description;
                    driver.Email = SessionContext.CurrentDriver.Email;
                    db.Entry(driver).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    var driver = new Drivers();
                    driver.Id = 0;
                    driver.Name = SessionContext.CurrentDriver.Name;
                    driver.MiddleName = SessionContext.CurrentDriver.MiddleName;
                    driver.PassportSeries = SessionContext.CurrentDriver.PassportSeries;
                    driver.PassportNumber = SessionContext.CurrentDriver.PassportNumber;
                    driver.PostCode = SessionContext.CurrentDriver.PostCode;
                    driver.Address = SessionContext.CurrentDriver.Address;
                    driver.AddressLife = SessionContext.CurrentDriver.AddressLife;
                    driver.Company = SessionContext.CurrentDriver.Company;
                    driver.Jobname = SessionContext.CurrentDriver.Jobname;
                    driver.Phone = SessionContext.CurrentDriver.Phone;
                    driver.Photo = SessionContext.CurrentDriver.Photo;
                    driver.Description = SessionContext.CurrentDriver.Description;
                    driver.Email = SessionContext.CurrentDriver.Email;
                    db.Drivers.Add(driver);
                }
                db.SaveChanges();
            }
            this.Close();
        }

        private void ButtonPick_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            string folderpath = System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resources\\Drivers\\";
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                        "Portable Network Graphic (*.png)|*.png";

            bool? myResult;
            myResult = op.ShowDialog();
            if (myResult != null && myResult == true)
            {
                txtPhoto.Text = op.SafeFileName;
                SessionContext.CurrentDriver.Photo = op.SafeFileName;
                if (!Directory.Exists(folderpath))
                {
                    Directory.CreateDirectory(folderpath);
                }
                string filePath = folderpath + System.IO.Path.GetFileName(op.FileName);
                System.IO.File.Copy(op.FileName, filePath, true);
            }
        }
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            App.RestartTimer();
        }
    }
}
