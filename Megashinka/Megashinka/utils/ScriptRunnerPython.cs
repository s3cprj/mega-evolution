using System.Diagnostics;
using System.Threading.Tasks;

public class ScriptRunnerPython
{
    private Process runningProcess = null;

    public async Task RunAsync(string scriptPath)
    {
        runningProcess = new Process();

        // Python実行可能ファイルのパスを指定します。環境によってはフルパスが必要になる場合があります。
        runningProcess.StartInfo.FileName = "python";

        // 実行するスクリプトのパスを引数として指定します。
        runningProcess.StartInfo.Arguments = scriptPath;

        runningProcess.StartInfo.UseShellExecute = false;
        runningProcess.StartInfo.RedirectStandardOutput = false;
        runningProcess.StartInfo.RedirectStandardError = true;
        runningProcess.StartInfo.CreateNoWindow = true;
        runningProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        runningProcess.Start();
    }

    public void Stop()
    {
        if (runningProcess != null && !runningProcess.HasExited)
        {
            runningProcess.Kill(true);
            runningProcess.Dispose();
            runningProcess = null;
        }
    }
}