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
        private List<Tractor> _tractorsForCustomers = new List<Tractor>();

        [ObservableProperty]
        private List<Tractor> _tractorsForAdmin = new List<Tractor>();

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
            TractorsForCustomers = _tractorService.GetTractorsForCustomers();
            TractorsForAdmin = _tractorService.GetTractorsForAdmin();
            _isInitialized = true;
        }
    }
}
