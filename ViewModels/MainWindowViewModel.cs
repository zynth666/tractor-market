using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using TractorMarket.Helpers;
using TractorMarket.Services;
using TractorMarket.Views.Pages;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace TractorMarket.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private bool _isInitialized = false;
        private INavigationService _navigationService;

        [ObservableProperty]
        private string _applicationTitle = String.Empty;

        [ObservableProperty]
        private ObservableCollection<INavigationControl> _navigationItems = new();

        [ObservableProperty]
        private ObservableCollection<INavigationControl> _navigationFooter = new();

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new();

        public event Action? ProcessLogout;

        public MainWindowViewModel(INavigationService navigationService, RefreshDatabase refreshDatabaseHelper)
        {
            if (!_isInitialized)
                InitializeViewModel();

            refreshDatabaseHelper.Execute();
            _navigationService = navigationService;
        }

        private void InitializeViewModel()
        {
            ApplicationTitle = "Big Boys - TractorMarket";

            NavigationItems = new ObservableCollection<INavigationControl>
            {
                new NavigationItem()
                {
                    Content = "Home",
                    PageTag = "dashboard",
                    Icon = SymbolRegular.Home24,
                    PageType = typeof(Views.Pages.DashboardPage)
                },
                new NavigationItem()
                {
                    Content = "Markt",
                    PageTag = "market",
                    Icon = SymbolRegular.VehicleTruckBag24,
                    PageType = typeof(Views.Pages.MarketPage)
                }
            };

            NavigationFooter = new ObservableCollection<INavigationControl>
            {
                new NavigationItem()
                {
                    Content = "Logout",
                    PageTag = "logout",
                    Icon = SymbolRegular.ArrowExit20,
                    Command = LogOutCommand
                },
                new NavigationItem()
                {
                    Content = "Settings",
                    PageTag = "settings",
                    Icon = SymbolRegular.Settings24,
                    PageType = typeof(Views.Pages.SettingsPage)
                }
            };

            TrayMenuItems = new ObservableCollection<MenuItem>
            {
                new MenuItem
                {
                    Header = "Home",
                    Tag = "tray_home"
                }
            };

            _isInitialized = true;
        }

        [RelayCommand]
        public void LogOut()
        {
            UserService.LoggedInUser = null;
            ProcessLogout?.Invoke();
            _navigationService.Navigate(typeof(LoginPage));
        }
    }
}
