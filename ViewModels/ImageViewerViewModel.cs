using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using TractorMarket.Services;
using TractorMarket.Views.Pages;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace TractorMarket.ViewModels;

public partial class ImageViewerViewModel : ObservableObject, INavigationAware
{
    private INavigationService _navigationService;
    public bool inviewer = false;
    public event Action? ShowNavigation;
    public event Action? HideNavigation;

    [ObservableProperty]
    private string _name = ImageViewerService.Name;

    [ObservableProperty]
    private string _type = ImageViewerService.Type;

    [ObservableProperty]
    private string _manufacturer = ImageViewerService.Manufacturer;

    [ObservableProperty]
    private int _zoomSliderVal = 0;

    public ImageViewerViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    /// <summary>
    /// Reset certain Bindings when navigating to this view to get rid of stale data
    /// in case the data in the ImageViewerService has changed and hides the navigation.
    /// </summary>
    public void OnNavigatedTo()
    {
        Name = ImageViewerService.Name;
        Type = ImageViewerService.Type;
        Manufacturer = ImageViewerService.Manufacturer;
        HideNavigation?.Invoke();
    }

    public void OnNavigatedFrom()
    {
        inviewer = false;
    }

    /// <summary>
    /// Navigates back to the Page from which you opened the ImageViewer and shows the navigation.
    /// </summary>
    [RelayCommand]
    private void LeaveImageViewer()
    {
        if (ImageViewerService.Category == "addon") {
            _navigationService.Navigate(typeof(AddonPage));
        }
        else if(ImageViewerService.Category == "market") {
            _navigationService.Navigate(typeof(MarketPage));
        }
        else if(ImageViewerService.Category == "cart")
        {
            _navigationService.Navigate(typeof(CartPage));
        }
        inviewer = false;
        ShowNavigation?.Invoke();
    }
}