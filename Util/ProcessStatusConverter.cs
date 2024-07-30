using System.Globalization;
using System.Windows.Data;

namespace D4Macro.Util;

public class ProcessStatusConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool status)
        {
            if (status) return "디아블로 실행중";
            else return "디아블로 대기중";
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is System.Windows.Visibility visibility)
        {
            return visibility == System.Windows.Visibility.Visible;
        }
        return false;
    }
}