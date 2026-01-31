using System.Collections.ObjectModel;
using D4Macro.Model;

namespace D4Macro.ViewModel;

public class ScenarioViewModel : BaseViewModel
{
    public ScenarioModel Model { get; }

    public ScenarioViewModel()
    {
        Model = new ScenarioModel();
        UpdateVisibility();
    }

    public string SelectedCondition
    {
        get => Model.SelectedCondition;
        set
        {
            if (Model.SelectedCondition != value)
            {
                Model.SelectedCondition = value;
                OnPropertyChanged();
                UpdateVisibility();
            }
        }
    }

    public string TriggerButton
    {
        get => Model.TriggerButton;
        set
        {
            if (Model.TriggerButton != value)
            {
                Model.TriggerButton = value;
                OnPropertyChanged();
            }
        }
    }

    public string TimeoutValue
    {
        get => Model.TimeoutValue;
        set
        {
            if (Model.TimeoutValue != value)
            {
                Model.TimeoutValue = value;
                OnPropertyChanged();
            }
        }
    }

    public string ActionButton
    {
        get => Model.ActionButton;
        set
        {
            if (Model.ActionButton != value)
            {
                Model.ActionButton = value;
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

    private bool _isAddButtonVisible;
    public bool IsAddButtonVisible
    {
        get => _isAddButtonVisible;
        set
        {
            if (_isAddButtonVisible != value)
            {
                _isAddButtonVisible = value;
                OnPropertyChanged();
            }
        }
    }

    private bool _isDeleteButtonVisible;
    public bool IsDeleteButtonVisible
    {
        get => _isDeleteButtonVisible;
        set
        {
            if (_isDeleteButtonVisible != value)
            {
                _isDeleteButtonVisible = value;
                OnPropertyChanged();
            }
        }
    }

    public ObservableCollection<string> Conditions { get; } = new ObservableCollection<string> { "버튼입력", "시간초과" };
    public ObservableCollection<string> ButtonInputs { get; } = new ObservableCollection<string> { "1", "2", "3", "4" };

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
}