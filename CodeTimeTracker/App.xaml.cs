using Microsoft.Win32;
using System.Configuration;
using System.Data;
using System.Windows;

namespace CodeTimeTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        bool _isDarkMode = false;
        public App()
        {
            SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
            LoadThemeAccordingToSystem();
        }
        private void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            if (e.Category == UserPreferenceCategory.General)
            {
                LoadThemeAccordingToSystem();
            }
        }

        private void LoadThemeAccordingToSystem()
        {
            _isDarkMode = IsWindowsDarkThemeActive();
            var themeUri = _isDarkMode ? "pack://application:,,,/CodeTimeTracker;component/Themes/DarkTheme.xaml" : "pack://application:,,,/CodeTimeTracker;component/Themes/LightTheme.xaml";
            var theme = new ResourceDictionary() { Source = new System.Uri(themeUri)  };
            
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(theme);
        }

        private bool IsWindowsDarkThemeActive()
        {
            const string registryKeyPath = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
            const string registryValueName = "AppsUseLightTheme";

            object? registryValueObj = Registry.GetValue(registryKeyPath, registryValueName, null);
            if (registryValueObj == null)
                return false;

            int registryValue = (int)registryValueObj;
            return registryValue <= 0;
        }
    }

}
