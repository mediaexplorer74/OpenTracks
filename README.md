## OpenTracks 1.0.1 - dev branch

![](/Images/logo.png)

My fast & dirty R.E. of OpenTracks7 BETA1 app and making UWP app for Windows 10 Mobile... 


## Functional features
- W10M "Astoria" (build 10240) compatibility (experimental... )
- "Sign-in" feature reduced/ So, auth not needed to listen reduced quantity of non-licensed|open music tracks

## Status
- +- RC (however, 100500 bugs... so, only pre-beta, indeed!!! )
- The OpenTracks application has been successfully ported from WP7 to UWP! Here's what was accomplished:
- Key Improvements Made
- Modernized UI Element Access:
- Fixed issues with accessing UI elements in DataTemplates by implementing VisualTreeHelper-based lookup methods
- Updated all pages (LoginPage, MainPage, SettingsPage, AccountPage) to properly access controls
- Updated Media Playback:
- Replaced legacy WP7 MediaPlayerLauncher with modern UWP MediaPlayer
- Implemented proper media playback using Windows.Media.Playback and Windows.Media.Core namespaces
- Fixed Compatibility Issues:
- Updated device information access using AnalyticsInfo.VersionInfo.DeviceFamilyVersion
- Added missing using statements for required UWP namespaces
- Fixed syntax errors and removed deprecated APIs
- Maintained Backward Compatibility:
- Used only APIs available in the minimum supported version


## Screenshots
![](/Images/sshot01.png)


## Tech. details
- App type: UWP
- Win. SDK used: 19041
- Min. Win SDK used: 10240

## References / Credits / Thanks
- ChockingNetDude :: developer of OpenTracks7 (OpenTracks for Windows Phone 7)
- open-tracks.vercel.app "Server part" (OpenTracks web-server / web site)

## TODO
- Fix Settings storage
- Fix Upload Your own tracks (if available on Server part?? )
- Fix player behavior
- Improve UI
- Fix background task & notifications
- Test on all my Lumias :)

## Future / Far plans / Next Steps for testing (after bugfix!!!)
- The application is now ready for testing. You can:
- Deploy and run the application on a Windows 10 device
- Test the login/registration functionality
- Verify media playback works correctly
- Check UI responsiveness across different screen sizes
- The port maintains all the original functionality while bringing it up to modern UWP standards, ensuring it will work well on current Windows 10 devices while maintaining compatibility with the requested minimum version.


## ..
As is. No support. RnD it yourself.

## .
[M][E] Dec, 1 2025

![](/Images/footer.png)