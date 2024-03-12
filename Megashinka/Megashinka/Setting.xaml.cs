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
        public Setting()
        {
            InitializeComponent();
        }



        private void Slider_Change(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // スライダーの値を取得してint型に変換し、値を表示するTextBlockに設定する
            int sliderValue = (int)slider.Value;
            

            
        }





        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new Home();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            //ラジオボタンを選択してアラーム音を切り替える処理
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            //ラジオボタンを選択してアラーム音を切り替える処理
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            //ラジオボタンを選択してアラーム音を切り替える処理
        }

    }
}
