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
            isEdit = mode == 1 ? true : false;
            LoadLicenceData();
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

            public Licences Licence { get { return licence; } set {} }
            public Colors Color { get { return color; } set { } }
            public EngineTypes Engine { get { return engine; } set { } }
            public string StatusText { get; set; }
        }
        private void LoadLicenceData()
        {
            using (var db = new EntityModel())
                colorBox.ItemsSource = db.Colors.ToList();

            if (isEdit)
            {

            }
            else
            {

            }

        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonPickColor_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ButtonPickDriver_Click(object sender, RoutedEventArgs e)
        {
            new DriversMainWindow(1).Show();
        }
        private void ButtonPickEngine_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            App.RestartTimer();
        }
    }
}
