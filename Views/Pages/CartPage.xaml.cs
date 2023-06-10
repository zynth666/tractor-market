using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace TractorMarket.Views.Pages;

/// <summary>
/// Interaction logic for CartPage.xaml
/// </summary>
public partial class CartPage : INavigableView<ViewModels.CartViewModel>
{
    public ViewModels.CartViewModel ViewModel
    {
        get;
    }

    public CartPage(ViewModels.CartViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}