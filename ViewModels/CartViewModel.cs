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
    private double _totalPrice;

    private readonly TractorService _tractorService;
    private readonly CartService _cartService;

    [ObservableProperty]
    private bool _isAdmin = UserService.LoggedInUser!.IsAdmin;

    [ObservableProperty]
    private bool _isNotAdmin = !UserService.LoggedInUser!.IsAdmin;

    public CartViewModel(TractorService tractorService, CartService cartService)
    {
        _tractorService = tractorService;
        _cartService = cartService;

        TotalPrice = GetTotalByAccount();

        Cart.CollectionChanged += (_, _) =>
        {
            TotalPrice = GetTotalByAccount();
            OnPropertyChanged(nameof(TotalPrice));
        };
    }

    private double GetTotalByAccount()
    {
        if (IsAdmin)
        {
            return CartService.GetTotalAdminPrice(Cart);
        }
        
        return CartService.GetTotalPrice(Cart);
    }

    private bool HasEnoughBudgetToCheckout()
    {
        double totalPrice = GetTotalByAccount();
        return totalPrice <= UserService.LoggedInUser!.Budget && totalPrice != 0;
    }

    public void OnNavigatedTo()
    {
        Cart = UserService.LoggedInUser!.Cart;
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
        _cartService.Checkout(UserService.LoggedInUser!);
    }
}
