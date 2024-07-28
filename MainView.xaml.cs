using System.Windows;
using System.Windows.Controls;
using D4Macro.ViewModel;
using WindowsInput;
using WindowsInput.Native;

namespace D4Macro;

public partial class MainView : UserControl
{
    private MainViewModel _mainViewModel;
    
    private const string SettingsFilePath = "macroSettings.json";
    
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
        var settings = new MacroJson
        {
            Key1Interval = int.TryParse(Key1IntervalTextBox.Text, out int key1Interval) ? key1Interval : 0,
            Key1Enabled = Key1CheckBox.IsChecked == true,
            Key2Interval = int.TryParse(Key2IntervalTextBox.Text, out int key2Interval) ? key2Interval : 0,
            Key2Enabled = Key2CheckBox.IsChecked == true,
            Key3Interval = int.TryParse(Key3IntervalTextBox.Text, out int key3Interval) ? key3Interval : 0,
            Key3Enabled = Key3CheckBox.IsChecked == true,
            Key4Interval = int.TryParse(Key4IntervalTextBox.Text, out int key4Interval) ? key4Interval : 0,
            Key4Enabled = Key4CheckBox.IsChecked == true,
            MouseLeftInterval = int.TryParse(MouseLeftIntervalTextBox.Text, out int mouseLeftInterval) ? mouseLeftInterval : 0,
            MouseLeftEnabled = MouseLeftCheckBox.IsChecked == true,
            MouseRightInterval = int.TryParse(MouseRightIntervalTextBox.Text, out int mouseRightInterval) ? mouseRightInterval : 0,
            MouseRightEnabled = MouseRightCheckBox.IsChecked == true,
        };

        MacroJson.SaveSettings(settings, SettingsFilePath);
    }

    private void LoadButton_Click(object sender, RoutedEventArgs e)
    {
        var settings = MacroJson.LoadSettings(SettingsFilePath);

        Key1IntervalTextBox.Text = settings.Key1Interval.ToString();
        Key1CheckBox.IsChecked = settings.Key1Enabled;
        Key2IntervalTextBox.Text = settings.Key2Interval.ToString();
        Key2CheckBox.IsChecked = settings.Key2Enabled;
        Key3IntervalTextBox.Text = settings.Key3Interval.ToString();
        Key3CheckBox.IsChecked = settings.Key3Enabled;
        Key4IntervalTextBox.Text = settings.Key4Interval.ToString();
        Key4CheckBox.IsChecked = settings.Key4Enabled;
        MouseLeftIntervalTextBox.Text = settings.MouseLeftInterval.ToString();
        MouseLeftCheckBox.IsChecked = settings.MouseLeftEnabled;
        MouseRightIntervalTextBox.Text = settings.MouseRightInterval.ToString();
        MouseRightCheckBox.IsChecked = settings.MouseRightEnabled;
    }
    
}