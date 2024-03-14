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
        private DispatcherTimer timer;
        private bool sleepDetectionActive = false;
        private string exePath = "mydata/eye_blink_detecterv2.exe";
        private string csvPath = "mydata/output.csv";

        public MainWindow()
        {
            InitializeComponent();
            this.Closing += MainWindow_Closing;
            SetupTimer();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.SleepDetectionProcess.Stop();
        }

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
                EvaluateSleepStatusAndUpdateUI();
            }
        }

        private void EvaluateSleepStatusAndUpdateUI()
        {
            string filePath = csvPath; // CSVファイルのパスを指定
            int numberOfLines = 15; // 読み込む行数
            double threshold = 0.5; // trueの割合の閾値

            bool result = FileUtil.CheckTruePercentageInRecentLines(filePath, numberOfLines, threshold);
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