using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Diagnostics;
using TractorMarket.Helpers;
using TractorMarket.Services;
using Wpf.Ui.Common.Interfaces;

namespace TractorMarket.ViewModels
{
    public partial class LoginViewModel : ObservableObject, INavigationAware
    {
        private UserService _userService;
        private RefreshDatabase _refreshDatabaseHelper;
        public event Action? ProcessLogin;
        [ObservableProperty]
        private string _usernameInput = "";
        [ObservableProperty]
        private string _passwordInput = "";

        public LoginViewModel(RefreshDatabase refreshDatabaseHelper, UserService userService)
        {
            _refreshDatabaseHelper = refreshDatabaseHelper;
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
        private void OnDoLogin()
        {
            if (_userService.LoginUser(_usernameInput, _passwordInput) == false) {
                return;
            }
            ProcessLogin?.Invoke();
        }
    }
}