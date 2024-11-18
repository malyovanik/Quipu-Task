using System.Globalization;
using System.Windows.Data;

namespace Quipu_Task.Converters
{
    public class EnumToListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var enumType = value as Type;
            if (enumType == null || !enumType.IsEnum)
                throw new ArgumentException("Value must be an Enum type", nameof(value));

            return Enum.GetValues(enumType);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}