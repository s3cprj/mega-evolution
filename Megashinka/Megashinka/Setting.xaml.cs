using Microsoft.Win32;
using System;
using System.IO;
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
using System.Diagnostics;

namespace Megashinka
{
    /// <summary>
    /// Setting.xaml の相互作用ロジック
    /// </summary>
    public partial class Setting : Page
    {
        private int volume;
        private double sleepThreshold;
        private string alarmSound;
        public Setting()
        {
            InitializeComponent();
            LoadSettings();
            // ここでユーザー設定音の存在を確認して、RadioButton3の有効/無効を設定
            CheckUserSoundAvailability();
        }

        private void LoadSettings()
        {
            //　設定ファイルを呼び出しsleepThresholdを設定
            sleepThreshold = double.Parse(SettingsManager.GetSettingValueByKey("sleepThreshold"));
            sleepThreshold = (int)(100 - sleepThreshold * 100);
            Slider0.Value = sleepThreshold;
            //　設定ファイルを呼び出しVolumeを設定
            volume = int.Parse(SettingsManager.GetSettingValueByKey("volume"));
            App.VolumeController.SetVolume(volume);
            Slider1.Value = volume;
            //　設定ファイルを呼び出しラジオボタンを選択
            string alarmSound = SettingsManager.GetSettingValueByKey("alarmSound");
            switch (alarmSound)
            {
                case "mydata/sound/Warning.mp3":
                    RadioButton1.IsChecked = true;
                    break;
                case "mydata/sound/Voice.mp3":
                    RadioButton2.IsChecked = true;
                    break;
                case "mydata/sound/UserSound.mp3":
                    RadioButton3.IsChecked = true;
                    break;
                default:
                    RadioButton1.IsChecked = true;
                    break;
            }
        }

        private void SaveSettings()
        {
            int volume = (int)Slider1.Value;
            SettingsManager.UpdateSettingValueByKey("volume", volume.ToString());
            SettingsManager.UpdateSettingValueByKey("alarmSound", alarmSound);
            double sleepThreshold = 100 - (int)Slider0.Value;
            sleepThreshold = (double)(sleepThreshold / 100);
            SettingsManager.UpdateSettingValueByKey("sleepThreshold", sleepThreshold.ToString());
            App.BgmPlayer.Stop();
        }

        private void Slider0_Change(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // スライダーの値が変更されたときの処理
        }

        private void Slider1_Change(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // スライダーの値を取得してint型に変換
            int volume = (int)Slider1.Value;
            // 音量を設定
            App.VolumeController.SetVolume(volume);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
            Application.Current.MainWindow.Content = new Home();
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            alarmSound = "mydata/sound/Warning.mp3";
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            alarmSound = "mydata/sound/Voice.mp3";
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            alarmSound = "mydata/sound/UserSound.mp3";
        }

        public void FileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            openFileDialog.Filter = "MP3 ファイル|*.mp3"; // 拡張子が .mp3 のファイルのみを表示
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == true)
            {
                string sourceFilePath = openFileDialog.FileName; // ユーザーが選択したファイルのパス
                string targetDirectory = "mydata/sound/"; // ファイルを保存するディレクトリ
                string targetFilePath = System.IO.Path.Combine(targetDirectory, "UserSound.mp3"); // 保存するファイルの完全なパス
                // ファイルを指定したパスにコピー (既に存在する場合は上書き)
                System.IO.File.Copy(sourceFilePath, targetFilePath, true);
                CheckUserSoundAvailability();
                if (IsUserSoundExists()) { RadioButton3.IsChecked = true; }
            }
        }

        private bool IsUserSoundExists()
        {
            string filePath = "mydata/sound/UserSound.mp3";
            return File.Exists(filePath);
        }

        private void CheckUserSoundAvailability()
        {
            // ユーザー設定音が存在する場合ラジオボタンを有効化する。
            bool userSoundExists = IsUserSoundExists();
            RadioButton3.IsEnabled = userSoundExists;
        }

        private void PreviewButton_Click(object sender, RoutedEventArgs e)
        {
            App.BgmPlayer.Stop();
            App.BgmPlayer.IsRepeating = false;
            App.BgmPlayer.PlayMp3File(alarmSound);
        }
    }
}
