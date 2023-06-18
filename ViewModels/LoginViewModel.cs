using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using TractorMarket.Services;
using TractorMarket.Views.Pages;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace TractorMarket.ViewModels;

public partial class LoginViewModel : ObservableObject, INavigationAware
{
    private UserService _userService;
    private INavigationService _navigationService;

    public event Action? ShowNavigation;

    [ObservableProperty]
    private string _usernameInput = "";

    [ObservableProperty]
    private string _passwordInput = "";

    [ObservableProperty]
    private bool _hadErrorLoggingIn;

    public LoginViewModel(INavigationService navigationService, UserService userService)
    {
        _userService = userService;
        _navigationService = navigationService;
    }

    public void OnNavigatedTo()
    {
    }

    public void OnNavigatedFrom()
    {

    }

    /// <summary>
    /// Attempts to login the user and redirects them to the account page on success.
    /// </summary>
    [RelayCommand]
    private void OnDoLogin()
    {
        if (!_userService.LoginUser(UsernameInput, PasswordInput)) {
            HadErrorLoggingIn = true;
            return;
        }

        HadErrorLoggingIn = false;

        UsernameInput = string.Empty;
        PasswordInput = string.Empty;

        ShowNavigation?.Invoke();
        _navigationService.Navigate(typeof(AccountPage));
    }

    [RelayCommand]
    private void OnNavigateToRegisterPage()
    {
        _navigationService.Navigate(typeof(RegisterPage));
    }
}