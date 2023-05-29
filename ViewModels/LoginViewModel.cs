using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Diagnostics;
using TractorMarket.Helpers;
using Wpf.Ui.Common.Interfaces;

namespace TractorMarket.ViewModels
{
    public partial class LoginViewModel : ObservableObject, INavigationAware
    {
        private RefreshDatabase _refreshDatabaseHelper;
        public event Action? ProcessLogin;

        public LoginViewModel(RefreshDatabase refreshDatabaseHelper)
        {
            _refreshDatabaseHelper = refreshDatabaseHelper;
            
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
    }
}