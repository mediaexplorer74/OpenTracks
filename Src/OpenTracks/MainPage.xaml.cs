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
        private bool isSeeking;

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
                var playPauseButton = FindFirstChild<Button>(this, "PlayPauseButton");
                if (playPauseButton == null)
                    return;

                var icon = playPauseButton.Content as SymbolIcon;
                var state = mediaPlayer.PlaybackSession.PlaybackState;

                switch (state)
                {
                    case Windows.Media.Playback.MediaPlaybackState.Playing:
                        playPauseButton.IsEnabled = true;
                        if (icon != null)
                            icon.Symbol = Symbol.Pause;
                        break;
                    case Windows.Media.Playback.MediaPlaybackState.Paused:
                        playPauseButton.IsEnabled = true;
                        if (icon != null)
                            icon.Symbol = Symbol.Play;
                        break;
                    case Windows.Media.Playback.MediaPlaybackState.Buffering:
                    case Windows.Media.Playback.MediaPlaybackState.Opening:
                        playPauseButton.IsEnabled = false;
                        if (icon != null)
                            icon.Symbol = Symbol.Play;
                        break;
                    default:
                        playPauseButton.IsEnabled = false;
                        if (icon != null)
                            icon.Symbol = Symbol.Play;
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

            // Adjust player colors for light theme
            if (themeName == "Light")
            {
                var accent = Windows.UI.Color.FromArgb(255, 27, 161, 226);
                if (MiniPlayerBar != null)
                {
                    MiniPlayerBar.Background = new SolidColorBrush(Windows.UI.Colors.White);
                    MiniPlayerBar.BorderBrush = new SolidColorBrush(accent);
                }
                if (MiniPlayerTitle != null)
                {
                    MiniPlayerTitle.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
                }
            }

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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var miniPlayerTitle = FindFirstChild<TextBlock>(this, "MiniPlayerTitle");
            if (miniPlayerTitle != null)
            {
                if (this.viewModel.CurrentTrack != null && !string.IsNullOrEmpty(this.viewModel.CurrentTrack.Title))
                    miniPlayerTitle.Text = this.viewModel.CurrentTrack.Title;
                else
                    miniPlayerTitle.Text = "No track selected";
            }
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
            var miniPlayerTitle = FindFirstChild<TextBlock>(this, "MiniPlayerTitle");

            if (selectedTrack == null || string.IsNullOrEmpty(selectedTrack.AudioPath))
            {
                if (miniPlayerTitle != null)
                    miniPlayerTitle.Text = string.IsNullOrEmpty(selectedTrack?.Title) ? "Loading.." : selectedTrack.Title;

                var playPauseButton = FindFirstChild<Button>(this, "PlayPauseButton");
                if (playPauseButton != null)
                {
                    playPauseButton.IsEnabled = false;
                    var icon = playPauseButton.Content as SymbolIcon;
                    if (icon != null)
                        icon.Symbol = Symbol.Play;
                }
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
                        var mediaSource = Windows.Media.Core.MediaSource.CreateFromUri(new Uri(selectedTrack.AudioPath));
                        this.mediaPlayer.Source = mediaSource;
                        this.mediaPlayer.Play();

                        this.playbackTimer = new DispatcherTimer();
                        this.playbackTimer.Interval = TimeSpan.FromSeconds(1.0);
                        this.playbackTimer.Tick += PlaybackTimer_Tick;
                        this.playbackTimer.Start();

                        var playPauseButton = FindFirstChild<Button>(this, "PlayPauseButton");
                        if (playPauseButton != null)
                        {
                            playPauseButton.IsEnabled = true;
                            var icon = playPauseButton.Content as SymbolIcon;
                            if (icon != null)
                                icon.Symbol = Symbol.Pause;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("[ex] MainPage - HandleTrackSelection error: " + ex.Message);
                    }
                };
                this.delayTimer.Start();
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

            // Update progress slider (0-100%)
            var fullPlayerProgress = FindFirstChild<Slider>(this, "FullPlayerProgress");
            if (fullPlayerProgress != null && !isSeeking)
            {
                if (duration.TotalSeconds > 0)
                {
                    double percent = position.TotalSeconds / duration.TotalSeconds * 100.0;
                    if (!double.IsNaN(percent) && !double.IsInfinity(percent))
                    {
                        if (percent < 0) percent = 0;
                        if (percent > 100) percent = 100;
                        fullPlayerProgress.Value = percent;
                    }
                }
                else
                {
                    fullPlayerProgress.Value = 0;
                }
            }
        }

        private void SeekFromSlider()
        {
            if (mediaPlayer?.PlaybackSession == null)
                return;

            var fullPlayerProgress = FindFirstChild<Slider>(this, "FullPlayerProgress");
            if (fullPlayerProgress == null)
                return;

            TimeSpan duration = mediaPlayer.PlaybackSession.NaturalDuration;
            if (duration.TotalSeconds <= 0)
                return;

            double percent = fullPlayerProgress.Value;
            double targetSeconds = duration.TotalSeconds * (percent / 100.0);
            if (double.IsNaN(targetSeconds) || double.IsInfinity(targetSeconds))
                return;

            try
            {
                mediaPlayer.PlaybackSession.Position = TimeSpan.FromSeconds(targetSeconds);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex] MainPage - SeekFromSlider error: " + ex.Message);
            }
        }

        private void FullPlayerProgress_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            isSeeking = true;
        }

        private void FullPlayerProgress_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            SeekFromSlider();
            isSeeking = false;
        }

        private void FullPlayerProgress_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (!isSeeking)
                return;
            // Фактическое обновление позиции делаем только при отпускании ползунка в SeekFromSlider
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            PlayAdjacentTrack(+1);
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            PlayAdjacentTrack(-1);
        }

        private void PlayAdjacentTrack(int offset)
        {
            if (this.viewModel == null || this.viewModel.CurrentTrack == null)
                return;

            var current = this.viewModel.CurrentTrack;

            // Try TopTracks first
            if (this.viewModel.TopTracks != null)
            {
                var list = new List<TrackItem>(this.viewModel.TopTracks);
                int idx = list.IndexOf(current);
                if (idx >= 0 && list.Count > 0)
                {
                    int target = NormalizeIndex(idx + offset, list.Count);
                    HandleTrackSelection(list[target], true);
                    return;
                }
            }

            // Then AllTracks
            if (this.viewModel.AllTracks != null)
            {
                var list = new List<TrackItem>(this.viewModel.AllTracks);
                int idx = list.IndexOf(current);
                if (idx >= 0 && list.Count > 0)
                {
                    int target = NormalizeIndex(idx + offset, list.Count);
                    HandleTrackSelection(list[target], false);
                    return;
                }
            }
        }

        private int NormalizeIndex(int index, int length)
        {
            if (length <= 0)
                return 0;

            if (index < 0)
                index = length - 1;
            if (index >= length)
                index = 0;

            return index;
        }

        private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (mediaPlayer == null)
                return;

            switch (mediaPlayer.PlaybackSession.PlaybackState)
            {
                case Windows.Media.Playback.MediaPlaybackState.Playing:
                    mediaPlayer.Pause();
                    var playPauseButton = FindFirstChild<Button>(this, "PlayPauseButton");
                    if (playPauseButton != null)
                    {
                        var icon = playPauseButton.Content as SymbolIcon;
                        if (icon != null)
                            icon.Symbol = Symbol.Play;
                    }
                    this.playbackTimer?.Stop();
                    break;
                case Windows.Media.Playback.MediaPlaybackState.Paused:
                    mediaPlayer.Play();
                    var playPauseButton2 = FindFirstChild<Button>(this, "PlayPauseButton");
                    if (playPauseButton2 != null)
                    {
                        var icon2 = playPauseButton2.Content as SymbolIcon;
                        if (icon2 != null)
                            icon2.Symbol = Symbol.Pause;
                    }
                    this.playbackTimer?.Start();
                    break;
            }
        }

        private void MiniPlayerBar_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var fullScreenPlayer = FindFirstChild<Grid>(this, "FullScreenPlayer");
            if (fullScreenPlayer != null)
                fullScreenPlayer.Visibility = Visibility.Visible;

            if (this.viewModel.CurrentTrack == null)
                return;

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
            //if (SignInButtonTopHits != null)  SignInButtonTopHits.Visibility = visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                
            //var signInButtonAllTracks = FindFirstChild<Button>(this, "SignInButtonAllTracks");
            // if (SignInButtonAllTracks != null) SignInButtonAllTracks.Visibility = visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

      
    }
}