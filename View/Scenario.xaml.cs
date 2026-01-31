using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using D4Macro.ViewModel;

namespace D4Macro.View;

public partial class Scenario : UserControl
{
    public ObservableCollection<ScenarioViewModel> Scenarios { get; set; }

    public Scenario()
    {
        InitializeComponent();
        Scenarios = new ObservableCollection<ScenarioViewModel>();
        // 초기 행 추가
        AddScenarioRow();
        DataContext = this;
    }

    private void AddScenarioRow()
    {
        if (Scenarios.Count >= 6) return;

        var newItem = new ScenarioViewModel();
        Scenarios.Add(newItem);
        UpdateButtons();
    }

    private void RemoveScenarioRow(ScenarioViewModel item)
    {
        if (Scenarios.Count <= 1) return;
        
        Scenarios.Remove(item);
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        for (int i = 0; i < Scenarios.Count; i++)
        {
            var item = Scenarios[i];
            bool isLast = (i == Scenarios.Count - 1);
            
            // 추가 버튼: 마지막 행이면서 전체 개수가 6개 미만일 때
            item.IsAddButtonVisible = isLast && (Scenarios.Count < 6);

            // 삭제 버튼: 마지막 행이면서 전체 개수가 1개 초과일 때 (가장 마지막 행에만 표시)
            item.IsDeleteButtonVisible = isLast && (Scenarios.Count > 1);
        }
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        AddScenarioRow();
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is ScenarioViewModel item)
        {
            RemoveScenarioRow(item);
        }
    }
}