using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System;
using System.Text;
using System.Linq;

namespace TractorMarket.Helpers
{
    /// <summary>
    /// Does line break in string list
    /// </summary>

    public class StringListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<string> stringList)
            {
                const int entriesPerLine = 4;
                var stringBuilder = new StringBuilder();
                var stringListAsList = stringList.ToList();
                for (int i = 0; i < stringListAsList.Count; i++)
                {
                    stringBuilder.Append(stringListAsList[i]);
                    if ((i + 1) % entriesPerLine == 0 && i < stringListAsList.Count - 1)
                    {
                        stringBuilder.AppendLine();
                    }
                    else if (i < stringListAsList.Count - 1)
                    {
                        stringBuilder.Append(", ");
                    }
                }
                return stringBuilder.ToString();
            }
            return string.Empty;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
