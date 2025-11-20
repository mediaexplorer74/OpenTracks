// Decompiled with JetBrains decompiler
// Type: OpenTracksBETA.AccountPage
// Assembly: OpenTracksBETA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 37D9428F-DECE-49E6-8BF2-BFFD8E04DA19
// Assembly location: C:\Users\Admin\Desktop\RE\OpenTracksCE\OpenTracksBETA.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation.Metadata;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage;
using Windows.System.Profile; // Added for AnalyticsInfo
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

#nullable disable

namespace OpenTracksBETA
{
    public sealed partial class AccountPage : Page
    {
       
        public AccountPage()
        {
            this.InitializeComponent();
            this.Loaded += new RoutedEventHandler(this.AccountPage_Loaded);
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

        private void AccountPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.SlideInStoryboard.Begin();
            LoadUserData();
        }

        private async void LoadUserData()
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey("LoggedInEmail"))
            {
                this.EmailText.Text = localSettings.Values["LoggedInEmail"].ToString();
            }
            else
            {
                this.EmailText.Text = "Not available";
            }

            // Generate user ID
            var provider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha1);
            var hash = provider.HashData(CryptographicBuffer.ConvertStringToBinary(Guid.NewGuid().ToString(), BinaryStringEncoding.Utf8));
            this.UserIdText.Text = CryptographicBuffer.EncodeToHexString(hash).Substring(0, 8).ToUpper();

            // For device info in UWP, we use different APIs
            var deviceInfo = new Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation();
            this.DeviceNameText.Text = deviceInfo.FriendlyName;
            this.ManufacturerText.Text = deviceInfo.SystemManufacturer;
            
            // Use AnalyticsInfo to get OS version in UWP
            var osVersion = AnalyticsInfo.VersionInfo.DeviceFamilyVersion;
            this.OSVersionText.Text = GetOSVersionString(osVersion);
        }
        
        private string GetOSVersionString(string deviceFamilyVersion)
        {
            // Convert the device family version to a readable format
            try
            {
                ulong version = ulong.Parse(deviceFamilyVersion);
                ulong major = (version & 0xFFFF000000000000L) >> 48;
                ulong minor = (version & 0x0000FFFF00000000L) >> 32;
                ulong build = (version & 0x00000000FFFF0000L) >> 16;
                ulong revision = (version & 0x000000000000FFFFL);
                return $"{major}.{minor}.{build}.{revision}";
            }
            catch
            {
                return "Unknown";
            }
        }

        private async void Logout_Click(object sender, RoutedEventArgs e)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey("LoggedInEmail"))
            {
                localSettings.Values.Remove("LoggedInEmail");
            }

            var dialog = new Windows.UI.Popups.MessageDialog("👋 You've been logged out.");
            await dialog.ShowAsync();

            // Navigate back to main page
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(MainPage));
            }

            this.SlideOutStoryboard.Begin();
        }
    }
}