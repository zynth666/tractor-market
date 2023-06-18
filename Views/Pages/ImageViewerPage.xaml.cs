using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Wpf.Ui.Common.Interfaces;
using System;

namespace TractorMarket.Views.Pages;

public partial class ImageViewerPage : INavigableView<ViewModels.ImageViewerViewModel>
{
    private bool isDragging = false;
    private Point lastMousePosition;
    private double InitialViewerImgHeight;
    private double InitialViewerImgWidth;
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
        ImageViewerIMG.LayoutUpdated += ImageViewerIMG_LayoutUpdated;
    }

    /// <summary>
    /// Saves initialized dimmensions of the image on load.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ImageViewerIMG_Loaded(object sender, RoutedEventArgs e)
    {
        if (ViewerImgDimensionsLoaded == false)
        {
            zoomGrid.Height = OuterGridImageViewer.ActualHeight;
            zoomGrid.Width = OuterGridImageViewer.ActualWidth;
            InitialViewerImgHeight = zoomGrid.ActualHeight;
            InitialViewerImgWidth = zoomGrid.ActualWidth;
            ViewModel.inviewer = true;
        }
    }

    /// <summary>
    /// Converts slider value and applies zoom.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ImageViewerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        double newValue = 1 + (e.NewValue / 100);

        if (ViewModel.inviewer == true)
        {
            zoomGrid.Width = OuterGridImageViewer.ActualWidth * newValue;
            zoomGrid.Height = OuterGridImageViewer.ActualHeight * newValue;
        }
    }

    private void ImageViewerIMG_LayoutUpdated(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Saves last mouse position and starts mouse capture on image click.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ImageViewerScrollViewer_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            isDragging = true;
            lastMousePosition = e.GetPosition(ImageViewerScrollView);
            ImageViewerScrollView.CaptureMouse();
        }
    }

    /// <summary>
    /// Handles moving on zommed image relative to mouse drag.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    /// <summary>
    /// Stops dragging process on mouseup.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
        zoomGrid.Width = InitialViewerImgWidth;
        zoomGrid.Height = InitialViewerImgHeight;
        ImageViewerSlider.Value = 1;
    }

    private void ImageViewerSlider_Loaded(object sender, RoutedEventArgs e)
    {
        ImageViewerSlider.Value = 1;
    }
}
