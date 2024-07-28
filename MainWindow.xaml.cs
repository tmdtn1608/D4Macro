using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using D4Macro.Model;
using D4Macro.ViewModel;
using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Win32;
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
    private void ApplicationTerminate(object sender, RoutedEventArgs e)
    {
        ApplicationTerminate();
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

    private void Import_Setting(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "MacroData (*.json)|*.json";
        openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        if (openFileDialog.ShowDialog() == true)
        {
            string filePath = openFileDialog.FileName;
            try
            {
                InputModel jsonObject = InputModel.LoadSettings(filePath);
                _mainViewModel.InputModel = jsonObject;
            }
            catch (Exception ex)
            {
                MessageBox.Show("매크로 데이터 불러오기 실패: " + ex.Message);
            }
        }
    }

    private void Export_Setting(object sender, RoutedEventArgs e)
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "MacroData (*.json)|*.json";
        saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        if (saveFileDialog.ShowDialog() == true)
        {
            string filePath = saveFileDialog.FileName;
            try
            {
                InputModel.SaveSettings(_mainViewModel.InputModel,filePath);
                MessageBox.Show("매크로 데이터 저장 성공");
            }
            catch (Exception ex)
            {
                MessageBox.Show("매크로 데이저 저장 실패: " + ex.Message);
            }
        }
    }

    private void SetConfig(object sender, RoutedEventArgs e)
    {
        SettingWindow settingsWindow = new SettingWindow();
        settingsWindow.ShowDialog();
    }
}