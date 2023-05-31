using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using TractorMarket.Entities;
using TractorMarket.Services;
using Wpf.Ui.Common.Interfaces;

namespace TractorMarket.ViewModels
{
    public partial class MarketViewModel : ObservableObject, INavigationAware
    {
        private bool _isInitialized = false;
        private TractorService _tractorService;

        [ObservableProperty]
        public List<Tractor> tractorsForCustomers = new List<Tractor>();

        [ObservableProperty]
        public List<Tractor> tractorsForAdmin = new List<Tractor>();

        public MarketViewModel(TractorService tractorService)
        {
            _tractorService = tractorService;
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
            tractorsForCustomers = _tractorService.GetTractorsForCustomers();
            tractorsForAdmin = _tractorService.GetTractorsForAdmin();

            _isInitialized = true;
        }
    }
}
