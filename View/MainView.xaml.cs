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
        var settings = new DataModel
        {
            Key1Interval = int.TryParse(Key1IntervalTextBox.Text, out int key1Interval) ? key1Interval : 0,
            Key1CheckBox = Key1CheckBox.IsChecked == true,
            Key2Interval = int.TryParse(Key2IntervalTextBox.Text, out int key2Interval) ? key2Interval : 0,
            Key2CheckBox = Key2CheckBox.IsChecked == true,
            Key3Interval = int.TryParse(Key3IntervalTextBox.Text, out int key3Interval) ? key3Interval : 0,
            Key3CheckBox = Key3CheckBox.IsChecked == true,
            Key4Interval = int.TryParse(Key4IntervalTextBox.Text, out int key4Interval) ? key4Interval : 0,
            Key4CheckBox = Key4CheckBox.IsChecked == true,
            MouseLeftInterval = int.TryParse(MouseLeftIntervalTextBox.Text, out int mouseLeftInterval) ? mouseLeftInterval : 0,
            MouseLeftCheckBox = MouseLeftCheckBox.IsChecked == true,
            MouseRightInterval = int.TryParse(MouseRightIntervalTextBox.Text, out int mouseRightInterval) ? mouseRightInterval : 0,
            MouseRightCheckBox = MouseRightCheckBox.IsChecked == true,
        };

        JsonController.Instance.WriteJson(settings, Const.DATA_FILE_PATH);
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