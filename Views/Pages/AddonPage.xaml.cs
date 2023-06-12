using Wpf.Ui.Common.Interfaces;

namespace TractorMarket.Views.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class AddonPage : INavigableView<ViewModels.AddonViewModel>
    {
        public ViewModels.AddonViewModel ViewModel
        {
            get;
        }
        public AddonPage(ViewModels.AddonViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
        }
    }
}