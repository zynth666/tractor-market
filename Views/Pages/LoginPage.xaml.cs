using Wpf.Ui.Common.Interfaces;

namespace TractorMarket.Views.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : INavigableView<ViewModels.LoginViewModel>
    {
        public ViewModels.LoginViewModel ViewModel
        {
            get;
        }
        public LoginPage(ViewModels.LoginViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
        }
    }
}