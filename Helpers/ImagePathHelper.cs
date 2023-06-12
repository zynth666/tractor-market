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
    class ImagePathHelper : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is string imageName)
            {
                if(imageName != "" && imageName != null)
                {
                    string convertedImageName = imageName.Replace(" ", "_");
                    string imagePath = $"pack://application:,,,/Assets/Images/Products/{convertedImageName}.jpg";
                    return imagePath;
                }
              
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
