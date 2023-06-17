using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;


namespace TractorMarket.Helpers
{
    /// <summary>
    /// Does convert the image zoom value
    /// </summary>

    class ZoomSliderValHelper : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int sliderVal)
            {
                double newSliderVal = 1 + ((double)sliderVal / 100);
                return newSliderVal;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
