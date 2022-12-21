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
    /// Логика взаимодействия для DriversEditWindow.xaml
    /// </summary>
    public partial class DriversEditWindow : Window
    {

        public DriversEditWindow()
        {
            InitializeComponent();

        }

        public DriversEditWindow(int mode)
        {
            InitializeComponent();
            if(mode == 0)
            {
                LoadDriverData();
            }
            else if(mode == 1)
            {

            }
        }

        private void LoadDriverData()
        {
            
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonPick_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            App.RestartTimer();
        }
    }
}
