using System.Diagnostics;
using System.Timers;
using System.Windows;
using MessageBox = System.Windows.MessageBox;
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
            // TODO : 실행시 디아블로4 체크 추가
            MessageBoxResult result = MessageBox.Show("디아블로4가 실행중이지 않습니다","D4Macro", 
                MessageBoxButton.OK, MessageBoxImage.Information);
            if (result == MessageBoxResult.OK)
            {
                if(App.ConfigModel.WithKill) ProcessExited?.Invoke(this, EventArgs.Empty);
            }
            
        }
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}