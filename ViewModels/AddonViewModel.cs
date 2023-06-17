﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public List<string> AddonFilters = new();


        [ObservableProperty]
        private bool _isAdmin = UserService.LoggedInUser!.IsAdmin;

        [ObservableProperty]
        private bool _isNotAdmin = !UserService.LoggedInUser!.IsAdmin;

        [ObservableProperty]
        private List<TractorAddon> _allAddons = new List<TractorAddon>();

        [ObservableProperty]
        private bool _selectedQuantity;

        [ObservableProperty]
        private bool _claasIsChecked;

        [ObservableProperty]
        private bool _deutzIsChecked;

        [ObservableProperty]
        private bool _fendtIsChecked;

        [ObservableProperty]
        private bool _jCBIsChecked;

        [ObservableProperty]
        private bool _johnDeereIsChecked;

        [ObservableProperty]
        private bool _kubotaIsChecked;

        [ObservableProperty]
        private bool _lindnerIsChecked;

        [ObservableProperty]
        private bool _masseyFergusonIsChecked;

        [ObservableProperty]
        private bool _newHollandIsChecked;

        [ObservableProperty]
        private bool _steyrIsChecked;

        [ObservableProperty]
        private bool _valtraIsChecked;


        partial void OnClaasIsCheckedChanged(bool value) {
            UpdateAllAddons("Claas", value);
        }

        partial void OnDeutzIsCheckedChanged(bool value)
        {
            UpdateAllAddons("Deutz", value);
        }

        partial void OnFendtIsCheckedChanged(bool value)
        {
            UpdateAllAddons("Fendt", value);
        }

        partial void OnJCBIsCheckedChanged(bool value)
        {
            UpdateAllAddons("JCB", value);
        }

        partial void OnJohnDeereIsCheckedChanged(bool value)
        {
            UpdateAllAddons("John Deere", value);
        }

        partial void OnKubotaIsCheckedChanged(bool value)
        {
            UpdateAllAddons("Kubota", value);
        }

        partial void OnLindnerIsCheckedChanged(bool value)
        {
            UpdateAllAddons("Lindner", value);
        }

        partial void OnMasseyFergusonIsCheckedChanged(bool value)
        {
            UpdateAllAddons("Massey Ferguson", value);
        }

        partial void OnNewHollandIsCheckedChanged(bool value)
        {
            UpdateAllAddons("New Holland", value);
        }

        partial void OnSteyrIsCheckedChanged(bool value)
        {
            UpdateAllAddons("Steyr", value);
        }

        partial void OnValtraIsCheckedChanged(bool value)
        {
            UpdateAllAddons("Valtra", value);
        }

        public void UpdateAllAddons(string addon_in, bool selected_in)
        {
            if (selected_in == true)
            {
                AddonFilters.Add(addon_in);
            }
            else
            {
                AddonFilters.Remove(addon_in);
            }

            if(IsAdmin)
            {
                AllAddons = _addonService.GetFilteredAdminAddons(AddonFilters);
            }
            else
            {
                AllAddons = _addonService.GetFilteredAddons(AddonFilters);
            }
        }

        public AddonViewModel(AddonService addonService, INavigationService navigationService, CartService cartService)
        {
            _navigationService = navigationService;
            _cartService = cartService;
            _addonService = addonService;

            if(IsAdmin)
            {
                AllAddons = _addonService.GetAllAdminAddons();
            }
            else
            {
                AllAddons = _addonService.GetAllAddons();
            }
        }

        [RelayCommand]
        private void OpenImageViewer(TractorAddon tractoraddon_in)
        {
            ImageViewerService.Name = tractoraddon_in.Name;
            ImageViewerService.Category = "addon";
            _navigationService.Navigate(typeof(ImageViewerPage));
        }

        public void OnNavigatedTo()
        {
            AddonFilters.Clear();

            if(AddonService.RelatedMarketProduct != "" )
            {
                DeutzIsChecked = false;
                FendtIsChecked = false;
                JCBIsChecked = false;
                JohnDeereIsChecked = false;
                KubotaIsChecked = false;
                LindnerIsChecked = false;
                MasseyFergusonIsChecked = false;
                NewHollandIsChecked = false;
                SteyrIsChecked = false;
                ValtraIsChecked = false;
                ClaasIsChecked = false;
            }

            if (AddonService.RelatedMarketProduct == "Claas")
            {
                ClaasIsChecked = true;
            }else if(AddonService.RelatedMarketProduct == "Deutz")
            {
                DeutzIsChecked = true;  
            }
            else if (AddonService.RelatedMarketProduct == "Fendt")
            {
                FendtIsChecked = true;
            }
            else if (AddonService.RelatedMarketProduct == "JCB")
            {
                JCBIsChecked = true;
            }
            else if (AddonService.RelatedMarketProduct == "John Deere")
            {
                JohnDeereIsChecked = true;
            }
            else if (AddonService.RelatedMarketProduct == "Kubota")
            {
                KubotaIsChecked = true; 
            }
            else if (AddonService.RelatedMarketProduct == "Lindner")
            {
                LindnerIsChecked = true;
            }
            else if (AddonService.RelatedMarketProduct == "Massey Ferguson")
            {
                MasseyFergusonIsChecked = true;
            }
            else if (AddonService.RelatedMarketProduct == "New Holland")
            {
                NewHollandIsChecked = true;
            }
            else if (AddonService.RelatedMarketProduct == "Steyr")
            {
                SteyrIsChecked = true;
            }
            else if (AddonService.RelatedMarketProduct == "Valtra")
            {
                ValtraIsChecked = true;
            }
        }

        public void OnNavigatedFrom()
        {
            AddonService.RelatedMarketProduct = "";
        }

        [RelayCommand]
        public static void AddToCart(TractorAddon tractorAddon)
        {
            CartItem<ItemisableBaseEntity> cartItem = new(tractorAddon, tractorAddon.SelectedQuantity);
            CartService.AddToCart(UserService.LoggedInUser!.Cart, cartItem);
        }
    }
}
