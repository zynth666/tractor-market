using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using TractorMarket.Entities;
using TractorMarket.Helpers;
using TractorMarket.Models;
using TractorMarket.Services;
using TractorMarket.Views.Pages;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace TractorMarket.ViewModels;

public partial class CartViewModel : ObservableObject, INavigationAware
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalPrice))]
    private DeepObservableCollection<CartItem<ItemisableBaseEntity>> _cart = UserService.LoggedInUser!.Cart;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CheckoutCommand))]
    [NotifyPropertyChangedFor(nameof(HasNotEnoughBudget))]
    private double _totalPrice;

    private readonly INavigationService _navigationService;

    private readonly TractorService _tractorService;
    private readonly CartService _cartService;

    [ObservableProperty]
    private bool _isAdmin = UserService.LoggedInUser!.IsAdmin;

    [ObservableProperty]
    private bool _isNotAdmin = !UserService.LoggedInUser!.IsAdmin;

    [ObservableProperty]
    private bool _hasNotEnoughBudget;

    public CartViewModel(TractorService tractorService, INavigationService navigationService, CartService cartService)
    {
        _tractorService = tractorService;
        _cartService = cartService;
        _navigationService = navigationService;
    }

    private double GetTotalByAccount()
    {
        if (IsAdmin)
        {
            return CartService.GetTotalAdminPrice(Cart);
        }
        
        return CartService.GetTotalPrice(Cart);
    }

    /// <summary>
    /// Checks if user has enough budget to checkout.
    /// Also sets a helper flag (HasNotEnoughBudget) for the view, since you can't negate values in Binding expressions.
    /// </summary>
    /// <returns>Boolean if user has enough budget.</returns>
    private bool HasEnoughBudgetToCheckout()
    {
        bool hasNotEnoughBudget = TotalPrice >= UserService.LoggedInUser!.Budget && TotalPrice != 0;
        HasNotEnoughBudget = hasNotEnoughBudget;
        return !hasNotEnoughBudget;
    }

    /// <summary>
    /// Reset certain Bindings when navigating to this view to get rid of stale data in case the logged in user has changed.
    /// </summary>
    public void OnNavigatedTo()
    {
        Cart = UserService.LoggedInUser!.Cart;
        IsAdmin = UserService.LoggedInUser!.IsAdmin;
        IsNotAdmin = !UserService.LoggedInUser!.IsAdmin;

        TotalPrice = GetTotalByAccount();

        Cart.CollectionChanged += (_, _) =>
        {
            TotalPrice = GetTotalByAccount();
        };
    }

    public void OnNavigatedFrom()
    {
    }

    [RelayCommand]
    private void OpenImageViewer(ItemisableBaseEntity item)
    {
        ImageViewerService.Name = item.Name;
        ImageViewerService.Category = "cart";
        _navigationService.Navigate(typeof(ImageViewerPage));
    }

    [RelayCommand]
    public void RemoveCartItem(CartItem<ItemisableBaseEntity> item)
    {
        Cart.Remove(item);
    }

    [RelayCommand(CanExecute = nameof(HasEnoughBudgetToCheckout))]
    public void Checkout()
    {
        _cartService.Checkout(UserService.LoggedInUser!);
    }
}
