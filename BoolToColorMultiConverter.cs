using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class BoolToColorMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2 || !(values[0] is bool) || !(values[1] is bool))
                return Colors.Transparent; // Valore di default

            bool boolValue1 = (bool)values[0];
            bool boolValue2 = (bool)values[1];

            // Logica per decidere il colore in base ai due booleani
            if (boolValue1)
                return Colors.LightGrey; // Solo il primo vero
            else if (boolValue2)
                return Colors.Yellow; // Solo il secondo vero
            else
                return Colors.DarkGrey; // Entrambi falsi
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException(); 
        }

    }
}
