using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using TractorMarket.Helpers;
using TractorMarket.Services;
using TractorMarket.Views.Pages;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace TractorMarket.ViewModels
{
    public partial class RegisterViewModel : ObservableObject, INavigationAware
    {
        private RefreshDatabase _refreshDatabaseHelper;
        private INavigationService _navigationService;
        private UserService _userService;

        public event Action? ShowNavigation;

        [ObservableProperty]
        private string _usernameInput = "";
        [ObservableProperty]
        private string _passwordInput = "";
        [ObservableProperty]
        private int _budgetInput = 0;

        public RegisterViewModel(RefreshDatabase refreshDatabaseHelper, INavigationService navigationService, UserService userService)
        {
            _refreshDatabaseHelper = refreshDatabaseHelper;
            _navigationService = navigationService;
            _userService = userService;
        }

        public void OnNavigatedTo()
        {
        }

        public void OnNavigatedFrom()
        {
        }

        [RelayCommand]
        private void OnRefreshDatabase()
        {
            _refreshDatabaseHelper.Execute();
        }

        [RelayCommand]
        private void OnRegister()
        {
            _userService.RegisterUser(UsernameInput, PasswordInput, BudgetInput);
            ShowNavigation?.Invoke();
            _navigationService.Navigate(typeof(AccountPage));
        }

        [RelayCommand]
        private void OnNavigateToLoginPage()
        {
            _navigationService.Navigate(typeof(LoginPage));
        }
    }
}