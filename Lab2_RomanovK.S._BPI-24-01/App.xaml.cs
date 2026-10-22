using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Lab2_RomanovK.S._BPI_24_01
{
    public enum AppTheme { Light, Dark }

    public partial class App : Application
    {
        public static AppTheme CurrentTheme { get; private set; } = AppTheme.Light;

        public static void ApplyTheme(AppTheme theme)
        {
            CurrentTheme = theme;

            string themeFile = theme == AppTheme.Dark
                ? "Themes/DarkTheme.xaml"
                : "Themes/LightTheme.xaml";

            var newTheme = new ResourceDictionary
            {
                Source = new Uri(themeFile, UriKind.Relative)
            };

            var dicts = Current.Resources.MergedDictionaries;

            for (int i = dicts.Count - 1; i >= 0; i--)
            {
                if (dicts[i].Source != null &&
                    dicts[i].Source.OriginalString.Contains("Theme"))
                {
                    dicts.RemoveAt(i);
                }
            }

            dicts.Insert(0, newTheme);
        }
    }
}
