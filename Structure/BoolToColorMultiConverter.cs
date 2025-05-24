using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Structure
{
    public class BoolToColorMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: values is null or has less than 2 elements");
                return Colors.Transparent;
            }


            if (!(values[0] is bool boolValue1) || !(values[1] is bool boolValue2))
            {
                System.Diagnostics.Debug.WriteLine("ERROR: Values are not booleans");
                return Colors.Transparent;
            }

            if (boolValue1)
                return Colors.LightGrey;
            else if (boolValue2)
                return Colors.Yellow;
            else
                return Colors.DarkGrey;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
