using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Diagnostics;
using TractorMarket.Entities;
using TractorMarket.Models;
using TractorMarket.Services;
using Wpf.Ui.Common.Interfaces;

namespace TractorMarket.ViewModels;

public partial class CartViewModel : ObservableObject, INavigationAware
{
    [ObservableProperty]
    private List<CartItem> _cart = UserService.LoggedInUser!.Cart;

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
    }

    public void OnNavigatedTo()
    {
        Debug.WriteLine(_cartService.GetTotalPrice(Cart));
    }

    public void OnNavigatedFrom()
    {
    }
}
