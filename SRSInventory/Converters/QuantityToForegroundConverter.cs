using System.Globalization;

namespace SRSInventory.Converters
{
    public class QuantityToForegroundConverter : IValueConverter
    {
        private Color _soldOutForeground = Colors.Red;
        private Color _availableForeground = Colors.Black;
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(value is decimal decimalValue)
            {
                return decimalValue > 0 ? _availableForeground : _soldOutForeground;
            }

            return Binding.DoNothing;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
