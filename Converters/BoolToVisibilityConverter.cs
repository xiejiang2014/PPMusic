using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PPMusic.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public BoolToVisibilityConverter()
        {
        }

        public BoolToVisibilityConverter(bool useHidden,
                                         bool isReversed
        )
        {
            UseHidden  = useHidden;
            IsReversed = isReversed;
        }

        public bool UseHidden { get; set; }

        public bool IsReversed { get; set; }

        public object Convert(object      value,
                              Type        targetType,
                              object      parameter,
                              CultureInfo culture
        )
        {
            bool flag = System.Convert.ToBoolean(value, CultureInfo.InvariantCulture);
            if (IsReversed)
            {
                flag = !flag;
            }

            return flag ? Visibility.Visible : (Visibility)(UseHidden ? 1 : 2);
        }

        public object ConvertBack(
            object      value,
            Type        targetType,
            object      parameter,
            CultureInfo culture
        )
        {
            if (value is not Visibility visibility)
            {
                return DependencyProperty.UnsetValue;
            }

            var flag = visibility == Visibility.Visible;
            if (IsReversed)
            {
                flag = !flag;
            }

            return flag;
        }
    }
}