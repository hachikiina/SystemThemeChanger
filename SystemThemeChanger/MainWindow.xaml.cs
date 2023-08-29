using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Forms;
using SystemThemeChanger.Core;

namespace SystemThemeChanger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool IsLightTheme = false;
        NotifyIcon TrayIcon = new NotifyIcon();
        public MainWindow()
        {
            InitializeComponent();
            this.Hide();
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize"))
                {
                    if (key != null)
                    {
                        Object o = key.GetValue("SystemUsesLightTheme");
                        if (o != null && Type.GetTypeCode(o.GetType()) == TypeCode.Int32)
                        {
                            //System.Windows.MessageBox.Show((o as int?).ToString());
                            int keyValue;
                            if (int.TryParse((o as int?).ToString(), out keyValue))
                                IsLightTheme = Convert.ToBoolean(keyValue);
                        }
                    }

                }
            }
            catch (Exception)
            {
                // todo: remove this really
                throw;
            }

            
            //ni.Icon = Properties.Resources.dark_on_white_512_ico;
            if (IsLightTheme)
            {
                TrayIcon.Icon = Properties.Resources.dark_on_white_512_ico;
                TrayIcon.Text = "Switch Theme to Dark Mode";
            }
            else
            {
                TrayIcon.Icon = Properties.Resources.white_on_dark_512_ico;
                TrayIcon.Text = "Switch Theme to Light Mode";
            }
            TrayIcon.Visible = true;
            //TrayIcon.Text = "Switch Themes";
            TrayIcon.ContextMenuStrip = new ContextMenuStrip();
            //ni.ContextMenuStrip.Items.Add("Maximize", null, this.OnMaximize);
            TrayIcon.ContextMenuStrip.Items.Add("Switch to Dark Theme", null, this.SwitchToDark);
            TrayIcon.ContextMenuStrip.Items.Add("Switch to Light Theme", null, this.SwitchToLight);
            TrayIcon.ContextMenuStrip.Items.Add("Close", null, this.Close);
            TrayIcon.MouseClick +=
                delegate (object sender, System.Windows.Forms.MouseEventArgs args)
                {
                    if (args.Button != MouseButtons.Left) return;
                    Extensions.SwitchThemes(ref IsLightTheme, ref TrayIcon);
                };
        }

        private void SwitchToLight(object sender, EventArgs e)
        {
            Extensions.SetThemeToLight(ref IsLightTheme, ref TrayIcon);
        }

        private void SwitchToDark(object sender, EventArgs e)
        {
            Extensions.SetThemeToDark(ref IsLightTheme, ref TrayIcon);
        }

        private void Close(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
