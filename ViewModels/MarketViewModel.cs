using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using TractorMarket.Entities;
using TractorMarket.Models;
using TractorMarket.Services;
using TractorMarket.Views.Pages;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace TractorMarket.ViewModels
{
    public partial class MarketViewModel : ObservableObject, INavigationAware
    {
        private readonly TractorService _tractorService;

        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private List<Tractor> _tractors = new();

        [ObservableProperty]
        private bool _isAdmin = UserService.LoggedInUser!.IsAdmin;

        [ObservableProperty]
        private bool _isNotAdmin = !UserService.LoggedInUser!.IsAdmin;

        public MarketViewModel(TractorService tractorService, INavigationService navigationService)
        {
            _tractorService = tractorService;
            _navigationService = navigationService;
        }

        public void OnNavigatedTo()
        {
            IsAdmin = UserService.LoggedInUser!.IsAdmin;
            IsNotAdmin = !UserService.LoggedInUser!.IsAdmin;

            UpdateTractorList();
        }

        public void OnNavigatedFrom()
        {
        }

        [RelayCommand]
        private void OpenImageViewer(Tractor tractor_in)
        {
            ImageViewerService.Name = tractor_in.Type;
            ImageViewerService.Type = "tractor";
            ImageViewerService.Manufacturer = tractor_in.Manufacturer;

            _navigationService.Navigate(typeof(ImageViewerPage));
        }

        [RelayCommand]
        public static void AddToCart(Tractor tractor)
        {
            CartItem cartItem = new(tractor, tractor.SelectedQuantity);
            CartService.AddToCart(UserService.LoggedInUser!.Cart, cartItem);
        }

        private void UpdateTractorList()
        {
            if (IsAdmin)
            {
                Tractors = _tractorService.GetTractorsForAdmin();
            }
            else
            {
                Tractors = _tractorService.GetTractorsForCustomers();
            }
        }
    }
}
