using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TractorMarket.Entities;
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

        private INavigationService _navigationService;

        [ObservableProperty]
        private List<Tractor> _tractorsForCustomers = new List<Tractor>();

        [ObservableProperty]
        private List<Tractor> _tractorsForAdmin = new List<Tractor>();

        public MarketViewModel(TractorService tractorService, INavigationService navigationService)
        {
            _tractorService = tractorService;
            _navigationService = navigationService;
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
            Debug.WriteLine("OPEN IMAGE VIEWER: " + tractor_in);
            ImageViewerService.Type = tractor_in.Type;
            ImageViewerService.Manufacturer = tractor_in.Manufacturer;

            _navigationService.Navigate(typeof(ImageViewerPage));
        }

    }
}
