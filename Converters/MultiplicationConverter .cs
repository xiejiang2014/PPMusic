using System;
using System.Globalization;
using System.Windows.Data;

namespace PPMusic.Converters
{
    class MultiplicationConverter : IValueConverter
    {
        public double Multiplier { get; set; }

        public object Convert(object      value,
                              Type        targetType,
                              object      parameter,
                              CultureInfo culture
        )
        {
            if (value is double doubleValue)
            {
                return doubleValue * Multiplier;
            }

            throw new ArgumentException("转换器数据类型错误.");
        }

        public object ConvertBack(object      value,
                                  Type        targetType,
                                  object      parameter,
                                  CultureInfo culture
        )
        {
            if (value is double doubleValue)
            {
                return doubleValue / Multiplier;
            }

            throw new ArgumentException("转换器数据类型错误.");
        }
    }
}