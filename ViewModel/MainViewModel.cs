using System.Windows.Input;
using System.Windows.Threading;
using D4Macro.Model;
using D4Macro.Util;
using Hardcodet.Wpf.TaskbarNotification;
using WindowsInput;
using WindowsInput.Native;

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
    
    private bool _isMacroRunning = false;
    public bool IsMacroRunning
    {
        get { return _isMacroRunning; }
        set
        {
            _isMacroRunning = value;
            if (!IsMacroRunning) _executeButtonText = "실행";
            else _executeButtonText = "중단";
            OnPropertyChanged(nameof(ButtonText));
        }
    }
    private string _executeButtonText = "실행";
    
    public string ButtonText
    {
        get { return _executeButtonText;}
        set { _executeButtonText = value; }
    }

    private InputModel _inputModel;
    public InputModel InputModel
    {
        get { return _inputModel;}
        set
        {
            _inputModel = value;
            OnPropertyChanged(nameof(InputModel));
        }
    }

    public MainViewModel()
    {
        InputModel = new InputModel();
        _shutdownCommand = new ShutdownCommand(this);
        InitializeMacroTimers();
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
        _key1Timer.Tick += (sender, e) => ExecuteMacroAction(VirtualKeyCode.VK_1);

        _key2Timer = new DispatcherTimer();
        _key2Timer.Tick += (sender, e) => ExecuteMacroAction(VirtualKeyCode.VK_2);

        _key3Timer = new DispatcherTimer();
        _key3Timer.Tick += (sender, e) => ExecuteMacroAction(VirtualKeyCode.VK_3);

        _key4Timer = new DispatcherTimer();
        _key4Timer.Tick += (sender, e) => ExecuteMacroAction(VirtualKeyCode.VK_4);

        _mouseLeftTimer = new DispatcherTimer();
        _mouseLeftTimer.Tick += (sender, e) => ExecuteMouseAction(true);

        _mouseRightTimer = new DispatcherTimer();
        _mouseRightTimer.Tick += (sender, e) => ExecuteMouseAction(false);
        
        
    }
    
    private void ExecuteMacroAction(VirtualKeyCode keyCode)
    {
        InputSimulator sim = new InputSimulator();
        sim.Keyboard.KeyPress(keyCode);
    }
    
    private void ExecuteMouseAction(bool isLeftClick)
    {
        InputSimulator sim = new InputSimulator();
        if (isLeftClick)
        {
            sim.Mouse.LeftButtonClick();
        }
        else
        {
            sim.Mouse.RightButtonClick();
        }
    }
    
    public void ToggleMacro()
    {
        IsMacroRunning = !IsMacroRunning;
        if (IsMacroRunning)
        {
            if (InputModel.Key1CheckBox == true && InputModel.Key1Interval != 0)
            {
                _key1Timer.Interval = TimeSpan.FromMilliseconds(InputModel.Key1Interval);
                _key1Timer.Start();
            }

            if (InputModel.Key2CheckBox == true && InputModel.Key2Interval != 0)
            {
                _key2Timer.Interval = TimeSpan.FromMilliseconds(InputModel.Key2Interval);
                _key2Timer.Start();
            }

            if (InputModel.Key3CheckBox == true && InputModel.Key3Interval != 0)
            {
                _key3Timer.Interval = TimeSpan.FromMilliseconds(InputModel.Key3Interval);
                _key3Timer.Start();
            }

            if (InputModel.Key4CheckBox == true && InputModel.Key4Interval != 0)
            {
                _key4Timer.Interval = TimeSpan.FromMilliseconds(InputModel.Key4Interval);
                _key4Timer.Start();
            }

            if (InputModel.MouseLeftCheckBox == true && InputModel.MouseLeftInterval != 0)
            {
                _mouseLeftTimer.Interval = TimeSpan.FromMilliseconds(InputModel.MouseLeftInterval);
                _mouseLeftTimer.Start();
            }

            if (InputModel.MouseRightCheckBox == true && InputModel.MouseRightInterval != 0)
            {
                _mouseRightTimer.Interval = TimeSpan.FromMilliseconds(InputModel.MouseRightInterval);
                _mouseRightTimer.Start();
            }
        }
        else
        {
            _key1Timer.Stop();
            _key2Timer.Stop();
            _key3Timer.Stop();
            _key4Timer.Stop();
            _mouseLeftTimer.Stop();
            _mouseRightTimer.Stop();
        }
    }

    
}