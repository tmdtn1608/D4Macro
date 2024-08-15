using System.Diagnostics;
using System.Timers;
using System.Windows;
using D4Macro.ViewModel;
using MessageBox = System.Windows.MessageBox;
using Timer = System.Timers.Timer;

namespace D4Macro.Util;

public class ProcessMonitor : IDisposable
{
    public event EventHandler ProcessExited;

    private readonly string _processName;
    private readonly Timer _timer;
    private MainViewModel _mainViewModel;
    private bool _isPaused;

    public ProcessMonitor(string processName, MainViewModel mainViewModel)
    {
        _processName = processName;
        _mainViewModel = mainViewModel;
        _timer = new Timer(1000);
        _timer.Elapsed += CheckProcess;
        _timer.Start();
        _isPaused = false;
    }

    private void CheckProcess(object sender, ElapsedEventArgs e)
    {
        var processes = Process.GetProcessesByName(_processName);
        
        if (!processes.Any())
        {
            // 실행중인 상태에서
            if (_mainViewModel.IsProcessRunning)
            {
                // 종료시켰을때.
                if (App.ConfigModel.WithKill)
                {
                    _timer.Stop();
                    ProcessExited?.Invoke(this, EventArgs.Empty);
                }
            }
            // 실행중이지 않은 상태에서
            else
            {
                // 타이머 일시중지상태가 아니고 프로세스가 실행되지 않았을때. 
                // 일시중지상태라면 이 로직은 더이상 실행되지 않는다.
                if (App.ConfigModel.InitRunCheck && !_isPaused)
                {
                    // 일시중지상태로 만들고
                    _isPaused = true;
                    // 메시지박스 출력
                    MessageBoxResult result = MessageBox.Show("디아블로4가 실행중이지 않습니다","D4Macro", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            if(_mainViewModel.IsProcessRunning) _mainViewModel.IsProcessRunning = false;
        }
        else
        {
            if(!_isPaused) _isPaused = true;
            _mainViewModel.IsProcessRunning = true;
        }
        
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}