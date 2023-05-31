using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TractorMarket.Entities;
using TractorMarket.Services;
using Wpf.Ui.Common.Interfaces;

namespace TractorMarket.ViewModels;

public partial class AccountViewModel : ObservableObject, INavigationAware
{
    [ObservableProperty]
    private User _user = UserService.LoggedInUser!;

    public void OnNavigatedTo()
    {
    }

    public void OnNavigatedFrom()
    {
    }
}
