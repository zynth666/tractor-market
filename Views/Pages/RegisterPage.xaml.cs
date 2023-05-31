using Wpf.Ui.Common.Interfaces;

namespace TractorMarket.Views.Pages;

public partial class RegisterPage : INavigableView<ViewModels.RegisterViewModel>
{
    public ViewModels.RegisterViewModel ViewModel
    {
        get;
    }

    public RegisterPage(ViewModels.RegisterViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}