using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

        public bool TryPurifyAndValidateIp(string input, out string purifiedIp)
        {
            purifiedIp = null; // Initialize the output variable

            // 1. Prepend a dummy scheme if one is missing.
            // This allows Uri.TryCreate to correctly parse the string's structure,
            // especially if it contains a port number.
            string prependedUrl = input;
            if (!input.Contains("://"))
            {
                prependedUrl = "http://" + input;
            }

            // 2. Attempt to create the Uri object.
            if (!Uri.TryCreate(prependedUrl, UriKind.Absolute, out Uri uri))
            {
                return false; // The string structure is fundamentally invalid.
            }

            // 3. Extract the Host property. This is the SANITARY step:
            //    The Uri object automatically strips the scheme and port.
            string hostPart = uri.Host;

            // 4. Validate the extracted host part as a true IP address.
            if (!IPAddress.TryParse(hostPart, out IPAddress ipAddress))
            {
                return false; // The host is not a valid IP (e.g., it's a domain name or invalid numbers).
            }

            // 5. Optional: Ensure it's an IPv4 or IPv6 address.
            var family = ipAddress.AddressFamily;
            if (family != AddressFamily.InterNetwork && family != AddressFamily.InterNetworkV6)
            {
                return false;
            }

            // Success: Set the purified IP and return true.
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

            if (ip.Contains(":"))
            {
                string content = "Please remove the port from the IP address.";
                MessageBox.Show(content, Text, MessageBoxButtons.OK);
                return;
            }

            if (TryPurifyAndValidateIp(ip, out string purifiedIP))
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
    }
}
