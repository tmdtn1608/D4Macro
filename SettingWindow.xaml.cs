using System.Windows;
using D4Macro.Model;

namespace D4Macro;

public partial class SettingWindow : Window
{
    private ConfigModel _configModel = App.ConfigModel;
    public SettingWindow()
    {
        InitializeComponent();
        Loaded += GetConfig;
        // TODO : Appdata에서 설정JSON 가져오기
        // TODO : 설정 변경시 설정JSON 변경
        // TODO : 실행키 변경기능 F1~F12
        // TODO : 디아블로4 종료시 같이 종료 / 남아있기
        // TODO : 빠른저장 삭제
    }

    private void GetConfig(object sender, RoutedEventArgs e)
    {
        
        DataContext = _configModel;
    }
}