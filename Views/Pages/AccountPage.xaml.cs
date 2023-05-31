using Wpf.Ui.Common.Interfaces;

namespace TractorMarket.Views.Pages
{
    /// <summary>
    /// Interaction logic for AccountPage.xaml
    /// </summary>
    public partial class AccountPage : INavigableView<ViewModels.AccountViewModel>
    {
        public ViewModels.AccountViewModel ViewModel
        {
            get;
        }

        public AccountPage(ViewModels.AccountViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();
        }
    }
}