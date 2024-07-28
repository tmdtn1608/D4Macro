using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using D4Macro.ViewModel;
using Hardcodet.Wpf.TaskbarNotification;
using NHotkey;
using NHotkey.Wpf;

namespace D4Macro;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private TaskbarIcon _notifyIcon;
    private ProcessMonitor _processMonitor;
    private bool _isApplicationClosing;
    private KeyboardListener _keyboardListener;
    private const string ProcessName = "Diablo IV";
    private const int HOTKEY_ID = 9000;
    private const uint MOD_NOREPEAT = 0x4000;
    // private const uint VK_F12 = 0x7B;
    private const uint VK_F12 = 0x73;
    private Icon icon = new System.Drawing.Icon(Application.GetResourceStream(new Uri("pack://application:,,,/Resources/devil.ico")).Stream);
    private MainViewModel _mainViewModel;
    public MainWindow()
    {
        InitializeComponent();
        InitializeTrayIcon();
        CheckTargetProcess();
        InitializeKeyboardHook();
        _mainViewModel = new MainViewModel();
        DataContext = _mainViewModel;
    }

    private void InitializeTrayIcon()
    {
        _notifyIcon = new TaskbarIcon
        {
            Icon = icon,
            ToolTipText = "D4Macro"
        };
        _notifyIcon.TrayMouseDoubleClick += NotifyIcon_TrayMouseDoubleClick;

        ContextMenu contextMenu = new ContextMenu();
        MenuItem exitMenuItem = new MenuItem { Header = "Exit" };
        exitMenuItem.Click += (sender, args) => ApplicationTerminate();
        contextMenu.Items.Add(exitMenuItem);
        _notifyIcon.ContextMenu = contextMenu;
    }
    
    private void InitializeKeyboardHook()
    {
        _keyboardListener = new KeyboardListener();
        _keyboardListener.KeyPressed += OnKeyPressed;
    }

    private void OnKeyPressed(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.F12)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.ToggleMacro();
        }
    }

    private void NotifyIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
    {
        Show();
        WindowState = WindowState.Normal;
        ShowInTaskbar = true;
    }

    private void Window_StateChanged(object sender, EventArgs e)
    {
        if (WindowState == WindowState.Minimized)
        {
            Hide();
            ShowInTaskbar = false;
        }
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        // HotkeyManager.Current.Remove("ToggleMacro");
        // _processMonitor?.Dispose();
        // _notifyIcon?.Dispose();
        if (!_isApplicationClosing)
        {
            e.Cancel = true;
            Hide();
            ShowInTaskbar = false;
        }
        else
        {
            ApplicationTerminate();
        }
    }

    private void ApplicationTerminate()
    {
        HotkeyManager.Current.Remove("ToggleMacro");
        _processMonitor?.Dispose();
        _notifyIcon?.Dispose();
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

    private void CheckTargetProcess()
    {
        _processMonitor = new ProcessMonitor("chrome");
        _processMonitor.ProcessExited += (s, e) =>
        {
            _isApplicationClosing = true;
            Application.Current.Dispatcher.Invoke(() => Application.Current.Shutdown());
        };
    }

    private void OnToggleMacro(object sender, HotkeyEventArgs e)
    {
        var viewModel = (MainViewModel)DataContext;
        viewModel.ToggleMacro();
        e.Handled = true;
    }
}