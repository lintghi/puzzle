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
    //取非转换器
    public class NotConverter : IValueConverter{
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return not(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return not(value);
        }

        private bool not(object value) {
            return !(bool)value;
        }
    }
}
