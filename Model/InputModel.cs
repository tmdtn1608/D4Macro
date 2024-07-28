using System.ComponentModel;

namespace D4Macro.Model;

public class InputModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    private bool _key1CheckBox;
    public bool Key1CheckBox {
        get { return _key1CheckBox; }
        set
        {
            _key1CheckBox = value;
            OnPropertyChanged(nameof(Key1CheckBox));
        }
    }

    private int _key1Interval = 0;

    public int key1Interval
    {
        get { return _key1Interval;}
        set
        {
            _key1Interval = value;
            OnPropertyChanged(nameof(key1Interval));
        }
    }
    
    private bool _key2CheckBox;
    public bool Key2CheckBox {
        get { return _key2CheckBox; }
        set
        {
            _key2CheckBox = value;
            OnPropertyChanged(nameof(Key2CheckBox));
        }
    }

    private int _key2Interval = 0;

    public int key2Interval
    {
        get { return _key2Interval;}
        set
        {
            _key2Interval = value;
            OnPropertyChanged(nameof(key2Interval));
        }
    }
    
    private bool _key3CheckBox;
    public bool Key3CheckBox {
        get { return _key3CheckBox; }
        set
        {
            _key3CheckBox = value;
            OnPropertyChanged(nameof(Key3CheckBox));
        }
    }

    private int _key3Interval = 0;

    public int key3Interval
    {
        get { return _key3Interval;}
        set
        {
            _key3Interval = value;
            OnPropertyChanged(nameof(key3Interval));
        }
    }
    
    private bool _key4CheckBox;
    public bool Key4CheckBox {
        get { return _key4CheckBox; }
        set
        {
            _key4CheckBox = value;
            OnPropertyChanged(nameof(Key4CheckBox));
        }
    }

    private int _key4Interval = 0;

    public int key4Interval
    {
        get { return _key4Interval;}
        set
        {
            _key4Interval = value;
            OnPropertyChanged(nameof(key4Interval));
        }
    }

    private bool _mouseLeftCheckBox;
    public bool mouseLeftCheckBox
    {
        get { return _mouseLeftCheckBox; }
        set
        {
            _mouseLeftCheckBox = value;
            OnPropertyChanged(nameof(mouseLeftCheckBox));
        }
    }

    private int _mouseLeftInterval = 0;

    public int mouseLeftInterval
    {
        get { return _mouseLeftInterval; }
        set
        {
            _mouseLeftInterval = value;
            OnPropertyChanged(nameof(mouseLeftInterval));
        }
    }
    
    private bool _mouseRightCheckBox;
    public bool mouseRightCheckBox
    {
        get { return _mouseRightCheckBox; }
        set
        {
            _mouseRightCheckBox = value;
            OnPropertyChanged(nameof(mouseRightCheckBox));
        }
    }

    private int _mouseRightInterval = 0;

    public int mouseRightInterval
    {
        get { return _mouseRightInterval; }
        set
        {
            _mouseRightInterval = value;
            OnPropertyChanged(nameof(mouseRightInterval));
        }
    }

}