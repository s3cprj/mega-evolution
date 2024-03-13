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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Megashinka
{
    /// <summary>
    /// Home.xaml の相互作用ロジック
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new Warning();
        }

        private void HowtouseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new Howtouse();
        }

        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new Setting();
        }
    }
}
