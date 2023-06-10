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
        private bool _isInitialized = false;
        private TractorService _tractorService;
        private CartService _cartService;

        private INavigationService _navigationService;

        [ObservableProperty]
        private int _selectedQuantity;

        [ObservableProperty]
        private List<Tractor> _tractorsForCustomers = new List<Tractor>();

        [ObservableProperty]
        private List<Tractor> _tractorsForAdmin = new List<Tractor>();

        public MarketViewModel(TractorService tractorService, INavigationService navigationService, CartService cartService)
        {
            _tractorService = tractorService;
            _navigationService = navigationService;
            _cartService = cartService;
        }

        public void OnNavigatedTo()
        {
            if (!_isInitialized)
                InitializeViewModel();
        }

        public void OnNavigatedFrom()
        {
        }

        private void InitializeViewModel()
        {
            TractorsForCustomers = _tractorService.GetTractorsForCustomers();
            TractorsForAdmin = _tractorService.GetTractorsForAdmin();
            _isInitialized = true;
        }

        [RelayCommand]
        private void OpenImageViewer(Tractor tractor_in)
        {
            ImageViewerService.Type = tractor_in.Type;
            ImageViewerService.Manufacturer = tractor_in.Manufacturer;

            _navigationService.Navigate(typeof(ImageViewerPage));
        }

        [RelayCommand]
        public void AddToCart(Tractor tractor)
        {
            CartItem cartItem = new(tractor, SelectedQuantity);
            _cartService.AddToCart(UserService.LoggedInUser!.Cart, cartItem);
        }
    }
}
