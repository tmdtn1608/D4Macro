using Newtonsoft.Json;

namespace D4Macro.Model;

public class DataModel : BaseModel
{
    private bool _key1CheckBox;
    [JsonProperty(nameof(Key1CheckBox))]
    public bool Key1CheckBox {
        get { return _key1CheckBox; }
        set
        {
            _key1CheckBox = value;
            OnPropertyChanged(nameof(Key1CheckBox));
        }
    }

    private int _key1Interval = 0;
    [JsonProperty(nameof(Key1Interval))]
    public int Key1Interval
    {
        get { return _key1Interval;}
        set
        {
            _key1Interval = value;
            OnPropertyChanged(nameof(Key1Interval));
        }
    }
    
    private bool _key2CheckBox;
    [JsonProperty(nameof(Key2CheckBox))]
    public bool Key2CheckBox {
        get { return _key2CheckBox; }
        set
        {
            _key2CheckBox = value;
            OnPropertyChanged(nameof(Key2CheckBox));
        }
    }

    private int _key2Interval = 0;
    [JsonProperty(nameof(Key2Interval))]
    public int Key2Interval
    {
        get { return _key2Interval;}
        set
        {
            _key2Interval = value;
            OnPropertyChanged(nameof(Key2Interval));
        }
    }
    
    private bool _key3CheckBox;
    [JsonProperty(nameof(Key3CheckBox))]
    public bool Key3CheckBox {
        get { return _key3CheckBox; }
        set
        {
            _key3CheckBox = value;
            OnPropertyChanged(nameof(Key3CheckBox));
        }
    }

    private int _key3Interval = 0;
    [JsonProperty(nameof(Key3Interval))]
    public int Key3Interval
    {
        get { return _key3Interval;}
        set
        {
            _key3Interval = value;
            OnPropertyChanged(nameof(Key3Interval));
        }
    }
    
    private bool _key4CheckBox;
    [JsonProperty(nameof(Key4CheckBox))]
    public bool Key4CheckBox {
        get { return _key4CheckBox; }
        set
        {
            _key4CheckBox = value;
            OnPropertyChanged(nameof(Key4CheckBox));
        }
    }

    private int _key4Interval = 0;
    [JsonProperty(nameof(Key4Interval))]
    public int Key4Interval
    {
        get { return _key4Interval;}
        set
        {
            _key4Interval = value;
            OnPropertyChanged(nameof(Key4Interval));
        }
    }

    private bool _mouseLeftCheckBox;
    [JsonProperty(nameof(MouseLeftCheckBox))]
    public bool MouseLeftCheckBox
    {
        get { return _mouseLeftCheckBox; }
        set
        {
            _mouseLeftCheckBox = value;
            OnPropertyChanged(nameof(MouseLeftCheckBox));
        }
    }

    private int _mouseLeftInterval = 0;
    [JsonProperty(nameof(MouseLeftInterval))]
    public int MouseLeftInterval
    {
        get { return _mouseLeftInterval; }
        set
        {
            _mouseLeftInterval = value;
            OnPropertyChanged(nameof(MouseLeftInterval));
        }
    }
    
    private bool _mouseRightCheckBox;
    [JsonProperty(nameof(MouseRightCheckBox))]
    public bool MouseRightCheckBox
    {
        get { return _mouseRightCheckBox; }
        set
        {
            _mouseRightCheckBox = value;
            OnPropertyChanged(nameof(MouseRightCheckBox));
        }
    }

    private int _mouseRightInterval = 0;
    [JsonProperty(nameof(MouseRightInterval))]
    public int MouseRightInterval
    {
        get { return _mouseRightInterval; }
        set
        {
            _mouseRightInterval = value;
            OnPropertyChanged(nameof(MouseRightInterval));
        }
    }
}