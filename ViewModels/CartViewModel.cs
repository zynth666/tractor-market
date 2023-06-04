using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TractorMarket.Models;
using TractorMarket.Services;
using Wpf.Ui.Common.Interfaces;

namespace TractorMarket.ViewModels;

public partial class CartViewModel : ObservableObject, INavigationAware
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PriceSum))]
    private ObservableCollection<CartItem> _cart = UserService.LoggedInUser!.Cart;
    
    public long PriceSum { get; private set; }

    private TractorService _tractorService;
    private CartService _cartService;

    public CartViewModel(TractorService tractorService, CartService cartService)
    {
        _tractorService = tractorService;
        _cartService = cartService;

        var tractors = _tractorService.GetTractorsForCustomers();

        foreach (var tractor in tractors)
        {
            CartItem item = new CartItem(tractor, 1);
            Cart.Add(item);
        }

        PriceSum = _cartService.GetTotalPrice(Cart);

        Cart.CollectionChanged += (_, _) =>
        {
            PriceSum = _cartService.GetTotalPrice(Cart);
            OnPropertyChanged(nameof(PriceSum));
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
