using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WPF_CloverApp
{
    public partial class FormInfo : Form
    {
        public FormInfo()
        {
            InitializeComponent();
            Text = string.Format("About {0}", AssemblyTitle);
            labelVersion.Text = string.Format("Version {0}", AssemblyVersion);
            if (!Properties.Settings.Default.EnableDebug)
                btnDebug.Visible = false;
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
        #endregion

        private void labelArtistLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://twitter.com/3Renico");
            }
            catch (Exception ex) // shouldn't happen, but just in case...
            {
                MessageBox.Show(ex.Message, "something happened cloverPANIC");
            }
        }

        private void labelClover_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://twitter.com/cloverinari");
            }
            catch (Exception ex) // also shouldn't happen, but just in case...
            {
                MessageBox.Show(ex.Message, "something happened cloverPANIC");
            }
        }

        private void labelClover_DoubleClick(object sender, EventArgs e) // little easter egg.
        {
            MessageBox.Show("one stinky lil fox");
        }
    }
}
