using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using D4Macro.Model;
using D4Macro.Util;

namespace D4Macro.Windows;

public partial class SettingWindow : Window
{
    private ConfigModel _configModel = App.ConfigModel;
    public SettingWindow()
    {
        InitializeComponent();
        // Appdata에서 설정JSON 가져오기
        Loaded += (sender, args) =>
        {
            DataContext = _configModel;
        };
        // 실행키 변경 ComboBoxItem 생성
        Loaded += SetLaunchComboboxItem;
        
        // 닫을때 자동저장
        this.Closed += SaveConfig;
    }

    private void SetLaunchComboboxItem(object sender, RoutedEventArgs e)
    {
        foreach (Const.LaunchKeyEnum key in Enum.GetValues(typeof(Const.LaunchKeyEnum)))
        {
            ComboBoxItem item = new ComboBoxItem
            {
                Content = key.ToString(),
                Tag = (Key)key
            };
            LaunchComboBox.Items.Add(item);

            if ((Key)key == _configModel.LaunchKey)
            {
                LaunchComboBox.SelectedItem = item;
            }
        }

    }
    private void ChangeLaunchKey(object sender, SelectionChangedEventArgs e)
    {
        if (LaunchComboBox.SelectedItem is ComboBoxItem selectedItem)
        {
            // 선택된 ComboBoxItem의 Tag 값을 모델에 반영
            _configModel.LaunchKey = (Key)selectedItem.Tag;
        }
    }

    private void SaveConfig(object? sender, EventArgs e)
    {
        JsonController.Instance.WriteJson(App.ConfigModel, Const.SETTING_FILE_PATH);
    }
    private void ResetConfig(object sender, RoutedEventArgs e)
    {
        App.ConfigModel = new ConfigModel();
        _configModel = App.ConfigModel;
        DataContext = _configModel;
        foreach (ComboBoxItem item in LaunchComboBox.Items)
        {
            if ((Key)item.Tag == _configModel.LaunchKey)
            {
                LaunchComboBox.SelectedItem = item;
                break;
            }
        }
    }

    private void ResetQuickSave(object sender, RoutedEventArgs e)
    {
        JsonController.Instance.DeleteJson(Const.DATA_FILE_PATH);
        MessageBox.Show("성공적으로 초기화 되었습니다");
    }

}