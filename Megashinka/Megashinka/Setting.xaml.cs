﻿using Microsoft.Win32;
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
using System.Diagnostics;

namespace Megashinka
{
    /// <summary>
    /// Setting.xaml の相互作用ロジック
    /// </summary>
    public partial class Setting : Page
    {
        private int volume;
        private string alarmSound;
        public Setting()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            //　設定ファイルを呼び出しVolumeを設定
            volume = int.Parse(SettingsManager.GetSettingValueByKey("volume"));
            App.VolumeController.SetVolume(volume);
            slider.Value = volume;
            //　設定ファイルを呼び出しラジオボタンを選択
            string alarmSound = SettingsManager.GetSettingValueByKey("alarmSound");
            switch (alarmSound)
            {
                case "mydata/sound/Warning.mp3":
                    RadioButton1.IsChecked = true;
                    break;
                case "mydata/sound/sound01.mp3":
                    RadioButton2.IsChecked = true;
                    break;
                case "mydata/sound/voice01.mp3":
                    RadioButton3.IsChecked = true;
                    break;
                default:
                    RadioButton1.IsChecked = true;
                    break;
            }
        }

        private void SaveSettings()
        {
            SettingsManager.UpdateSettingValueByKey("volume", volume.ToString());
            SettingsManager.UpdateSettingValueByKey("alarmSound", alarmSound);
        }

        private void Slider_Change(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // スライダーの値を取得してint型に変換し、値を表示するTextBlockに設定する
            int volume = (int)slider.Value;
            // 音量を設定
            App.VolumeController.SetVolume(volume);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new Home();
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            alarmSound = "mydata/sound/Warning.mp3";
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            alarmSound = "mydata/sound/sound01.mp3";
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            alarmSound = "mydata/sound/voice01.mp3";
        }

        public void FileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            openFileDialog.Filter = "MP3 ファイル|*.mp3"; // 拡張子が .mp3 のファイルのみを表示
            openFileDialog.FilterIndex = 1; // フィルターの初期選択を指定
            openFileDialog.Multiselect = false; // 複数のファイルの選択を許可するかどうかを指定

            if (openFileDialog.ShowDialog() == true)
            {
                RadioButton1.IsChecked = false;
                RadioButton2.IsChecked = false;
                RadioButton3.IsChecked = false;
                alarmSound = openFileDialog.FileName; // alarmSoundにファイルパスを格納
                string selectedFilePath = openFileDialog.FileName;
                
            }
        }

        private void PreviewButton_Click(object sender, RoutedEventArgs e)
        {
            App.BgmPlayer.Stop();
            App.BgmPlayer.IsRepeating = false;
            App.BgmPlayer.PlayMp3File(alarmSound);
        }
    }
}
