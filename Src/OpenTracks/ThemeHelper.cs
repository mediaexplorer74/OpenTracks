// Decompiled with JetBrains decompiler
// Type: OpenTracksBETA.ThemeHelper
// Assembly: OpenTracksBETA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 37D9428F-DECE-49E6-8BF2-BFFD8E04DA19
// Assembly location: C:\Users\Admin\Desktop\RE\OpenTracksCE\OpenTracksBETA.dll

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Foundation; // Added for Point type
using Windows.UI; // Added for Colors

#nullable disable

namespace OpenTracksBETA
{
    public static class ThemeHelper
    {
        public static Brush GetGradientBrush()
        {
            var gradientBrush = new LinearGradientBrush();
            gradientBrush.StartPoint = new Point(0.0, 0.0);
            gradientBrush.EndPoint = new Point(1.0, 0.0);
            gradientBrush.GradientStops.Add(new GradientStop() { Color = Colors.Purple, Offset = 0.0 });
            gradientBrush.GradientStops.Add(new GradientStop() { Color = Colors.Orange, Offset = 1.0 });
            return gradientBrush;
        }

        public static Brush GetGradientBackground()
        {
            var gradientBackground = new LinearGradientBrush();
            gradientBackground.StartPoint = new Point(0.0, 0.0);
            gradientBackground.EndPoint = new Point(1.0, 1.0);
            gradientBackground.GradientStops.Add(new GradientStop() { Color = Colors.Purple, Offset = 0.0 });
            gradientBackground.GradientStops.Add(new GradientStop() { Color = Colors.Orange, Offset = 1.0 });
            return gradientBackground;
        }

        public static void ApplyTheme(string themeName, Grid layoutRoot)
        {
            switch (themeName)
            {
                case "Light":
                    ThemeHelper.UpdateResource("HubTitleBrush", new SolidColorBrush(Colors.White));
                    layoutRoot.Background = new ImageBrush()
                    {
                        ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/bg1.png"))
                    };
                    break;
                case "Gradient":
                    ThemeHelper.UpdateResource("HubTitleBrush", new SolidColorBrush(Colors.White));
                    layoutRoot.Background = ThemeHelper.GetGradientBackground();
                    break;
                default:
                    layoutRoot.Background = new SolidColorBrush(Colors.Black);
                    break;
            }
        }

        private static void UpdateResource(string key, object value)
        {
            if (Application.Current.Resources.ContainsKey(key))
                Application.Current.Resources.Remove(key);
            Application.Current.Resources.Add(key, value);
        }
    }
}