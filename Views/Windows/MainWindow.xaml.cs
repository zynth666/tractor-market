﻿using System;
using System.Windows;
using System.Windows.Controls;
using TractorMarket.ViewModels;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace TractorMarket.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INavigationWindow
    {
        public MainWindowViewModel ViewModel
        {
            get;
        }

        public MainWindow(MainWindowViewModel viewModel, LoginViewModel loginViewModel, RegisterViewModel registerViewModel, IPageService pageService, INavigationService navigationService)
        {
            ViewModel = viewModel;
            DataContext = this;

            loginViewModel.ProcessLogin += ShowNavigation;
            registerViewModel.ProcessRegister += ShowNavigation;
            viewModel.ProcessLogout += HideNavigation;

            InitializeComponent();
            SetPageService(pageService);
            navigationService.SetNavigationControl(RootNavigation);
        }

        #region INavigationWindow methods

        public Frame GetFrame()
            => RootFrame;

        public INavigation GetNavigation()
            => RootNavigation;

        public bool Navigate(Type pageType)
            => RootNavigation.Navigate(pageType);

        public void SetPageService(IPageService pageService)
            => RootNavigation.PageService = pageService;

        public void ShowWindow()
            => Show();

        public void CloseWindow()
            => Close();

        #endregion INavigationWindow methods

        /// <summary>
        /// Raises the closed event.
        /// </summary>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Make sure that closing this window will begin the process of closing the application.
            Application.Current.Shutdown();
        }

        private void ShowNavigation()
        {
            NavigationColumnDefinition.Width = GridLength.Auto;
            RootNavigation.Visibility = Visibility.Visible;
            RootBreadcrumb.Visibility = Visibility.Visible;
            Navigate(typeof(Pages.AccountPage));
        }

        private void HideNavigation()
        {
            NavigationColumnDefinition.Width = new GridLength(0);
            RootNavigation.Visibility = Visibility.Hidden;
            RootBreadcrumb.Visibility = Visibility.Hidden;
        }
    }
}