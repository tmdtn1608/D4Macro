using System.IO;
using System.Windows;
using D4Macro.Model;
using D4Macro.Util;

namespace D4Macro;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{


    public static ConfigModel ConfigModel { get; set; } = new ConfigModel();
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
            if (!File.Exists(Const.SETTING_FILE_PATH))
            {
                JsonController.Instance.WriteJson(ConfigModel, Const.SETTING_FILE_PATH);
            }
            else
            {
                ConfigModel = JsonController.Instance.ReadJson<ConfigModel>(Const.SETTING_FILE_PATH);
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
}