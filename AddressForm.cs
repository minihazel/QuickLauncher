using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickLauncher
{
    public partial class AddressForm : Form
    {
        public AddressForm()
        {
            InitializeComponent();
        }

        private void AddressForm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.fikaAddress))
            {
                valueAddress.Text = "https://" + Properties.Settings.Default.fikaAddress + ":6969";
            }

            if (!string.IsNullOrEmpty(Properties.Settings.Default.fikaAID))
            {
                valueProfile.Text = Properties.Settings.Default.fikaAID;
            }
        }

        public bool purifyAndValidateAddress(string input, out string purifiedIp)
        {
            purifiedIp = null;

            string prependedUrl = input;
            if (!input.Contains("://"))
            {
                prependedUrl = "http://" + input;
            }

            if (!Uri.TryCreate(prependedUrl, UriKind.Absolute, out Uri uri))
            {
                return false;
            }

            string hostPart = uri.Host;

            if (!IPAddress.TryParse(hostPart, out IPAddress ipAddress))
            {
                return false;
            }

            var addressFamily = ipAddress.AddressFamily;
            if (addressFamily != AddressFamily.InterNetwork && addressFamily != AddressFamily.InterNetworkV6)
            {
                return false;
            }

            purifiedIp = hostPart;
            return true;
        }

        private void btnClearAddress_Click(object sender, EventArgs e)
        {
            valueAddress.Clear();
        }

        private void btnConfused_Click(object sender, EventArgs e)
        {
            string content = $"1. If you have Fika installed, and the host computer is on the same WiFi/Ethernet network as you, then use the host computer\'s IPv4 IP address and port." + Environment.NewLine +
                             $"2. If you have Fika installed, and the host computer is NOT on the same WiFi/Ethernet network as you, then use the host computer\'s public IP address and port." + Environment.NewLine +
                             $"3. If you have Fika installed, and the host computer is this computer, then use 127.0.0.1 and the server\'s port.";
            
            MessageBox.Show(content, Text, MessageBoxButtons.OK);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            string ip = valueAddress.Text;
            string aid = valueProfile.Text;

            if (aid.Contains(".json"))
            {
                aid = aid.Replace(".json", string.Empty);
            }

            if (purifyAndValidateAddress(ip, out string purifiedIP))
            {
                string content = "Apply settings?";
                if (MessageBox.Show(content, Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Properties.Settings.Default.fikaAddress = purifiedIP;
                    Properties.Settings.Default.fikaAID = aid;

                    Properties.Settings.Default.Save();
                    Close();
                }
            }
            else
            {
                string content = "Could not validate IP address, please only provide the digits (i.e 192.168.1.1) and try again.";
                MessageBox.Show(content, Text, MessageBoxButtons.OK);
                return;
            }
        }

        private void btnClearProfile_Click(object sender, EventArgs e)
        {
            valueProfile.Clear();
        }

        private void valueAddress_TextChanged(object sender, EventArgs e)
        {
            string ip = valueAddress.Text;

            if (purifyAndValidateAddress(ip, out string purifiedIP))
            {
                lblSanitizedInput.Text = "Sanitized input: " + purifiedIP;
            }
        }
    }
}
