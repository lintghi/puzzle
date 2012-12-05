using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Globalization;

namespace Puzzle.Vo {
    //根据ToggleSwitch的IsChecked状态, 转换显示的内容
    public class ToggleSwitchContentConverter : IValueConverter{
        const string IS_CHECKED_CONTENT = "开";
        const string IS_UNCHECKED_CONTENT = "关";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture){
            return (bool)value ? IS_CHECKED_CONTENT : IS_UNCHECKED_CONTENT;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return value;
        }
    }
}
