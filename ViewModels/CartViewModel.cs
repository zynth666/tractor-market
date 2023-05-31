using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TractorMarket.Entities;
using TractorMarket.Models;
using TractorMarket.Services;
using Wpf.Ui.Common.Interfaces;

namespace TractorMarket.ViewModels;

public partial class CartViewModel : ObservableObject, INavigationAware
{
    [ObservableProperty]
    private User _user = UserService.LoggedInUser!;

    private TractorService _tractorService;

    public CartViewModel(TractorService tractorService) 
    {
        _tractorService = tractorService;
    }

    public void OnNavigatedTo()
    {
        var tractors = _tractorService.GetTractorsForCustomers();

        foreach (var tractor in tractors)
        {
            CartItem item = new CartItem(tractor, 1);
            User.Cart.Add(item);
        }
    }

    public void OnNavigatedFrom()
    {
    }
}
