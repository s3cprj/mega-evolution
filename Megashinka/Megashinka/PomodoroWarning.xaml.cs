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
using System.Windows.Threading;

namespace Megashinka
{
    /// <summary>
    /// PomodoroWarning.xaml の相互作用ロジック
    /// </summary>
    public partial class PomodoroWarning : Page
    {
        private DispatcherTimer timer;
        public PomodoroWarning()
        {
            InitializeComponent();
            SetupTimer();
            this.Loaded += Warning_Loaded;
            this.Unloaded += Warning_Unloaded;

        }
        private void SetupTimer()
        {
            UpdateTimeDisplay();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // 1秒ごとに更新
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateTimeDisplay();
        }

        private void UpdateTimeDisplay()
        {
            TimeSpan remainingTime = ((MainWindow)Application.Current.MainWindow).GetPomodoroRemainingTime();
            this.TimerText.Text = $"{remainingTime.Minutes:D2}:{remainingTime.Seconds:D2}";
        }
        private void Warning_Loaded(object sender, RoutedEventArgs e)
        {
            // ページが表示された時の処理
            //　設定ファイルを呼び出しVolumeを設定
            var volume = int.Parse(SettingsManager.GetSettingValueByKey("volume"));
            App.VolumeController.SetVolume(volume);
            //　設定ファイルを呼び出しアラームの音声ファイルパスを取得
            string alarmSound = SettingsManager.GetSettingValueByKey("alarmSound");
            // BGMを再生する
            App.BgmPlayer.IsRepeating = true;
            App.BgmPlayer.PlayMp3File(alarmSound);
        }

        private void Warning_Unloaded(object sender, RoutedEventArgs e)
        {
            // ページが非表示になった時の処理
            // BGMを停止する
            App.BgmPlayer.Stop();
        }

        private void BackIcon_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new Select();
        }
    }
}
