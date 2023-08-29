using Microsoft.Win32;
using System.Windows.Forms;

namespace SystemThemeChanger.Core
{
    internal static class Extensions
    {
        internal static void SetThemeToLight(ref bool isLightMode, ref NotifyIcon icon)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", true))
            {
                if (key != null)
                {
                    key.SetValue("SystemUsesLightTheme", 1);
                    isLightMode = true;
                    icon.Icon = Properties.Resources.dark_on_white_512_ico;
                    icon.Text = "Switch Theme to Dark Mode";
                }
            }
        }
        internal static void SetThemeToDark(ref bool isLightMode, ref NotifyIcon icon)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", true))
            {
                if (key != null)
                {
                    key.SetValue("SystemUsesLightTheme", 0);
                    isLightMode = false;
                    icon.Icon = Properties.Resources.white_on_dark_512_ico;
                    icon.Text = "Switch Theme to Light Mode";
                }
            }
        }

        internal static void SwitchThemes(ref bool isLightMode, ref NotifyIcon icon)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", true))
            {
                if (key != null)
                {
                    if (isLightMode) // set to dark
                    {
                        key.SetValue("SystemUsesLightTheme", 0);
                        isLightMode = false;
                        icon.Icon = Properties.Resources.white_on_dark_512_ico;
                        icon.Text = "Switch Theme to Light Mode";
                    }
                    else // set to light
                    {
                        key.SetValue("SystemUsesLightTheme", 1);
                        isLightMode = true;
                        icon.Icon = Properties.Resources.dark_on_white_512_ico;
                        icon.Text = "Switch Theme to Dark Mode";
                    }
                }
            }
        }
    }
}
