using System.Windows.Input;
using Newtonsoft.Json;

namespace D4Macro.Model;

public class ConfigModel : BaseModel
{
    private Key _launchKey = Key.F12;
    
    [JsonProperty(nameof(LaunchKey))]
    public Key LaunchKey
    {
        get { return _launchKey;}
        set
        {
            _launchKey = value;
            OnPropertyChanged(nameof(ButtonText));
            OnPropertyChanged(nameof(LaunchKey));
        }
    }

    private bool _activeTray = true;

    [JsonProperty(nameof(ActiveTray))]
    public bool ActiveTray
    {
        get { return _activeTray; }
        set
        {
            _activeTray = value;
            OnPropertyChanged(nameof(ActiveTray));
        }
    }

    private bool _withKill = true;

    [JsonProperty(nameof(WithKill))]
    public bool WithKill
    {
        get { return _withKill; }
        set
        {
            _withKill = value;
            OnPropertyChanged(nameof(WithKill));
        }
    }

    private bool _initRunCheck = false;

    [JsonProperty(nameof(InitRunCheck))]
    public bool InitRunCheck
    {
        get { return _initRunCheck; }
        set
        {
            _initRunCheck = value;
            OnPropertyChanged(nameof(InitRunCheck));
        }
    }
    
    [JsonIgnore]
    public string ButtonText
    {
        get
        {
            return $"({LaunchKey.ToString()})";
        }
    }
}