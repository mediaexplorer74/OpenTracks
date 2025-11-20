// Decompiled with JetBrains decompiler
// Type: OpenTracksBETA.LoginPage
// Assembly: OpenTracksBETA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 37D9428F-DECE-49E6-8BF2-BFFD8E04DA19
// Assembly location: C:\Users\Admin\Desktop\RE\OpenTracksCE\OpenTracksBETA.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media; // Added for VisualTreeHelper

#nullable disable

namespace OpenTracksBETA
{
    public partial class LoginPage : Page
    {
        // Add fields to hold references to UI elements
        //private TextBox _emailBox;
        //private PasswordBox _passwordBox;
        //private TextBox _regEmailBox;
        //private PasswordBox _regPasswordBox;
        
        public LoginPage()
        {
            this.InitializeComponent();
            this.Loaded += new RoutedEventHandler(this.LoginPage_Loaded);
        }

        private void LoginPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Initialize UI element references
            InitializeUIElements();
            
            var localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey("LoggedInEmail"))
            {
                if (this.Frame != null)
                {
                    this.Frame.Navigate(typeof(AccountPage), localSettings.Values["LoggedInEmail"].ToString());
                }
            }
        }

        private void InitializeUIElements()
        {
            // Since elements are in DataTemplates, we need to find them after the template is applied
            // For simplicity, we'll access them directly in event handlers
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

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Get references to the elements when needed
            var emailBox = FindFirstChild<TextBox>(this, "EmailBox");
            var passwordBox = FindFirstChild<PasswordBox>(this, "PasswordBox");
            
            string email = emailBox?.Text.Trim() ?? "";
            string password = passwordBox?.Password.Trim() ?? "";
            
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                var dialog = new MessageDialog("Please enter both email and password.");
                await dialog.ShowAsync();
            }
            else
            {
                await PerformLogin(email, password);
            }
        }

        private async Task PerformLogin(string email, string password)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var formData = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("email", email),
                        new KeyValuePair<string, string>("password", password)
                    });

                    var response = await httpClient.PostAsync("http://opsrvlega.atwebpages.com/login.js", formData);
                    var result = await response.Content.ReadAsStringAsync();

                    if (result != null && result.Contains("success"))
                    {
                        var dialog = new MessageDialog("✅ Login Successful! Please restart your app and you will be able to start using our service!");
                        await dialog.ShowAsync();

                        var localSettings = ApplicationData.Current.LocalSettings;
                        localSettings.Values["LoggedInEmail"] = email;
                    }
                    else if (result.Contains("email_not_confirmed"))
                    {
                        var dialog = new MessageDialog("📧 Please confirm your email before logging in.");
                        await dialog.ShowAsync();
                    }
                    else
                    {
                        var dialog = new MessageDialog("⚠️ Invalid credentials.");
                        await dialog.ShowAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                var dialog = new MessageDialog($"❌ Login failed: {ex.Message}");
                await dialog.ShowAsync();
            }
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Get references to the elements when needed
            var regEmailBox = FindFirstChild<TextBox>(this, "RegEmailBox");
            var regPasswordBox = FindFirstChild<PasswordBox>(this, "RegPasswordBox");
            
            string email = regEmailBox?.Text.Trim() ?? "";
            string password = regPasswordBox?.Password.Trim() ?? "";
            
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                var dialog = new MessageDialog("⚠️ Please enter both email and password.", "Missing Info");
                await dialog.ShowAsync();
            }
            else
            {
                await PerformRegistration(email, password);
            }
        }

        private async Task PerformRegistration(string email, string password)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var jsonContent = $"{{\"email\":\"{email}\",\"password\":\"{password}\"}}";
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync("http://opsrvlega.atwebpages.com/register.js", content);
                    var result = await response.Content.ReadAsStringAsync();

                    if (result != null)
                    {
                        if (result.Contains("\"success\":true") || result.Contains("success"))
                        {
                            var dialog = new MessageDialog("✅ Registration Successful!\nYou can now log in to OpenTracks CE.", "Success");
                            await dialog.ShowAsync();
                        }
                        else if (result.Contains("already_exists"))
                        {
                            var dialog = new MessageDialog("⚠️ This email is already registered.\nTry logging in instead.", "Duplicate Account");
                            await dialog.ShowAsync();
                        }
                        else if (result.Contains("invalid"))
                        {
                            var dialog = new MessageDialog("❌ Invalid registration data.\nPlease check your input.", "Validation Error");
                            await dialog.ShowAsync();
                        }
                        else
                        {
                            var dialog = new MessageDialog($"⚠️ Server returned unexpected response:\n{result}", "Server Error");
                            await dialog.ShowAsync();
                        }
                    }
                    else
                    {
                        var dialog = new MessageDialog("⚠️ No response received from server.", "Empty Response");
                        await dialog.ShowAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                var dialog = new MessageDialog($"❌ Unexpected error:\n{ex.Message}", "Exception");
                await dialog.ShowAsync();
            }
        }
        
        // Helper method to find child elements in the visual tree
        private T FindFirstChild<T>(DependencyObject parent, string name) where T : FrameworkElement
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
        }
    }
}