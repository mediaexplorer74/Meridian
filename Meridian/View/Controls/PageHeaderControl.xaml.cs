﻿using Jupiter.Application;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Meridian.View.Controls
{
    public sealed partial class PageHeaderControl : UserControl
    {
        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(PageHeaderControl), new PropertyMetadata(default(string)));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public PageHeaderControl()
        {
            this.InitializeComponent();
        }

        private void PageHeader_Loaded(object sender, RoutedEventArgs e)
        {
            Shell.Current.DisplayModeChanged += Current_DisplayModeChanged;
            JupiterApp.Current.NavigationService.Frame.Navigated += Frame_Navigated;

            UpdateSpacier(Shell.Current.DisplayMode);
            UpdateBackButton();
        }

        private void Frame_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            UpdateBackButton();
        }

        private void PageHeader_Unloaded(object sender, RoutedEventArgs e)
        {
            Shell.Current.DisplayModeChanged -= Current_DisplayModeChanged;
            JupiterApp.Current.NavigationService.Frame.Navigated -= Frame_Navigated;
        }

        private void Current_DisplayModeChanged(object sender, SplitViewDisplayMode e)
        {
            UpdateSpacier(e);
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            if (JupiterApp.Current.NavigationService.CanGoBack)
                JupiterApp.Current.NavigationService.GoBack();
        }

        private void UpdateSpacier(SplitViewDisplayMode mode)
        {
            if (mode == SplitViewDisplayMode.Overlay)
            {
                VisualStateManager.GoToState(this, "HamburgerButtonState", false);
                TitleTextBlock.Text = Title; //required to reapply Typography.Capitals
            }
            else
            {
                VisualStateManager.GoToState(this, "NormalState", false);
                TitleTextBlock.Text = Title; //required to reapply Typography.Capitals
            }
        }


        private void UpdateBackButton()
        {
            bool hardwareButtonsPresented = ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons");
            BackButton.Visibility = JupiterApp.Current.NavigationService.CanGoBack && !hardwareButtonsPresented ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
