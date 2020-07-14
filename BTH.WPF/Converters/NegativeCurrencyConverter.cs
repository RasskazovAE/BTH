using BTH.Core.Environment;
using System;
using System.Globalization;
using System.Windows.Data;

namespace BTH.WPF.Converters
{
    public class NegativeCurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var currValue = (decimal)value;
            if (currValue < 0) return Math.Abs(currValue).ToString("C", BTHCulture.CultureInfo);
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
