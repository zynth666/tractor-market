using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using TractorMarket.Entities;
using TractorMarket.Helpers;
using TractorMarket.Models;
using TractorMarket.Services;
using TractorMarket.Views.Pages;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace TractorMarket.ViewModels
{
    public partial class AddonViewModel : ObservableObject, INavigationAware
    {
        private INavigationService _navigationService;
        private AddonService _addonService;
        private CartService _cartService;

        [ObservableProperty]
        private List<TractorAddon> _allAddons = new List<TractorAddon>();

        [ObservableProperty]
        private int _selectedQuantity;


        public AddonViewModel(AddonService addonService, INavigationService navigationService, CartService cartService)
        {
            _navigationService = navigationService;
            _cartService = cartService;
            _addonService = addonService;
            AllAddons = _addonService.GetAllAddons();
        }

        [RelayCommand]
        private void OpenImageViewer(TractorAddon tractoraddon_in)
        {
            ImageViewerService.Name = tractoraddon_in.Name;
            _navigationService.Navigate(typeof(ImageViewerPage));
        }


        public void OnNavigatedTo()
        {

          
        }

        public void OnNavigatedFrom()
        {

        }

        [RelayCommand]
        public static void AddToCart(TractorAddon tractorAddon)
        {
            CartItem<ItemisableBaseEntity> cartItem = new(tractorAddon, tractorAddon.SelectedQuantity);
            CartService.AddToCart(UserService.LoggedInUser!.Cart, cartItem);
        }
    }
}
