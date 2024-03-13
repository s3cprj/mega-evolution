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
            //ホームに戻った際に、睡眠検地を停止
            var mainWindow = Application.Current.MainWindow as Megashinka.MainWindow;
            if (mainWindow != null)
            {
                mainWindow.DisableSleepDetection();
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            //スタートボタンを押した際に、睡眠検地を開始
            var mainWindow = Application.Current.MainWindow as Megashinka.MainWindow;
            if (mainWindow != null)
            {
                mainWindow.EnableSleepDetection();
            }
        }


        private void HowtouseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new Howtouse();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
