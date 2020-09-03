using BTH.Core.Environment;
using System;
using System.Globalization;
using System.Windows.Data;

namespace BTH.WPF.Converters
{
    [ValueConversion(typeof(decimal), typeof(string))]
    public class PositiveCurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var currValue = (decimal)value;
            if (currValue < 0) return null;
            return currValue.ToString("C", BTHCulture.CultureInfo);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}