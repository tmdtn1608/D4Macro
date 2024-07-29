using System.Windows;

namespace D4Macro.Command;

public partial class SettingWindow : Window
{
    public SettingWindow()
    {
        InitializeComponent();
        // TODO : 설정JSON 없을경우 default 설정 JSON Appdata에 복사
        // TODO : 설정 변경시 설정JSON 변경
        // TODO : 실행키 변경기능 F1~F12
        // TODO : 디아블로4 종료시 같이 종료 / 남아있기
        // TODO : 빠른저장 삭제
    }
}