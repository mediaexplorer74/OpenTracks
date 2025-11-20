// Decompiled with JetBrains decompiler
// Type: OpenTracksBETA.SettingsPage
// Assembly: OpenTracksBETA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 37D9428F-DECE-49E6-8BF2-BFFD8E04DA19
// Assembly location: C:\Users\Admin\Desktop\RE\OpenTracksCE\OpenTracksBETA.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media; // Added for VisualTreeHelper

#nullable disable

namespace OpenTracksBETA
{
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // ------------
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility
                = AppViewBackButtonVisibility.Visible;

            SystemNavigationManager.GetForCurrentView().BackRequested += (s, a) =>
            {
                // If we don't have proper parameters, go back
                if (Frame.CanGoBack)
                    Frame.GoBack();
                a.Handled = true;
            };

            if (ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1, 0))
            {
                Windows.Phone.UI.Input.HardwareButtons.BackPressed += (s, a) =>
                {
                    // If we don't have proper parameters, go back
                    if (Frame.CanGoBack)
                        Frame.GoBack();
                    a.Handled = true;
                };
            }
            // ------------   
        }

        private void NotificationsButton_Click(object sender, RoutedEventArgs e)
        {
            // Since the button is in a DataTemplate, we need to find it in the visual tree
            //var notificationsButton = FindFirstChild<Button>(this, "NotificationsButton");
            if (NotificationsButton != null)
            {
                if (NotificationsButton.Content.ToString().Contains("ON"))
                    NotificationsButton.Content = "Notifications: OFF";
                else
                    NotificationsButton.Content = "Notifications: ON";
            }
        }

        private void SyncButton_Click(object sender, RoutedEventArgs e)
        {
            // Since the button is in a DataTemplate, we need to find it in the visual tree
            //var syncButton = FindFirstChild<Button>(this, "SyncButton");
            if (SyncButton != null)
            {
                if (SyncButton.Content.ToString().Contains("ON"))
                    SyncButton.Content = "Auto Sync: OFF";
                else
                    SyncButton.Content = "Auto Sync: ON";
            }
        }

        private void ThemeRadio_Checked(object sender, RoutedEventArgs e)
        {
            if (!(sender is RadioButton radioButton) || radioButton.Tag == null)
            {
                return;
            }
            else
            {
                RadioButton rb = sender as RadioButton;
                string theme = rb.Tag.ToString();
                var localSettings = ApplicationData.Current.LocalSettings;
                localSettings.Values["AppTheme"] = theme;
            }               
        }
        
        // Helper method to find child elements in the visual tree
        /*private T FindFirstChild<T>(DependencyObject parent, string name) where T : FrameworkElement
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                
                if (child is T typedChild && typedChild.Name == name)
                {
                    return typedChild;
                }
                
                var result = FindFirstChild<T>(child, name);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }*/
    }
}