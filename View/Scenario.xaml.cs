using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace D4Macro.View;

public partial class Scenario : UserControl, INotifyPropertyChanged
{
    public ObservableCollection<ScenarioItem> Scenarios { get; set; }

    public Scenario()
    {
        InitializeComponent();
        Scenarios = new ObservableCollection<ScenarioItem>();
        // 초기 행 추가
        AddScenarioRow();
        DataContext = this;
    }

    private void AddScenarioRow()
    {
        var newItem = new ScenarioItem();
        // 이전 아이템들의 IsLastItem 속성을 false로 설정
        foreach (var item in Scenarios)
        {
            item.IsLastItem = false;
        }
        newItem.IsLastItem = true;
        Scenarios.Add(newItem);
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        AddScenarioRow();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

public class ScenarioItem : INotifyPropertyChanged
{
    private string _selectedCondition;
    public string SelectedCondition
    {
        get => _selectedCondition;
        set
        {
            if (_selectedCondition != value)
            {
                _selectedCondition = value;
                OnPropertyChanged();
                UpdateVisibility();
            }
        }
    }

    private string _triggerButton;
    public string TriggerButton
    {
        get => _triggerButton;
        set
        {
            if (_triggerButton != value)
            {
                _triggerButton = value;
                OnPropertyChanged();
            }
        }
    }

    private string _timeoutValue;
    public string TimeoutValue
    {
        get => _timeoutValue;
        set
        {
            if (_timeoutValue != value)
            {
                _timeoutValue = value;
                OnPropertyChanged();
            }
        }
    }

    private string _actionButton;
    public string ActionButton
    {
        get => _actionButton;
        set
        {
            if (_actionButton != value)
            {
                _actionButton = value;
                OnPropertyChanged();
            }
        }
    }

    private bool _isTriggerVisible;
    public bool IsTriggerVisible
    {
        get => _isTriggerVisible;
        set
        {
            if (_isTriggerVisible != value)
            {
                _isTriggerVisible = value;
                OnPropertyChanged();
            }
        }
    }

    private bool _isTimeoutVisible;
    public bool IsTimeoutVisible
    {
        get => _isTimeoutVisible;
        set
        {
            if (_isTimeoutVisible != value)
            {
                _isTimeoutVisible = value;
                OnPropertyChanged();
            }
        }
    }

    private bool _isLastItem;
    public bool IsLastItem
    {
        get => _isLastItem;
        set
        {
            if (_isLastItem != value)
            {
                _isLastItem = value;
                OnPropertyChanged();
            }
        }
    }

    public ObservableCollection<string> Conditions { get; } = new ObservableCollection<string> { "버튼입력", "시간초과" };
    public ObservableCollection<string> ButtonInputs { get; } = new ObservableCollection<string> { "1", "2", "3", "4" };

    public ScenarioItem()
    {
        SelectedCondition = "버튼입력";
        TriggerButton = "1";
        ActionButton = "1";
    }

    private void UpdateVisibility()
    {
        if (SelectedCondition == "버튼입력")
        {
            IsTriggerVisible = true;
            IsTimeoutVisible = false;
        }
        else
        {
            IsTriggerVisible = false;
            IsTimeoutVisible = true;
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}