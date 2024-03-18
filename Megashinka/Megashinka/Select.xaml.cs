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
    /// Select.xaml の相互作用ロジック
    /// </summary>
    public partial class Select : Page
    {
        public Select()
        {
            InitializeComponent();
            // 選択画面に戻った際に、睡眠検知を停止
            var mainWindow = Application.Current.MainWindow as Megashinka.MainWindow;
            if (mainWindow != null)
            {
                mainWindow.StopAllModes();
            }
        }

        private void NormalButton_Click(object sender, RoutedEventArgs e)
        {
            //Goボタンを押した際に、睡眠検知を開始
            var mainWindow = Application.Current.MainWindow as Megashinka.MainWindow;
            if (mainWindow != null)
            {
                mainWindow.StartNormalMode();
            }
        }

        private void PomodoroButton_Click(object sender, RoutedEventArgs e)
        {
            //Goボタンを押した際に、睡眠検知を開始
            var mainWindow = Application.Current.MainWindow as Megashinka.MainWindow;
            if (mainWindow != null)
            {
                mainWindow.StartPomodoroMode();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new Home();
        }
    }
}
