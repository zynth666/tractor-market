using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using TractorMarket.Models;
using TractorMarket.Services;

namespace TractorMarket.Helpers
{
    public class CartSumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ObservableCollection<CartItem> items)
            {
                return CartService.GetTotalPrice(items);
            }

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
