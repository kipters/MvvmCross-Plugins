using System;
using Cirrious.CrossCore.Converters;

namespace Converters
{
    public class SubtleNotificationLengthToStringValueConverter : MvxValueConverter<bool, string>
    {
        protected override string Convert(bool value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "Length: " + (value ? "Long" : "Short");
        }
    }
}

