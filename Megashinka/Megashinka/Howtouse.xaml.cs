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
    /// Howtouse.xaml の相互作用ロジック
    /// </summary>
    public partial class Howtouse : Page
    {
        public Howtouse()
        {
            InitializeComponent();
        }

        private void BackIcon_Click(object sender, RoutedEventArgs e)
        {

            Application.Current.MainWindow.Content = new Home();

        }
    }
}
