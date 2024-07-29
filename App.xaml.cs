using System.IO;
using System.Windows;
using D4Macro.Model;
using Newtonsoft.Json;

namespace D4Macro;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static readonly string SettingsFilePath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "D4Macro", "config.json");
    public static readonly string DataFilePath = 
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "D4Macro","quickSave.json");
    private static ConfigModel _configModel = new ConfigModel();

    public static ConfigModel ConfigModel
    {
        get { return _configModel; }
        set { _configModel = value; }
    }
    public App()
    {
        InitializeConfig();
    }

    private void InitializeConfig()
    {
        try
        {
            string appDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "D4Macro");
            if (!Directory.Exists(appDir))
            {
                Directory.CreateDirectory(appDir);
            }
            if (!File.Exists(SettingsFilePath))
            {
                CreateJson();
            }
            else
            {
                var config = File.ReadAllText(SettingsFilePath);
                ConfigModel = JsonConvert.DeserializeObject<ConfigModel>(config);
            }
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            MessageBoxResult result = MessageBox.Show("설정파일을 불러오는데 실패했습니다", 
                "Failed", MessageBoxButton.OK,
                MessageBoxImage.Information);
            if (result == MessageBoxResult.OK)
            {
                Current.Shutdown();
            }
        }
    }

    private void CreateJson()
    {
        var config = JsonConvert.SerializeObject(ConfigModel, Formatting.Indented);
        File.WriteAllText(SettingsFilePath, config);
    }
}