using System.Diagnostics;
using Wpf.Ui.Common.Interfaces;

namespace TractorMarket.Views.Pages;

/// <summary>
/// Interaction logic for MarketView.xaml
/// </summary>
public partial class MarketPage : INavigableView<ViewModels.MarketViewModel>
{
    public ViewModels.MarketViewModel ViewModel
    {
        get;
    }

    public MarketPage(ViewModels.MarketViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}
