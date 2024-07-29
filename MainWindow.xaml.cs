using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using D4Macro;
using D4Macro.Model;
using D4Macro.Util;
using D4Macro.ViewModel;
using D4Macro.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Win32;

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
    private readonly Icon _icon = new Icon(Application.GetResourceStream(new Uri("pack://application:,,,/Resources/devil.ico")).Stream);
    private MainViewModel _mainViewModel;
    
    public MainWindow()
    {
        InitializeComponent();
        _mainViewModel = new MainViewModel();
        InitializeTrayIcon();
        CheckTargetProcess();
        InitializeKeyboardHook();
        
        DataContext = _mainViewModel;
    }

    private void InitializeTrayIcon()
    {
        _notifyIcon = new TaskbarIcon
        {
            Icon = _icon,
            ToolTipText = "D4Macro"
        };
        _notifyIcon.TrayMouseDoubleClick += NotifyIcon_TrayMouseDoubleClick;

        ContextMenu contextMenu = new ContextMenu();
        MenuItem exitMenuItem = new MenuItem { Header = "Exit" };
        exitMenuItem.Click += (sender, args) => ApplicationTerminate();
        contextMenu.Items.Add(exitMenuItem);
        _notifyIcon.ContextMenu = contextMenu;
        
        _mainViewModel.TaskbarIcon = _notifyIcon;
    }
    private void NotifyIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
    {
        Show();
        WindowState = WindowState.Normal;
        ShowInTaskbar = true;
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
        _mainViewModel.ShutdownAction?.Invoke(null);
    }
    private void ApplicationTerminate(object sender, RoutedEventArgs e)
    {
        _mainViewModel.ShutdownAction?.Invoke(null);
    }

    private void CheckTargetProcess()
    {
        _processMonitor = new ProcessMonitor("notepad");
        _processMonitor.ProcessExited += (s, e) =>
        {
            _isApplicationClosing = true;
            Application.Current.Dispatcher.Invoke(() => Application.Current.Shutdown());
        };
        _mainViewModel.ProcessMonitor = _processMonitor;
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
                _mainViewModel.DataModel = JsonController.Instance.ReadJson<DataModel>(filePath);
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
                JsonController.Instance.WriteJson(_mainViewModel.DataModel,filePath);
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

    private void ViewInfo(object sender, RoutedEventArgs e)
    {
        InfoWindow infoWindow = new InfoWindow();
        infoWindow.ShowDialog();
    }
}