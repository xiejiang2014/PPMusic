using System;
using System.Globalization;
using System.Windows.Data;

namespace PPMusic.Player
{
    class TimeSpanToMsConverter : IValueConverter
    {
        public object Convert(object      value,
                              Type        targetType,
                              object      parameter,
                              CultureInfo culture
        )
        {
            if (value is TimeSpan timeSpan)
            {
                return timeSpan.TotalMilliseconds;
            }


            throw new ArgumentException("被转换值类型错误.");
        }

        public object ConvertBack(object      value,
                                  Type        targetType,
                                  object      parameter,
                                  CultureInfo culture
        )
        {
            if (value is double doubleValue)
            {
                return TimeSpan.FromMilliseconds(doubleValue);
            }


            throw new ArgumentException("被转换值类型错误.");
        }
    }
}