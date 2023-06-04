using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;
using Wpf.Ui.Common.Interfaces;
using System.Diagnostics;
using System;

namespace TractorMarket.Views.Pages;

public partial class ImageViewerPage : INavigableView<ViewModels.ImageViewerViewModel>
{
    private bool isDragging = false;
    private Point lastMousePosition;
    private double InitialViewerImgHeight;
    private double InitialViewerImgWidth;
    private bool ImageViewerInitialized = false;
    private bool ViewerImgDimensionsLoaded = false;

    public ViewModels.ImageViewerViewModel ViewModel
    {
        get;
    }

    public ImageViewerPage(ViewModels.ImageViewerViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        zoomGrid.Loaded += ImageViewerIMG_Loaded;
        ImageViewerScrollView.PreviewMouseDown += ImageViewerScrollViewer_PreviewMouseDown;
        ImageViewerScrollView.PreviewMouseMove += ImageViewerScrollViewer_PreviewMouseMove;
        ImageViewerScrollView.PreviewMouseUp += ImageViewerScrollViewer_PreviewMouseUp;

    }

    private void ImageViewerIMG_Loaded(object sender, RoutedEventArgs e)
    {
        if(ViewerImgDimensionsLoaded == false)
        {

            Debug.WriteLine("LOADINGGER");

            zoomGrid.Width = OuterGridImageViewer.ActualWidth;
            zoomGrid.Height = OuterGridImageViewer.ActualHeight;

  
      
            ViewModel.inviewer = true;

        }
    }

    private void ImageViewerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        double newValue = 1+(e.NewValue/100);

        if(ViewModel.inviewer == true)
        {
          
            zoomGrid.Width = InitialViewerImgWidth* newValue;
            zoomGrid.Height = InitialViewerImgHeight* newValue;

        }
    }


    private void ImageViewerScrollViewer_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            isDragging = true;
            lastMousePosition = e.GetPosition(ImageViewerScrollView);
            ImageViewerScrollView.CaptureMouse();
        }
    }

    private void ImageViewerScrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
    {
        if (isDragging)
        {
            Point currentMousePosition = e.GetPosition(ImageViewerScrollView);
            double offsetX = currentMousePosition.X - lastMousePosition.X;
            double offsetY = currentMousePosition.Y - lastMousePosition.Y;

            ScrollViewer scrollViewer = (ScrollViewer)sender;
            scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - offsetX);
            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - offsetY);

            lastMousePosition = currentMousePosition;
        }
    }

    private void ImageViewerScrollViewer_PreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Released)
        {
            isDragging = false;
            ImageViewerScrollView.ReleaseMouseCapture();
        }
    }

    private void Unloaded_ImageView(object sender, RoutedEventArgs e)
    {

        ImageViewerSlider.Value = 1;
    }

    private void ImageViewerSlider_Loaded(object sender, RoutedEventArgs e)
    {
        ImageViewerSlider.Value = 1;


    }
}

