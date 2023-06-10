using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CheckoutCommand))]
    private long _totalPrice;

    private TractorService _tractorService;
    private CartService _cartService;

    public CartViewModel(TractorService tractorService, CartService cartService)
    {
        _tractorService = tractorService;
        _cartService = cartService;

        long totalPrice = _cartService.GetTotalPrice(Cart);
        TotalPrice = totalPrice;

        Cart.CollectionChanged += (_, _) =>
        {
            long totalPrice = _cartService.GetTotalPrice(Cart);
            TotalPrice = totalPrice;
            OnPropertyChanged(nameof(TotalPrice));
        };
    }

    private bool HasEnoughBudgetToCheckout()
    {
        long totalPrice = _cartService.GetTotalPrice(Cart);
        return totalPrice <= UserService.LoggedInUser!.Budget && totalPrice != 0;
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

    [RelayCommand(CanExecute = nameof(HasEnoughBudgetToCheckout))]
    public void Checkout()
    {
        _cartService.Checkout(Cart, UserService.LoggedInUser!);
    }
}
