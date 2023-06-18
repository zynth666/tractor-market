using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Linq;
using TractorMarket.Helpers;
using TractorMarket.Services;
using TractorMarket.Views.Pages;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace TractorMarket.ViewModels
{
    public partial class RegisterViewModel : ObservableObject, INavigationAware
    {
        private INavigationService _navigationService;
        private UserService _userService;

        public event Action? ShowNavigation;

        [ObservableProperty]
        private string _usernameInput = "";

        [ObservableProperty]
        private string _passwordInput = "";

        [ObservableProperty]
        private int _budgetInput = 0;

        [ObservableProperty]
        private bool _isInvalidUsername;

        [ObservableProperty]
        private bool _isInvalidPassword;

        public RegisterViewModel(INavigationService navigationService, UserService userService)
        {
            _navigationService = navigationService;
            _userService = userService;
        }

        public void OnNavigatedTo()
        {
        }

        public void OnNavigatedFrom()
        {
        }

        /// <summary>
        /// Attempts to register a new user, logs them in and navigates to their account page if user input is valid.
        /// </summary>
        [RelayCommand]
        private void OnRegister()
        {
            IsInvalidUsername = UsernameInput.Length < 3;
            IsInvalidPassword = PasswordInput.Length < 8;

            if (IsInvalidUsername ||  IsInvalidPassword)
            {
                return;
            }

            _userService.RegisterUser(UsernameInput, PasswordInput, BudgetInput);

            UsernameInput = string.Empty;
            PasswordInput = string.Empty;
            BudgetInput = 0;

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