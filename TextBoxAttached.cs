using System.Windows;
using System.Windows.Controls;

namespace PPMusic
{
    public class TextBoxAttached
    {
        public static readonly DependencyProperty WaterMarkIconProperty
            = DependencyProperty.RegisterAttached(
                                                  "WaterMarkIcon",
                                                  typeof(object),
                                                  typeof(TextBoxAttached),
                                                  new UIPropertyMetadata(null)
                                                 );

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static void SetWaterMarkIcon(DependencyObject element,
                                            object           value
        )
        {
            element.SetValue(WaterMarkIconProperty, value);
        }

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static object GetWaterMarkIcon(DependencyObject element)
        {
            return (object) element.GetValue(WaterMarkIconProperty);
        }
    }
}