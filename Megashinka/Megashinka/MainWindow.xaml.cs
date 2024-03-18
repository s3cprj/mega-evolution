using System.Diagnostics;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //　全体用
        private DispatcherTimer timer;
        private bool sleepDetectionActive = false;
        public string exePath = "mydata/eye_blink_detecterv2.exe";
        public string csvPath = "mydata/output.csv";

        //　ポモドーロ管理用
        private bool pomodoroMode = false;
        private DispatcherTimer workTimer; // 作業用タイマー
        private DispatcherTimer breakTimer; // 休憩用タイマー
        //private TimeSpan workTime = TimeSpan.FromMinutes(25);
        //private TimeSpan breakTime = TimeSpan.FromMinutes(5);
        private TimeSpan workTime = TimeSpan.FromSeconds(15);
        private TimeSpan breakTime = TimeSpan.FromSeconds(10);
        private bool isWorkTime = true; // 現在が作業時間か休憩時間かを判定

        public MainWindow()
        {
            InitializeComponent();
            this.Closing += MainWindow_Closing;
            SetupTimer();
            SetupPomodoroTimers();
        }

        // ノーマルモードの開始時に呼び出す関数
        public void StartNormalMode()
        {
            StopPomodoroTimer();
            pomodoroMode = false;
            EnableSleepDetection();
            NormalModeUpdateUI();
        }

        //ポモドーロモードの開始時に呼び出す関数
        public void StartPomodoroMode()
        {
            StopPomodoroTimer();
            EnableSleepDetection();
            pomodoroMode = true;
            isWorkTime = true;
            workTimer.Start();
            PomodoroModeUpdateUI();
        }

        // ノーマルモード、ポモドーロモードで使える終了時に呼び出す関数
        public void StopAllModes()
        {
            StopPomodoroTimer();
            DisableSleepDetection();
            pomodoroMode = false;
        }

        // 終了時(Windowを閉じる際の処理)
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.SleepDetectionProcess.Stop();
        }

        // csvのデータをもとに眠たいかどうかを判定
        private bool IsUserSleepyBasedOnCsv()
        {
            string filePath = csvPath; // CSVファイルのパスを指定
            int numberOfLines = 15; // 読み込む行数
            double threshold = 0.5; // trueの割合の閾値

            bool result = FileUtil.CheckTruePercentageInRecentLines(filePath, numberOfLines, threshold);
            return result;
        }

        // 睡眠検知の間隔を操作するためのタイマー
        private void SetupTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (sleepDetectionActive)
            {
                if (pomodoroMode)
                {
                    PomodoroModeUpdateUI();
                }
                else
                {
                    NormalModeUpdateUI();
                }
            }
        }

        // 通常モード(画面UI変更)　睡眠検知をもとに画面変更
        private void NormalModeUpdateUI()
        {
            bool result = IsUserSleepyBasedOnCsv();
            var currentContent = Application.Current.MainWindow.Content;

            // ページの変更が必要ない場合は何もしない
            if ((result && currentContent is Warning) || (!result && currentContent is Normal))
            {
                return;
            }
            if (result)
            {
                Application.Current.MainWindow.Content = new Warning();
            }
            else
            {
                Application.Current.MainWindow.Content = new Normal();
            }
        }

        // ポモドーロ用のタイマー
        private void SetupPomodoroTimers()
        {
            // 作業用タイマーの設定
            workTimer = new DispatcherTimer();
            workTimer.Interval = workTime;
            workTimer.Tick += (sender, e) => {
                // 作業時間が終了したら休憩タイマーを開始
                DisableSleepDetection();
                isWorkTime = false;
                workTimer.Stop();
                breakTimer.Start();
                PomodoroModeUpdateUI();
            };

            // 休憩用タイマーの設定
            breakTimer = new DispatcherTimer();
            breakTimer.Interval = breakTime;
            breakTimer.Tick += (sender, e) => {
                // 休憩時間が終了したら作業タイマーを再開
                EnableSleepDetection();
                isWorkTime = true;
                breakTimer.Stop();
                workTimer.Start();
                PomodoroModeUpdateUI();
            };
        }

        // ポモドーロタイマーの開始
        private void StartPomodoroTimer()
        {
            isWorkTime = true;
            workTimer.Start();
        }

        // ポモドーロタイマーの停止
        private void StopPomodoroTimer()
        {
            workTimer.Stop();
            breakTimer.Stop();
        }

        // ポモドーロモード(画面UI変更)　タイマー・睡眠検知をもとに画面変更
        private void PomodoroModeUpdateUI()
        {
            bool result = IsUserSleepyBasedOnCsv();
            var currentContent = Application.Current.MainWindow.Content;

            if (isWorkTime)
            {
                if (result && !(currentContent is PomodoroWarning))
                {
                    Application.Current.MainWindow.Content = new PomodoroWarning();
                }
                else if (!result && !(currentContent is PomodoroNormal))
                {
                    Application.Current.MainWindow.Content = new PomodoroNormal();
                }
            }
            else
            {
                if (!(currentContent is Break))
                {
                    Application.Current.MainWindow.Content = new Break();
                }
            }

        }


        public void EnableSleepDetection()
        {
            Application.Current.MainWindow.Content = new Normal();
            sleepDetectionActive = true;
            FileUtil.CreateOrClearCsvFile(csvPath);
            App.SleepDetectionProcess.RunAsync(exePath);// パスを指定してexe実行
            timer.Start();
        }

        public void DisableSleepDetection()
        {
            sleepDetectionActive = false;
            App.SleepDetectionProcess.Stop();
            if (timer != null)
            {
                timer.Stop();
            }
        }
    }
}