using System.Configuration;
using System.Data;
using System.Windows;

namespace Megashinka
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Mp3Player BgmPlayer = new Mp3Player();
        public static ScriptRunner SleepDetectionProcess = new ScriptRunner();
        //public static ScriptRunnerPython SleepDetectionProcess = new ScriptRunnerPython();
        public static VolumeController VolumeController = new VolumeController();
    }

}
