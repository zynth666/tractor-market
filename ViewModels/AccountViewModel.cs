using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TractorMarket.Entities;
using TractorMarket.Services;
using TractorMarket.Views.Pages;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace TractorMarket.ViewModels;

public partial class AccountViewModel : ObservableObject, INavigationAware
{
    [ObservableProperty]
    private User _user = UserService.LoggedInUser!;

    private INavigationService _navigationService;

    public AccountViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public void OnNavigatedTo()
    {
        User = UserService.LoggedInUser!;
    }

    public void OnNavigatedFrom()
    {
    }

    [RelayCommand]
    public void NavigateToCartPage()
    {
        _navigationService.Navigate(typeof(CartPage));
    }

    [RelayCommand]
    public void NavigateToMarketPage()
    {
        _navigationService.Navigate(typeof(MarketPage));
    }
}
