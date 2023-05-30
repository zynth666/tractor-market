﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using TractorMarket.Helpers;
using TractorMarket.Views.Pages;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace TractorMarket.ViewModels
{
    public partial class LoginViewModel : ObservableObject, INavigationAware
    {
        private RefreshDatabase _refreshDatabaseHelper;
        private INavigationService _navigationService;
        public event Action? ProcessLogin;

        public LoginViewModel(RefreshDatabase refreshDatabaseHelper, INavigationService navigationService)
        {
            _refreshDatabaseHelper = refreshDatabaseHelper;
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
            ProcessLogin?.Invoke();
        }

        [RelayCommand]
        private void OnNavigateToRegisterPage()
        {
            _navigationService.Navigate(typeof(RegisterPage));
        }
    }
}