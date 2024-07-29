using System.Windows;
using System.Windows.Input;
using D4Macro.ViewModel;

namespace D4Macro.Command;

public class ShutdownCommand(MainViewModel mainViewModel) : ICommand
{
    public bool CanExecute(object? parameter){ return true; }

    public void Execute(object? parameter)
    {
        mainViewModel.ProcessMonitor?.Dispose();
        mainViewModel.TaskbarIcon?.Dispose();
        // 타이머가 있다면 종료
        foreach (var dispatcherTimer in mainViewModel.GetAllTimer())
        {
            dispatcherTimer?.Stop();
        }

        // Dispatcher 종료
        Application.Current.Dispatcher.InvokeShutdown();

        // 애플리케이션 종료
        Application.Current.Shutdown();
    }

    public event EventHandler? CanExecuteChanged;
}