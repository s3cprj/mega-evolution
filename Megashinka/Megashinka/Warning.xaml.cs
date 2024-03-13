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
    public partial class Warning : Page
    {
        public Warning()
        {
            InitializeComponent();
            this.Loaded += Warning_Loaded;
            this.Unloaded += Warning_Unloaded;
        }

        private void Warning_Loaded(object sender, RoutedEventArgs e)
        {
            // ページが表示された時の処理
            // BGMを再生する
            VolumeController controller = new VolumeController();
            controller.SetVolume(20);
            App.BgmPlayer.IsRepeating = true;
            App.BgmPlayer.PlayMp3File("mydata/Warning.mp3");
        }

        private void Warning_Unloaded(object sender, RoutedEventArgs e)
        {
            // ページが非表示になった時の処理
            // BGMを停止する
            App.BgmPlayer.Stop();
        }

        private void BackIcon_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new Home();
        }
    }
}

