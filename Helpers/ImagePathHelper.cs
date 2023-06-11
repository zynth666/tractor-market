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
                {
                    string convertedImageName = imageName.Replace(" ", "_");
                    string imagePath = $"pack://application:,,,/Assets/Images/Products/{convertedImageName}.jpg";
                    Debug.WriteLine(" IMAGE PATH : " + imagePath);
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
