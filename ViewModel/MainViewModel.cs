using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using D4Macro.Command;
using D4Macro.Model;
using D4Macro.Util;
using Hardcodet.Wpf.TaskbarNotification;
using NAudio.Wave;

namespace D4Macro.ViewModel;

public class MainViewModel : BaseViewModel
{
    private readonly ICommand _shutdownCommand;
    public Action<object> ShutdownAction => _shutdownCommand.Execute;
    public ProcessMonitor ProcessMonitor { get; set; }
    public TaskbarIcon TaskbarIcon { get; set; }
    
    private DispatcherTimer _key1Timer;
    private DispatcherTimer _key2Timer;
    private DispatcherTimer _key3Timer;
    private DispatcherTimer _key4Timer;
    private DispatcherTimer _mouseLeftTimer;
    private DispatcherTimer _mouseRightTimer;
    
    private IWavePlayer _waveOutDevice;
    private WaveStream _waveStream;
    
    private bool _isMacroRunning = false;
    public bool IsMacroRunning
    {
        get { return _isMacroRunning; }
        set
        {
            _isMacroRunning = value;
            if (!IsMacroRunning)
            {
                // off
                PlaySound("pack://application:,,,/Resources/off.mp3");
                _executeButtonText = $"실행({App.ConfigModel.LaunchKey})";
            }
            else
            {
                // on
                PlaySound("pack://application:,,,/Resources/on.mp3");
                _executeButtonText = $"중단({App.ConfigModel.LaunchKey})";
            }
            OnPropertyChanged(nameof(IsMacroRunning));
            OnPropertyChanged(nameof(ButtonText));
        }
    }

    private bool _isProcessRunning = false;

    public bool IsProcessRunning
    {
        get { return _isProcessRunning; }
        set
        {
            if (value == false)
            {
                IsMacroRunning = value;
                ResetTimer();
            }
            _isProcessRunning = value;
            OnPropertyChanged(nameof(IsProcessRunning));
        }
    }

    private string _executeButtonText = $"실행({App.ConfigModel.LaunchKey})";
    
    public string ButtonText
    {
        get { return _executeButtonText;}
        set
        {
            _executeButtonText = value;
            OnPropertyChanged(nameof(ButtonText));
        }
    }

    private DataModel _dataModel;
    public DataModel DataModel
    {
        get { return _dataModel;}
        set
        {
            _dataModel = value;
            OnPropertyChanged(nameof(DataModel));
        }
    }

    private string _dataName;

    public string DataName
    {
        get { return _dataName; }
        set
        {
            _dataName = value;
            OnPropertyChanged(nameof(DataName));
        }
    }

    public MainViewModel()
    {
        DataModel = new DataModel();
        _shutdownCommand = new ShutdownCommand(this);
        InitializeMacroTimers();
        App.ConfigModel.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(App.ConfigModel.LaunchKey))
            {
                if (!IsMacroRunning) ButtonText = $"실행({App.ConfigModel.LaunchKey})";
                else ButtonText = $"중단({App.ConfigModel.LaunchKey})";
            }
        };
    }

    public List<DispatcherTimer> GetAllTimer()
    {
        List<DispatcherTimer> list = new List<DispatcherTimer>();
        list.Add(_key1Timer);
        list.Add(_key2Timer);
        list.Add(_key3Timer);
        list.Add(_key4Timer);
        list.Add(_mouseLeftTimer);
        list.Add(_mouseRightTimer);
        return list;
    }
    
    private void InitializeMacroTimers()
    {
        _key1Timer = new DispatcherTimer();
        _key1Timer.Tick += (sender, e) => ExecuteMacroAction(Key.D1, DataModel.Key1Hold);

        _key2Timer = new DispatcherTimer();
        _key2Timer.Tick += (sender, e) => ExecuteMacroAction(Key.D2, DataModel.Key2Hold);

        _key3Timer = new DispatcherTimer();
        _key3Timer.Tick += (sender, e) => ExecuteMacroAction(Key.D3, DataModel.Key3Hold);

        _key4Timer = new DispatcherTimer();
        _key4Timer.Tick += (sender, e) => ExecuteMacroAction(Key.D4, DataModel.Key4Hold);

        _mouseLeftTimer = new DispatcherTimer();
        _mouseLeftTimer.Tick += (sender, e) => ExecuteMouseAction(true, DataModel.MouseLeftHold);

        _mouseRightTimer = new DispatcherTimer();
        _mouseRightTimer.Tick += (sender, e) => ExecuteMouseAction(false, DataModel.MouseRightHold);
    }
    
    private void ExecuteMacroAction(Key keyCode, bool isHold)
    {
        if (isHold)
            KeyboardSender.HoldKey(keyCode);
        else
            KeyboardSender.TapKey(keyCode);
    }
    
    private void ExecuteMouseAction(bool isLeftClick, bool isHold)
    {
        if (isLeftClick)
        {
            if (isHold) KeyboardSender.HoldLeftMouse();
            else { KeyboardSender.HoldLeftMouse(); KeyboardSender.ReleaseLeftMouse(); }
        }
        else
        {
            if (isHold) KeyboardSender.HoldRightMouse();
            else { KeyboardSender.HoldRightMouse(); KeyboardSender.ReleaseRightMouse(); }
        }
    }
    
    public void ToggleMacro()
    {
        if (!IsProcessRunning)
        {
            MessageBox.Show("디아블로가 실행중이지 않습니다");
            return;
        }
        IsMacroRunning = !IsMacroRunning;
        if (IsMacroRunning)
        {
            if (DataModel.Key1CheckBox == true)
            {
                if (DataModel.Key1Hold)
                {
                    ExecuteMacroAction(Key.D1, true); // 즉시 1회 입력 (딜레이 방지)
                    _key1Timer.Interval = TimeSpan.FromMilliseconds(Const.HOLD_REPEAT_INTERVAL); // 꾹 누르는 상태(OS 연속입력) 구현을 위해 짧은 간격으로 고정
                    _key1Timer.Start();
                }
                else if (DataModel.Key1Interval > 0)
                {
                    _key1Timer.Interval = TimeSpan.FromMilliseconds(DataModel.Key1Interval);
                    _key1Timer.Start();
                }
            }

            if (DataModel.Key2CheckBox == true)
            {
                if (DataModel.Key2Hold)
                {
                    ExecuteMacroAction(Key.D2, true);
                    _key2Timer.Interval = TimeSpan.FromMilliseconds(Const.HOLD_REPEAT_INTERVAL);
                    _key2Timer.Start();
                }
                else if (DataModel.Key2Interval > 0)
                {
                    _key2Timer.Interval = TimeSpan.FromMilliseconds(DataModel.Key2Interval);
                    _key2Timer.Start();
                }
            }

            if (DataModel.Key3CheckBox == true)
            {
                if (DataModel.Key3Hold)
                {
                    ExecuteMacroAction(Key.D3, true);
                    _key3Timer.Interval = TimeSpan.FromMilliseconds(Const.HOLD_REPEAT_INTERVAL);
                    _key3Timer.Start();
                }
                else if (DataModel.Key3Interval > 0)
                {
                    _key3Timer.Interval = TimeSpan.FromMilliseconds(DataModel.Key3Interval);
                    _key3Timer.Start();
                }
            }

            if (DataModel.Key4CheckBox == true)
            {
                if (DataModel.Key4Hold)
                {
                    ExecuteMacroAction(Key.D4, true);
                    _key4Timer.Interval = TimeSpan.FromMilliseconds(Const.HOLD_REPEAT_INTERVAL);
                    _key4Timer.Start();
                }
                else if (DataModel.Key4Interval > 0)
                {
                    _key4Timer.Interval = TimeSpan.FromMilliseconds(DataModel.Key4Interval);
                    _key4Timer.Start();
                }
            }

            if (DataModel.MouseLeftCheckBox == true)
            {
                if (DataModel.MouseLeftHold)
                {
                    ExecuteMouseAction(true, true);
                    _mouseLeftTimer.Interval = TimeSpan.FromMilliseconds(Const.HOLD_REPEAT_INTERVAL);
                    _mouseLeftTimer.Start();
                }
                else if (DataModel.MouseLeftInterval > 0)
                {
                    _mouseLeftTimer.Interval = TimeSpan.FromMilliseconds(DataModel.MouseLeftInterval);
                    _mouseLeftTimer.Start();
                }
            }

            if (DataModel.MouseRightCheckBox == true)
            {
                if (DataModel.MouseRightHold)
                {
                    ExecuteMouseAction(false, true);
                    _mouseRightTimer.Interval = TimeSpan.FromMilliseconds(Const.HOLD_REPEAT_INTERVAL);
                    _mouseRightTimer.Start();
                }
                else if (DataModel.MouseRightInterval > 0)
                {
                    _mouseRightTimer.Interval = TimeSpan.FromMilliseconds(DataModel.MouseRightInterval);
                    _mouseRightTimer.Start();
                }
            }
        }
        else
        {
            ResetTimer();
        }
    }
    
    private void ResetTimer()
    {
        _key1Timer.Stop();
        _key2Timer.Stop();
        _key3Timer.Stop();
        _key4Timer.Stop();
        _mouseLeftTimer.Stop();
        _mouseRightTimer.Stop();

        // 매크로 중단 시 '지속' 모드로 동작 중이었던 항목들만 확실하게 떼어줍니다. 
        // (누르지 않은 키에 대해 떼는 신호를 보내면 브라우저 등이 오작동할 수 있음)
        if (DataModel.Key1CheckBox && DataModel.Key1Hold) KeyboardSender.ReleaseKey(Key.D1);
        if (DataModel.Key2CheckBox && DataModel.Key2Hold) KeyboardSender.ReleaseKey(Key.D2);
        if (DataModel.Key3CheckBox && DataModel.Key3Hold) KeyboardSender.ReleaseKey(Key.D3);
        if (DataModel.Key4CheckBox && DataModel.Key4Hold) KeyboardSender.ReleaseKey(Key.D4);
        if (DataModel.MouseLeftCheckBox && DataModel.MouseLeftHold) KeyboardSender.ReleaseLeftMouse();
        if (DataModel.MouseRightCheckBox && DataModel.MouseRightHold) KeyboardSender.ReleaseRightMouse();
    }
    
    private void PlaySound(string resourceUri)
    {
        try
        {
            var uri = new Uri(resourceUri);
            var resourceInfo = Application.GetResourceStream(uri);

            // 리소스 스트림을 NAudio로 재생합니다
            using (var resourceStream = resourceInfo.Stream)
            {
                _waveOutDevice = new WaveOut();
                _waveStream = new Mp3FileReader(resourceStream);
                _waveOutDevice.Init(_waveStream);
                _waveOutDevice.Play();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error playing sound: {ex.Message}");
        }
    }

    
}