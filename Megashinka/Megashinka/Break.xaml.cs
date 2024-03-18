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
    /// Break.xaml の相互作用ロジック
    /// </summary>
    public partial class Break : Page
    {
        private DispatcherTimer timer;
        public Break()
        {
            InitializeComponent();
            SetupTimer();
        }

        private void BackIcon_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new Home();
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
    }
}
