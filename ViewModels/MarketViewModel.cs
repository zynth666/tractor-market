using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Diagnostics;
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
        private int _minEurSlider = 95000;

        [ObservableProperty]
        private int _maxEurSlider = 388000;

        [ObservableProperty]
        private int _minKmhSlider = 40;

        [ObservableProperty]
        private int _maxKmhSlider = 65;

        [ObservableProperty]
        private int _minPsSlider = 100;

        [ObservableProperty]
        private int _maxPsSlider = 700;

        [ObservableProperty]
        private int _minJahrSlider = 1999;

        [ObservableProperty]
        private int _maxJahrSlider = 2021;

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
            ImageViewerService.Cat = "market";
            ImageViewerService.Manufacturer = tractor_in.Manufacturer;
            _navigationService.Navigate(typeof(ImageViewerPage));
        }

        [RelayCommand]
        public static void AddToCart(Tractor tractor)
        {
            CartItem<ItemisableBaseEntity> cartItem = new(tractor, tractor.SelectedQuantity);
            CartService.AddToCart(UserService.LoggedInUser!.Cart, cartItem);
        }

        [RelayCommand]
        public void ApplyFilter()
        {
            if(MinEurSlider >= MaxEurSlider)
            {
                MinEurSlider = MaxEurSlider;
            }
            else if(MinKmhSlider >= MaxKmhSlider)
            {
                MinKmhSlider = MaxKmhSlider;
            }
            else if(MinPsSlider >= MaxPsSlider)
            {
                MinPsSlider = MaxPsSlider;
            }else if(MinJahrSlider >= MaxJahrSlider)
            {
                MinJahrSlider = MaxJahrSlider;
            }
            Tractors = _tractorService.GetFilteredTractorsForCustomers(MinEurSlider, MaxEurSlider, MinKmhSlider, MaxKmhSlider, MinPsSlider, MaxPsSlider, MinJahrSlider, MaxJahrSlider);
        }

        [RelayCommand]
        public void ResetFilter()
        {
            MinEurSlider = 95000;
            MaxEurSlider = 388000;
            MinKmhSlider = 40;
            MaxKmhSlider = 65;
            MinPsSlider = 100;
            MaxPsSlider = 700;
            MinJahrSlider = 1999;
            MaxJahrSlider = 2021;
            Tractors = _tractorService.GetFilteredTractorsForCustomers(MinEurSlider, MaxEurSlider, MinKmhSlider, MaxKmhSlider, MinPsSlider, MaxPsSlider, MinJahrSlider, MaxJahrSlider);
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
