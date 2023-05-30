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
    public partial class LoginViewModel : ObservableObject, INavigationAware
    {
        private UserService _userService;
        private RefreshDatabase _refreshDatabaseHelper;
        private INavigationService _navigationService;

        public event Action? ProcessLogin;

        [ObservableProperty]
        private string _usernameInput = "";
        [ObservableProperty]
        private string _passwordInput = "";

        public LoginViewModel(RefreshDatabase refreshDatabaseHelper, INavigationService navigationService, UserService userService)
        {
            _refreshDatabaseHelper = refreshDatabaseHelper;
            _userService = userService;
            _navigationService = navigationService;
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
        private void OnDoLogin()
        {
            if (_userService.LoginUser(_usernameInput, _passwordInput) == false) {
                return;
            }
            ProcessLogin?.Invoke();
        }

        [RelayCommand]
        private void OnNavigateToRegisterPage()
        {
            _navigationService.Navigate(typeof(RegisterPage));
        }
    }
}