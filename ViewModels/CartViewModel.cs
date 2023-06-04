using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TractorMarket.Helpers;
using TractorMarket.Models;
using TractorMarket.Services;
using Wpf.Ui.Common.Interfaces;

namespace TractorMarket.ViewModels;

public partial class CartViewModel : ObservableObject, INavigationAware
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalPrice))]
    private DeepObservableCollection<CartItem> _cart = UserService.LoggedInUser!.Cart;

    public long TotalPrice { get; private set; }

    private TractorService _tractorService;
    private CartService _cartService;

    public CartViewModel(TractorService tractorService, CartService cartService)
    {
        _tractorService = tractorService;
        _cartService = cartService;

        var tractors = _tractorService.GetTractorsForCustomers();

        foreach (var tractor in tractors)
        {
            CartItem item = new CartItem(tractor, 2);
            Cart.Add(item);
        }

        TotalPrice = _cartService.GetTotalPrice(Cart);

        Cart.CollectionChanged += (_, _) =>
        {
            TotalPrice = _cartService.GetTotalPrice(Cart);
            OnPropertyChanged(nameof(TotalPrice));
        };
    }

    public void OnNavigatedTo()
    {
    }

    public void OnNavigatedFrom()
    {
    }

    [RelayCommand]
    public void RemoveCartItem(CartItem item)
    {
        Cart.Remove(item);
    }
}
