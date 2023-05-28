using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TractorMarket.Helpers;
using Wpf.Ui.Common.Interfaces;

namespace TractorMarket.ViewModels
{
    public partial class LoginViewModel : ObservableObject, INavigationAware
    {
        private RefreshDatabase _refreshDatabaseHelper;

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
        private void OnRefreshDatabaseButtonClick()
        {
            _refreshDatabaseHelper.Execute();
        }
    }
}
