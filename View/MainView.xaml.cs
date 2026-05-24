using System.IO;
using System.Windows;
using System.Windows.Controls;
using D4Macro.Model;
using D4Macro.Util;
using D4Macro.ViewModel;
using MessageBox = System.Windows.Forms.MessageBox;

namespace D4Macro.View;

public partial class MainView : UserControl
{
    private MainViewModel _mainViewModel;
    
    
    
    public MainView()
    {
        InitializeComponent();
        Loaded += (sender, args) =>
        {
            _mainViewModel = DataContext as MainViewModel;
        };
    }

    private void ToggleMacroButton_Click(object sender, RoutedEventArgs e)
    {
        _mainViewModel.ToggleMacro();
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        // 바인딩된 DataModel이 최신 UI 상태를 이미 가지고 있으므로 그대로 저장합니다.
        if (_mainViewModel != null && _mainViewModel.DataModel != null)
        {
            JsonController.Instance.WriteJson(_mainViewModel.DataModel, Const.DATA_FILE_PATH);
        }
    }

    private void LoadButton_Click(object sender, RoutedEventArgs e)
    {
        if (File.Exists(Const.DATA_FILE_PATH))
        {
            _mainViewModel.DataModel = JsonController.Instance.ReadJson<DataModel>(Const.DATA_FILE_PATH);
        }
        else
        {
            MessageBox.Show("저장된 데이터가 없습니다");
        }
        
    }
    
}