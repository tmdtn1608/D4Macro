using System.Diagnostics;
using System.Timers;
using Timer = System.Timers.Timer;

namespace D4Macro.Util;

public class ProcessMonitor : IDisposable
{
    public event EventHandler ProcessExited;

    private readonly string _processName;
    private readonly Timer _timer;

    public ProcessMonitor(string processName)
    {
        _processName = processName;
        _timer = new Timer(1000);
        _timer.Elapsed += CheckProcess;
        _timer.Start();
    }

    private void CheckProcess(object sender, ElapsedEventArgs e)
    {
        var processes = Process.GetProcessesByName(_processName);
        
        if (!processes.Any() && App.ConfigModel.WithKill)
        {
            _timer.Stop();
            ProcessExited?.Invoke(this, EventArgs.Empty);
        }
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}