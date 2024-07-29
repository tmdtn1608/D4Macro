using System.Windows;
using System.Windows.Input;
using D4Macro.ViewModel;
using NHotkey.Wpf;

namespace D4Macro.Command;

public class ShutdownCommand : ICommand
{
    private MainViewModel _mainViewModel;
    public ShutdownCommand(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }
    public bool CanExecute(object? parameter){ return true; }

    public void Execute(object? parameter)
    {
        HotkeyManager.Current.Remove("ToggleMacro");
            
        _mainViewModel.ProcessMonitor?.Dispose();
        _mainViewModel.TaskbarIcon?.Dispose();
        // 타이머가 있다면 종료
        foreach (var dispatcherTimer in _mainViewModel.GetAllTimer())
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