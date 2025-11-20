// Decompiled with JetBrains decompiler
// Type: OpenTracksBETA.MainPage
// Assembly: OpenTracksBETA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 37D9428F-DECE-49E6-8BF2-BFFD8E04DA19
// Assembly location: C:\Users\Admin\Desktop\RE\OpenTracksCE\OpenTracksBETA.dll

using OpenTracksBETA.ViewModels;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Net.NetworkInformation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Playback;
using Windows.Media.Core;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.Foundation; // Added for Point type
using System.Collections.Generic; // Added for KeyValuePair

#nullable disable

namespace OpenTracksBETA
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel viewModel;
        private DispatcherTimer playbackTimer;
        private DispatcherTimer delayTimer;
        private MediaPlayer mediaPlayer;

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += new RoutedEventHandler(this.MainPage_Loaded);
            this.viewModel = new MainViewModel();
            this.DataContext = this.viewModel;
            
            // We'll set the ItemsSource in code-behind since the ListView is in a DataTemplate
            this.mediaPlayer = new MediaPlayer();
            this.mediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
            this.mediaPlayer.MediaFailed += MediaPlayer_MediaFailed;
            this.mediaPlayer.CurrentStateChanged += MediaPlayer_CurrentStateChanged;
        }

        private void MediaPlayer_CurrentStateChanged(MediaPlayer sender, object args)
        {
            var dispatcher = Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher;
            dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                switch (mediaPlayer.PlaybackSession.PlaybackState)
                {
                    case Windows.Media.Playback.MediaPlaybackState.Playing:
                        // Find the PlayPauseButton in the visual tree
                        var playPauseButton = FindFirstChild<Button>(this, "PlayPauseButton");
                        if (playPauseButton != null)
                            playPauseButton.Content = "||";
                        break;
                    case Windows.Media.Playback.MediaPlaybackState.Paused:
                        // Find the PlayPauseButton in the visual tree
                        var playPauseButton2 = FindFirstChild<Button>(this, "PlayPauseButton");
                        if (playPauseButton2 != null)
                            playPauseButton2.Content = "▶";
                        break;
                }
            });
        }

        private void MediaPlayer_MediaFailed(MediaPlayer sender, MediaPlayerFailedEventArgs args)
        {
            var dispatcher = Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher;
            dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                var dialog = new Windows.UI.Popups.MessageDialog("Failed to load media: " + args.ErrorMessage);
                await dialog.ShowAsync();
            });
        }

        private void MediaPlayer_MediaOpened(MediaPlayer sender, object args)
        {
            var dispatcher = Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher;
            dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                // Media is ready to play
            });
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            string themeName = "Dark";
            if (localSettings.Values.ContainsKey("AppTheme"))
                themeName = localSettings.Values["AppTheme"].ToString();
            ThemeHelper.ApplyTheme(themeName, this.LayoutRoot);
            
            TextBlock textBlock1 = new TextBlock();
            textBlock1.Text = "OpenTracks";
            textBlock1.FontSize = 186.0;
            textBlock1.FontWeight = Windows.UI.Text.FontWeights.ExtraLight;
            textBlock1.Margin = new Thickness(12.0, 24.0, 0.0, 0.0);
            textBlock1.VerticalAlignment = VerticalAlignment.Center;
            TextBlock textBlock2 = textBlock1;
            if (themeName == "Light" || themeName == "Gradient")
            {
                textBlock2.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
            }
            else
            {
                TextBlock textBlock3 = textBlock2;
                var linearGradientBrush = new LinearGradientBrush();
                linearGradientBrush.StartPoint = new Point(0.0, 0.0);
                linearGradientBrush.EndPoint = new Point(1.0, 0.0);
                linearGradientBrush.GradientStops.Add(new GradientStop() { Color = Windows.UI.Colors.Purple, Offset = 0.0 });
                linearGradientBrush.GradientStops.Add(new GradientStop() { Color = Windows.UI.Colors.Orange, Offset = 1.0 });
                textBlock3.Foreground = linearGradientBrush;
            }
            
            // Find the Pivot and set its title
            var mainPivot = FindFirstChild<Pivot>(this, "MainPivot");
            if (mainPivot != null)
                mainPivot.Title = textBlock2.Text;

            // --------------

            var connectionProfile = Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();
            bool isConnected = (connectionProfile != null 
                && connectionProfile.GetNetworkConnectivityLevel() 
                    == Windows.Networking.Connectivity.NetworkConnectivityLevel.InternetAccess);

            if (!isConnected)
            {
                Frame.Navigate(typeof(NoNetworkPage));
            }
            else
            {
                //var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                if (!localSettings.Values.ContainsKey("LoggedInEmail"))
                {
                    // Show sign in buttons, hide track lists
                    //SetUIVisibility("TopTrackList", Visibility.Collapsed);
                    //SetUIVisibility("TrackList", Visibility.Collapsed);
                    //SetUIVisibility("SignInButtonTopHits", Visibility.Visible);
                    SetUIVisibility("SignInButtonAllTracks", Visibility.Visible);
                }
                else
                {
                    // Hide sign in buttons, show track lists
                    //SetUIVisibility("TopTrackList", Visibility.Visible);
                    //SetUIVisibility("TrackList", Visibility.Visible);
                    //SetUIVisibility("SignInButtonTopHits", Visibility.Collapsed);
                    SetUIVisibility("SignInButtonAllTracks", Visibility.Collapsed);
                }
            }

        }

       

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            // Find MiniPlayerTitle and update it
            var miniPlayerTitle = FindFirstChild<TextBlock>(this, "MiniPlayerTitle");
            if (miniPlayerTitle != null)
            {
                if (this.viewModel.CurrentTrack != null && !string.IsNullOrEmpty(this.viewModel.CurrentTrack.Title))
                    miniPlayerTitle.Text = this.viewModel.CurrentTrack.Title;
                else
                    miniPlayerTitle.Text = "No track selected";
            }
        }

        private void PlaybackTimer_Tick(object sender, object e)
        {
            if (mediaPlayer?.PlaybackSession == null)
                return;
                
            TimeSpan position = mediaPlayer.PlaybackSession.Position;
            TimeSpan duration = mediaPlayer.PlaybackSession.NaturalDuration;
            int minutes = position.Minutes;
            string elapsedMinutes = minutes.ToString();
            string elapsedSeconds;
            if (position.Seconds >= 10)
            {
                elapsedSeconds = position.Seconds.ToString();
            }
            else
            {
                elapsedSeconds = "0" + position.Seconds.ToString();
            }
            
            // Find the FullPlayerElapsed text block
            var fullPlayerElapsed = FindFirstChild<TextBlock>(this, "FullPlayerElapsed");
            if (fullPlayerElapsed != null)
                fullPlayerElapsed.Text = elapsedMinutes + ":" + elapsedSeconds;
            
            int durationMinutes = duration.Minutes;
            string durationStr = durationMinutes.ToString();
            string durationSeconds;
            if (duration.Seconds >= 10)
            {
                durationSeconds = duration.Seconds.ToString();
            }
            else
            {
                durationSeconds = "0" + duration.Seconds.ToString();
            }
            
            // Find the FullPlayerDuration text block
            var fullPlayerDuration = FindFirstChild<TextBlock>(this, "FullPlayerDuration");
            if (fullPlayerDuration != null)
                fullPlayerDuration.Text = durationStr + ":" + durationSeconds;
        }


        private void TopTrackTapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender is ListView listView)
            {
                var item = (FrameworkElement)e.OriginalSource;
                while (item != null && !(item is ListViewItem))
                {
                    item = (FrameworkElement)item.Parent;
                }

                if (item != null)
                {
                    var dataContext = item.DataContext;
                    if (dataContext is TrackItem trackItem)
                    {
                        this.HandleTrackSelection(trackItem, true);
                    }
                }
            }
        }


        private void TrackSelected(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListView listView)
            {
                this.HandleTrackSelection(listView.SelectedItem as TrackItem, false);
                listView.SelectedItem = null;
            }
        }

private void TrackTapped(object sender, TappedRoutedEventArgs e)
{
    if (sender is ListView listView)
    {
        var item = (FrameworkElement)e.OriginalSource;
        while (item != null && !(item is ListViewItem))
        {
            item = (FrameworkElement)item.Parent;
        }

        if (item != null)
        {
            var dataContext = item.DataContext;
            if (dataContext is TrackItem trackItem)
            {
                this.HandleTrackSelection(trackItem, false);
            }
        }
    }
}


        private void TopTrackSelected(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListView listView)
            {
                this.HandleTrackSelection(listView.SelectedItem as TrackItem, true);
                listView.SelectedItem = null;
            }
        }

        private async void HandleTrackSelection(TrackItem selectedTrack, bool isTopTrack)
        {
            // Find MiniPlayerTitle
            var miniPlayerTitle = FindFirstChild<TextBlock>(this, "MiniPlayerTitle");
            
            if (selectedTrack == null || string.IsNullOrEmpty(selectedTrack.AudioPath))
            {
                if (miniPlayerTitle != null)
                    miniPlayerTitle.Text = string.IsNullOrEmpty(selectedTrack?.Title) ? "Loading.." : selectedTrack.Title;
            }
            else
            {
                this.viewModel.CurrentTrack = selectedTrack;
                string title = string.IsNullOrEmpty(selectedTrack.Title) ? "Loading..." : selectedTrack.Title;
                string artist = string.IsNullOrEmpty(selectedTrack.Artist) ? "Unknown Artist" : selectedTrack.Artist;
                
                if (miniPlayerTitle != null)
                    miniPlayerTitle.Text = title;
                    
                this.delayTimer = new DispatcherTimer();
                this.delayTimer.Interval = TimeSpan.FromMilliseconds(50.0);
                this.delayTimer.Tick += (s, args) =>
                {
                    this.delayTimer.Stop();
                    try
                    {
                        // Using MediaPlayer for UWP
                        var mediaSource = Windows.Media.Core.MediaSource.CreateFromUri(new Uri(selectedTrack.AudioPath));
                        this.mediaPlayer.Source = mediaSource;
                        this.mediaPlayer.Play();
                        
                        this.playbackTimer = new DispatcherTimer();
                        this.playbackTimer.Interval = TimeSpan.FromSeconds(1.0);
                        this.playbackTimer.Tick += PlaybackTimer_Tick;
                        this.playbackTimer.Start();
                        
                        // Update play button
                        var playPauseButton = FindFirstChild<Button>(this, "PlayPauseButton");
                        if (playPauseButton != null)
                            playPauseButton.Content = "||";
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("[ex] MainPage - HandleTrackSelection error: " + ex.Message);
                    }
                };
                this.delayTimer.Start();
            }
        }

        private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (mediaPlayer == null)
                return;
                
            switch (mediaPlayer.PlaybackSession.PlaybackState)
            {
                case Windows.Media.Playback.MediaPlaybackState.Playing:
                    mediaPlayer.Pause();
                    
                    // Update play button
                    var playPauseButton = FindFirstChild<Button>(this, "PlayPauseButton");
                    if (playPauseButton != null)
                        playPauseButton.Content = "▶";
                        
                    this.playbackTimer?.Stop();
                    break;
                case Windows.Media.Playback.MediaPlaybackState.Paused:
                    mediaPlayer.Play();
                    
                    // Update play button
                    var playPauseButton2 = FindFirstChild<Button>(this, "PlayPauseButton");
                    if (playPauseButton2 != null)
                        playPauseButton2.Content = "||";
                        
                    this.playbackTimer?.Start();
                    break;
            }
        }

        private void MiniPlayerBar_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            // Find FullScreenPlayer and show it
            var fullScreenPlayer = FindFirstChild<Grid>(this, "FullScreenPlayer");
            if (fullScreenPlayer != null)
                fullScreenPlayer.Visibility = Visibility.Visible;
                
            if (this.viewModel.CurrentTrack == null)
                return;
                
            // Update full player info
            var fullPlayerTitle = FindFirstChild<TextBlock>(this, "FullPlayerTitle");
            var fullPlayerArtist = FindFirstChild<TextBlock>(this, "FullPlayerArtist");
            
            if (fullPlayerTitle != null)
                fullPlayerTitle.Text = this.viewModel.CurrentTrack.Title;
            if (fullPlayerArtist != null)
                fullPlayerArtist.Text = this.viewModel.CurrentTrack.Artist;
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            // Find FullScreenPlayer and hide it
            var fullScreenPlayer = FindFirstChild<Grid>(this, "FullScreenPlayer");
            if (fullScreenPlayer != null)
                fullScreenPlayer.Visibility = Visibility.Collapsed;
        }

        private void GoToLibrary_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }

        private void GoToUpload_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            // Handle upload navigation in UWP way
        }

        private void GoToSettings_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage));
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
        
        // Helper method to find child elements by type only (for elements without names in DataTemplates)
        private T FindFirstElement<T>(DependencyObject parent) where T : FrameworkElement
        {
            if (parent is T element)
            {
                return element;
            }
            
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                var result = FindFirstElement<T>(child);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }
        
        // Helper method to set visibility of elements in DataTemplates
        private void SetUIVisibility(string elementName, Visibility visibility)
        {
            // ----------------------------
            // ToDo : if no visibility then show user's tracks from "local" storage? (or cloud storage?)
            // Update visibility for elements in Pivot
            //var topTrackList = FindFirstChild<ListView>(this, "TopTrackList");
            if (TopTrackList != null)
                TopTrackList.Visibility = Visibility.Visible;//visibility;
                
            //var trackList = FindFirstChild<ListView>(this, "TrackList");
            if (TrackList != null)
                TrackList.Visibility = Visibility.Visible;//visibility;

            // ------------------------------

            //var signInButtonTopHits = FindFirstChild<Button>(this, "SignInButtonTopHits");
            if (SignInButtonTopHits != null)
                SignInButtonTopHits.Visibility = visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                
            //var signInButtonAllTracks = FindFirstChild<Button>(this, "SignInButtonAllTracks");
            if (SignInButtonAllTracks != null)
                SignInButtonAllTracks.Visibility = visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

      
    }
}