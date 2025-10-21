using QuickLauncher.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickLauncher
{
    public partial class mainForm : Form
    {
        public Color listBackcolor = Color.FromArgb(255, 28, 28, 28);
        public Color listSelectedcolor = Color.FromArgb(255, 40, 40, 40);
        public Color listHovercolor = Color.FromArgb(255, 35, 35, 35);
        public Color selectedOptionColor = Color.FromArgb(50, 50, 50);

        // strings
        public string currentDir = null;
        public string currentAID;
        public string ipAddress = "";
        private List<string> globalProcesses;

        // ints
        public int akiPort;

        // stringbuilders
        public StringBuilder serverOut;

        // bools
        public bool shouldServerOpen = false;
        public bool isFikaInstalled = false;

        // misc
        private Dictionary<Control, bool> originalControlStates = new Dictionary<Control, bool>();
        public Process server = null;
        public Process launcher = null;

        // background working
        BackgroundWorker CheckServerWorker;
        public BackgroundWorker TarkovProcessDetector;
        public BackgroundWorker TarkovEndDetector;
        public StringBuilder akiServerOutputter;
        public BackgroundWorker isServerOpen;
        DateTime startTime;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case 0x84: //WM_NCHITTEST
                    var result = (HitTest)m.Result.ToInt32();
                    if (result == HitTest.Left || result == HitTest.Right)
                        m.Result = new IntPtr((int)HitTest.Caption);
                    if (result == HitTest.TopLeft || result == HitTest.TopRight)
                        m.Result = new IntPtr((int)HitTest.Top);
                    if (result == HitTest.BottomLeft || result == HitTest.BottomRight)
                        m.Result = new IntPtr((int)HitTest.Bottom);

                    break;
            }
        }
        enum HitTest
        {
            Caption = 2,
            Transparent = -1,
            Nowhere = 0,
            Client = 1,
            Left = 10,
            Right = 11,
            Top = 12,
            TopLeft = 13,
            TopRight = 14,
            Bottom = 15,
            BottomLeft = 16,
            BottomRight = 17,
            Border = 18
        }

        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            // D:\SPT Iterations\4.0.0
            // currentDir = "D:\\SPT Iterations\\4.0.0";
            currentDir = Environment.CurrentDirectory;

            if (Directory.Exists(currentDir))
            {
                string fikaPath = Path.Combine(currentDir, "BepInEx", "plugins", "Fika");
                string fikaCorePath = Path.Combine(fikaPath, "Fika.Core.dll");

                bool doesFikaFolderExist = Directory.Exists(fikaPath);
                bool doesFikaCoreExist = File.Exists(fikaCorePath);

                lblLimit1.Select();

                if (Properties.Settings.Default.serverToggle)
                {
                    chkToggleServer.Tag = "active";
                    shouldServerOpen = true;
                    chkToggleServer.BackgroundImage = Resources.send;
                }
                else
                {
                    chkToggleServer.Tag = "inactive";
                    shouldServerOpen = false;
                    chkToggleServer.BackgroundImage = Resources.send_inactive;
                }

                if (Properties.Settings.Default.menuToggle)
                {
                    panelBottom.Visible = true;
                }
                else
                {
                    panelBottom.Visible = false;
                }

                if (Properties.Settings.Default.globalPath != "")
                {
                    currentDir = Properties.Settings.Default.globalPath;
                    btnShowPath.Text = currentDir;
                }

                string userFolder = Path.Combine(currentDir, "SPT",  "user");
                if (Directory.Exists(userFolder))
                {
                    if (doesFikaFolderExist && doesFikaCoreExist)
                    {
                        isFikaInstalled = true;

                        int widthspacer = 38;
                        Label lbl = new Label();
                        lbl.Text = "✔️ Connect to Fika Server";
                        lbl.AutoSize = false;
                        lbl.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
                        lbl.TextAlign = ContentAlignment.MiddleLeft;
                        lbl.Size = new Size(this.Size.Width - 4, widthspacer);
                        lbl.Location = new Point(0, 1);
                        lbl.Font = new Font("Bahnschrift Light", 12, FontStyle.Regular);
                        lbl.BackColor = listBackcolor;
                        lbl.ForeColor = Color.LightGray;
                        lbl.Margin = new Padding(1, 1, 1, 1);
                        lbl.Cursor = Cursors.Hand;
                        lbl.MouseEnter += new EventHandler(lbl_MouseEnter);
                        lbl.MouseLeave += new EventHandler(lbl_MouseLeave);
                        lbl.MouseDown += new MouseEventHandler(profile_Clicked);
                        lbl.MouseUp += new MouseEventHandler(lbl_MouseUp);
                        this.Controls.Add(lbl);

                        if (string.IsNullOrEmpty(Properties.Settings.Default.fikaAddress) &&
                            string.IsNullOrEmpty(Properties.Settings.Default.fikaAID))
                        {
                            AddressForm frm = new AddressForm();
                            frm.ShowDialog();
                        }

                        ipAddress = Properties.Settings.Default.fikaAddress;
                        currentAID = Properties.Settings.Default.fikaAID;
                        akiPort = 6969;
                    }
                    else
                    {
                        isFikaInstalled = false;

                        string profilesFolder = Path.Combine(userFolder, "profiles");
                        if (Directory.Exists(profilesFolder))
                            listProfiles(profilesFolder);
                        else
                            exitApp("Couldn\'t detect the `profiles` folder.\n" +
                                "Please place this app in your SPT folder (where SPT.Server.exe is located)");
                    }
                }
                else
                {
                    panelPath.Visible = true;
                    MessageBox.Show("No SPT files were detected. Please browse to a folder that contains an SPT installation (3.9.0 or higher)", Text, MessageBoxButtons.OK);
                }
            }
            else
            {
                exitApp("Couldn\'t detect the main folder.\n" +
                    "Please place this app in your SPT folder (where SPT.Server.exe is located)");
            }
        }

        private void clearTempFiles()
        {
            string userFolder = Path.Combine(currentDir, "SPT",  "user");
            bool userFolderExists = Directory.Exists(userFolder);
            if (userFolderExists)
            {
                string sptAppData = Path.Combine(userFolder, "sptappdata");
                bool sptAppDataExists = Directory.Exists(sptAppData);
                if (sptAppDataExists)
                {
                    // Variables
                    string appdata_files = Path.Combine(sptAppData, "files");
                    string appdata_files_achievement = Path.Combine(appdata_files, "achievement");
                    string appdata_files_quest = Path.Combine(appdata_files, "quest");
                    string appdata_files_quest_icon = Path.Combine(appdata_files_quest, "icon");
                    string appdata_files_trader = Path.Combine(appdata_files, "trader");
                    string appdata_files_trader_avatar = Path.Combine(appdata_files, "avatar");

                    string appdata_live = Path.Combine(sptAppData, "live");
                    string appdata_live_Clothing = Path.Combine(appdata_live, "Clothing");
                    string appdata_live_PlayerIcons = Path.Combine(appdata_live, "PlayerIcons");

                    if (Directory.Exists(appdata_files))
                    {
                        // SPT stuff
                        if (Directory.Exists(appdata_files_achievement))
                        {
                            foreach (string file in Directory.GetFiles(appdata_files_achievement, "*.*"))
                            {
                                try
                                {
                                    File.Delete(file);
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine($"Delete error: {ex.Message}");
                                }
                            }
                        }
                        else
                        {
                            Debug.WriteLine($"Directory not exist: {appdata_files_achievement}");
                        }
                        if (Directory.Exists(appdata_files_quest_icon))
                        {
                            foreach (string file in Directory.GetFiles(appdata_files_quest_icon, "*.*"))
                            {
                                try
                                {
                                    File.Delete(file);
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine($"Delete error: {ex.Message}");
                                }
                            }
                        }
                        else
                        {
                            Debug.WriteLine($"Directory not exist: {appdata_files_quest_icon}");
                        }
                        if (Directory.Exists(appdata_files_trader))
                        {
                            if (Directory.Exists(appdata_files_trader_avatar))
                            {
                                foreach (string file in Directory.GetFiles(appdata_files_trader_avatar, "*.*"))
                                {
                                    try
                                    {
                                        File.Delete(file);
                                    }
                                    catch (Exception ex)
                                    {
                                        Debug.WriteLine($"Delete error: {ex.Message}");
                                    }
                                }
                            }
                            else
                            {
                                Debug.WriteLine($"Directory not exist: {appdata_files_trader_avatar}");
                            }
                        }
                        else
                        {
                            Debug.WriteLine($"Directory not exist: {appdata_files_trader}");
                        }
                    }
                    else
                    {
                        Debug.WriteLine($"Directory not exist: {appdata_files_trader}");
                    }

                    if (Directory.Exists(appdata_live))
                    {
                        foreach (string file in Directory.GetFiles(appdata_live, "*.*"))
                        {
                            try
                            {
                                File.Delete(file);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"Delete error: {ex.Message}");
                            }
                        }
                    }
                    else
                    {
                        Debug.WriteLine($"Directory not exist: {appdata_live}");
                    }
                    if (Directory.Exists(appdata_live_Clothing))
                    {
                        foreach (string file in Directory.GetFiles(appdata_live_Clothing, "*.*"))
                        {
                            try
                            {
                                File.Delete(file);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"Delete error: {ex.Message}");
                            }
                        }
                    }
                    else
                    {
                        Debug.WriteLine($"Directory not exist: {appdata_live_Clothing}");
                    }
                    if (Directory.Exists(appdata_live_PlayerIcons))
                    {
                        foreach (string file in Directory.GetFiles(appdata_live_PlayerIcons, "*.*"))
                        {
                            try
                            {
                                File.Delete(file);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"Delete error: {ex.Message}");
                            }
                        }
                    }
                    else
                    {
                        Debug.WriteLine($"Directory not exist: {appdata_live_PlayerIcons}");
                    }
                }
            }
        }

        private void setCurrentDir(bool empty, string parent)
        {
            if (empty)
            {
                Properties.Settings.Default.globalPath = null;
                Properties.Settings.Default.Save();

                currentDir = Environment.CurrentDirectory;
                btnShowPath.Text = "No path set!";

                try
                {
                    this.Controls.OfType<Label>().ToList().ForEach(label => this.Controls.Remove(label));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Removal error: {ex.Message}");
                }

                try
                {
                    string userFolder = Path.Combine(currentDir, "SPT",  "user");
                    if (Directory.Exists(userFolder))
                    {
                        string profilesFolder = Path.Combine(userFolder, "profiles");
                        if (Directory.Exists(profilesFolder))
                            listProfiles(profilesFolder);
                        else
                            exitApp("Couldn\'t detect the `profiles` folder.\n" +
                                "Please place this app in your SPT folder (where SPT.Server.exe is located)");
                    }
                    else
                    {
                        panelPath.Visible = true;
                        MessageBox.Show("No SPT files were detected. Please browse to a folder that contains an SPT installation (3.9.0 or higher)", Text, MessageBoxButtons.OK);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Regeneration error: {ex.Message}");
                }
            }
            else
            {
                Properties.Settings.Default.globalPath = parent;
                Properties.Settings.Default.Save();

                btnShowPath.Text = parent;
                currentDir = parent;

                try
                {
                    this.Controls.OfType<Label>().ToList().ForEach(label => this.Controls.Remove(label));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Removal error: {ex.Message}");
                }

                try
                {
                    string userFolder = Path.Combine(currentDir, "SPT",  "user");
                    if (Directory.Exists(userFolder))
                    {
                        string profilesFolder = Path.Combine(userFolder, "profiles");
                        if (Directory.Exists(profilesFolder))
                            listProfiles(profilesFolder);
                        else
                            exitApp("Couldn\'t detect the `profiles` folder.\n" +
                                "Please place this app in your SPT folder (where SPT.Server.exe is located)");
                    }
                    else
                    {
                        panelPath.Visible = true;
                        MessageBox.Show("No SPT files were detected. Please browse to a folder that contains an SPT installation (3.9.0 or higher)", Text, MessageBoxButtons.OK);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Regeneration error: {ex.Message}");
                }
            }
        }

        private void exitApp(string message)
        {
            MessageBox.Show(message, this.Text, MessageBoxButtons.OK);
            Application.Exit();
        }



        // LISTING AND LISTING EVENTS

        private void listProfiles(string path)
        {
            int totalHeight = 0;

            string[] profiles = Directory.GetFiles(path, "*.json");
            int widthspacer = 38;

            for (int i = 0; i < profiles.Length; i++)
            {
                Label lbl = new Label();
                lbl.Text = displayProfileName(profiles[i]);
                lbl.AutoSize = false;
                lbl.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
                lbl.TextAlign = ContentAlignment.MiddleLeft;
                lbl.Size = new Size(this.Size.Width - 4, widthspacer);
                lbl.Location = new Point(0, 1 + (i * widthspacer));
                lbl.Font = new Font("Bahnschrift Light", 12, FontStyle.Regular);
                lbl.BackColor = listBackcolor;
                lbl.ForeColor = Color.LightGray;
                lbl.Margin = new Padding(1, 1, 1, 1);
                lbl.Cursor = Cursors.Hand;
                lbl.MouseEnter += new EventHandler(lbl_MouseEnter);
                lbl.MouseLeave += new EventHandler(lbl_MouseLeave);
                lbl.MouseDown += new MouseEventHandler(profile_Clicked);
                lbl.MouseUp += new MouseEventHandler(lbl_MouseUp);
                this.Controls.Add(lbl);
            }

            string portFile = Path.Combine(currentDir, "SPT",  "SPT_Data");
            portFile = Path.Combine(portFile, "database", "server.json");
            bool portExists = File.Exists(portFile);

            if (portExists)
            {
                string read = File.ReadAllText(portFile);
                var serializer = JsonUtil.initSerializer();
                var json = serializer.DeserializeObject(read);
                Dictionary<string, object> profile = (Dictionary<string, object>)json;

                ipAddress = (string)profile["ip"];
                akiPort = Convert.ToInt32(profile["port"]);
            }
            else
            {
                ipAddress = "127.0.0.1";
                akiPort = 6969;
            }

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Label lbl)
                {
                    totalHeight += lbl.Height;
                }
            }

            int padding = 55;
            MinimumSize = new Size(this.Size.Width, totalHeight + panelBottom.Size.Height + padding);
        }

        private void lbl_MouseEnter(object sender, EventArgs e)
        {
            System.Windows.Forms.Label label = (System.Windows.Forms.Label)sender;
            if (label.Text != "")
            {
                if (label.BackColor != listSelectedcolor)
                {
                    label.BackColor = listHovercolor;
                }
            }
        }

        private void lbl_MouseLeave(object sender, EventArgs e)
        {
            System.Windows.Forms.Label label = (System.Windows.Forms.Label)sender;
            if (label.Text != "")
            {
                if (label.BackColor != listSelectedcolor)
                {
                    label.BackColor = listBackcolor;
                }
            }
        }

        // When a profile is clicked
        private void profile_Clicked(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.Label label = (System.Windows.Forms.Label)sender;

            if (label.Text != "")
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (label.BackColor == listHovercolor)
                    {
                        if (label.Text.ToLower() == ">> invalid profile hierarchy")
                        {
                            MessageBox.Show("This profile seems to have an improper profile structure. Cancelling load.\n\n\n" +
                                            "The structure should be `<profileAID>.json` -> `characters` -> `pmc` -> `Info`", this.Text, MessageBoxButtons.OK);
                        }
                        else
                        {
                            currentAID = label.Text;
                            runServer();
                        }
                    }
                    else
                    {
                        label.BackColor = listHovercolor;
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    // to be continued
                }
            }
        }

        private void lbl_MouseUp(object sender, EventArgs e)
        {
            System.Windows.Forms.Label label = (System.Windows.Forms.Label)sender;
            if (label.Text != "")
            {
                label.BackColor = listHovercolor;
            }
        }

        // LISTING AND LISTING EVENTS



        private string displayProfileName(string path)
        {
            string result = "";
            bool profileExists = File.Exists(path);
            if (profileExists)
            {
                string read = File.ReadAllText(path);
                var serializer = JsonUtil.initSerializer();
                var json = serializer.DeserializeObject(read);

                Dictionary<string, object> profile = (Dictionary<string, object>)json;
                if (profile.ContainsKey("characters") && profile["characters"] is Dictionary<string, object> characters)
                {
                    if (characters.ContainsKey("pmc") && characters["pmc"] is Dictionary<string, object> PMC)
                    {
                        if (PMC.ContainsKey("Info") && PMC["Info"] is Dictionary<string, object> Info)
                        {
                            string Nickname = Info["Nickname"]?.ToString();
                            int Level = Convert.ToInt32(Info["Level"]);
                            string Side = Info["Side"]?.ToString();
                            string GameVersion = Info["GameVersion"]?.ToString();

                            if (Nickname != null && Level != null && Side != null && GameVersion != null)
                            {
                                switch (GameVersion.ToLower())
                                {
                                    case "standard":
                                        GameVersion = "Standard";
                                        break;
                                    case "left_behind":
                                        GameVersion = "Left Behind";
                                        break;
                                    case "prepare_for_escape":
                                        GameVersion = "Prepare for Escape";
                                        break;
                                    case "edge_of_darkness":
                                        GameVersion = "EOD";
                                        break;
                                    case "unheard_edition":
                                        GameVersion = "Unheard";
                                        break;
                                }
                                result = $"{Nickname}  [{Side.ToUpper()}] [Lvl {Level.ToString()}] [{GameVersion.ToString()}]";
                            }
                            else
                            {
                                result = ">> Invalid profile hierarchy";
                            }
                        }
                        else
                        {
                            result = ">> Invalid profile hierarchy";
                        }
                    }
                    else
                    {
                        result = ">> Invalid profile hierarchy";
                    }
                }
                else
                {
                    result = ">> Invalid profile hierarchy";
                }
            }

            return result;
        }

        private string displayAID(string nickname)
        {
            string result = "";
            string userFolder = Path.Combine(currentDir, "SPT",  "user");

            bool userFolderExists = Directory.Exists(userFolder);
            if (userFolderExists)
            {
                string profilesFolder = Path.Combine(userFolder, "profiles");

                bool profileFolderExists = Directory.Exists(profilesFolder);
                if (profileFolderExists)
                {
                    string[] profiles = Directory.GetFiles(profilesFolder);
                    for (int i = 0; i < profiles.Length; i++)
                    {
                        string read = File.ReadAllText(profiles[i]);
                        var serializer = JsonUtil.initSerializer();
                        var json = serializer.DeserializeObject(read);
                        Dictionary<string, object> profile = (Dictionary<string, object>)json;
                        Dictionary<string, object> characters = (Dictionary<string, object>)profile["characters"];
                        Dictionary<string, object> PMC = (Dictionary<string, object>)characters["pmc"];
                        Dictionary<string, object> Info = (Dictionary<string, object>)PMC["Info"];

                        string Nickname = Info["Nickname"].ToString();

                        if (Nickname == nickname)
                        {
                            result = Path.GetFileName(profiles[i]).Replace(".json", "");
                        }
                    }
                }
            }

            return result ?? "N/A";
        }

        private bool isPortListening(int port)
        {
            try
            {
                IPGlobalProperties globalProps = IPGlobalProperties.GetIPGlobalProperties();
                IPEndPoint[] tcpListeners = globalProps.GetActiveTcpListeners();

                bool isListening = tcpListeners.Any(endpoint =>
                    endpoint.Port == port &&
                    (
                        endpoint.Address.Equals(IPAddress.Loopback) ||
                        endpoint.Address.ToString() == "127.0.0.1"
                    )
                );

                if (isListening)
                {
                    if (CheckServerWorker != null)
                        CheckServerWorker.Dispose();

                    runTarkov();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool isServerOn()
        {
            Process[] servers = Process.GetProcessesByName("SPT.Server");
            return servers.Length > 0;
        }


        // BACKGROUND PROCESSES

        private void checkWorker()
        {
            CheckServerWorker = new BackgroundWorker();
            if (CheckServerWorker != null)
                CheckServerWorker.Dispose();

            CheckServerWorker.WorkerSupportsCancellation = true;
            CheckServerWorker.WorkerReportsProgress = false;

            CheckServerWorker.DoWork += CheckServerWorker_DoWork;
            CheckServerWorker.RunWorkerCompleted += CheckServerWorker_RunWorkerCompleted;

            try
            {
                CheckServerWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
        }

        private void CheckServerWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int port = akiPort; // the port to check
            int timeout = 0;
            switch (timeoutLimit.Value)
            {
                case 0:
                    timeout = 30000;
                    break;
                case 1:
                    timeout = 60000;
                    break;
                case 2:
                    timeout = 120000;
                    break;
                case 3:
                    timeout = 180000;
                    break;
                case 4:
                    timeout = 240000;
                    break;
                case 5:
                    timeout = 300000;
                    break;
                case 6:
                    timeout = 360000;
                    break;
                case 7:
                    timeout = 420000;
                    break;
                case 8:
                    timeout = 480000;
                    break;
                case 9:
                    timeout = 540000;
                    break;
                case 10:
                    timeout = 600000;
                    break;
            }

            int delay = 1000; // the delay between port checks in milliseconds
            int elapsed = 0; // the time elapsed since starting to check the port

            while (!isPortListening(akiPort))
            {
                if (elapsed >= timeout)
                {
                    e.Cancel = true;
                    MessageBox.Show("We could not detect SPT's active port after the elapsed time. Please up your timeout limit or try again.\n" +
                                "\n" +
                                "Max duration reached, launching SPT.");
                    this.Show();
                    return;
                }

                Thread.Sleep(delay); // wait before checking again
                elapsed += delay;
            }
        }

        private void CheckServerWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Debug.WriteLine("Port could not be opened within the specified time period.");
            }
            else if (e.Error != null)
            {
                Debug.WriteLine("An error occurred while checking the port: " + e.Error.Message);
            }
        }

        private void TarkovEndDetector_DoWork(object sender, DoWorkEventArgs e)
        {
            string processName = "EscapeFromTarkov";
            while (true)
            {
                Process[] processes = Process.GetProcessesByName(processName);
                if (processes.Length == 0)
                {
                    killProcesses(true);

                    if (TarkovEndDetector != null)
                        TarkovEndDetector.Dispose();

                    break;
                }
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void TarkovEndDetector_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (TarkovEndDetector != null)
                TarkovEndDetector.Dispose();
        }

        private void server_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            string res = e.Data;
            if (!string.IsNullOrEmpty(res))
            {
                res = Regex.Replace(res, @"\[[0-1];[0-9][a-z]|\[[0-9][0-9][a-z]|\[[0-9][a-z]|\[[0-9][A-Z]", String.Empty);
            }

            serverOut.AppendLine(res);
            Debug.WriteLine(serverOut);
        }

        private void server_Exited(object sender, EventArgs e)
        {
        }

        private void isServerOpen_DoWork(object sender, DoWorkEventArgs e)
        {
            string processName = "SPT.Server";
            while (true)
            {
                Process[] processes = Process.GetProcessesByName(processName);
                if (processes.Length == 0)
                {
                    killProcesses(true);

                    if (isServerOpen != null)
                        isServerOpen.Dispose();

                    if (TarkovEndDetector != null)
                        TarkovEndDetector.Dispose();

                    break;
                }
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void isServerOpen_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (isServerOpen != null)
                isServerOpen.Dispose();

            if (TarkovEndDetector != null)
                TarkovEndDetector.Dispose();
        }

        // BACKGROUND PROCESSES



        private void killProcesses(bool isExit)
        {
            server = null;
            launcher = null;

            if (serverOut != null)
                serverOut.Clear();

            string akiServerProcess = "SPT.Server";
            string akiLauncherProcess = "SPT.Launcher";
            string eftProcess = "EscapeFromTarkov";
            bool akiServerTerminated = false;
            bool akiLauncherTerminated = false;
            bool eftTerminated = false;

            try
            {
                Process[] procs = Process.GetProcessesByName(akiServerProcess);
                if (procs != null && procs.Length > 0)
                {
                    foreach (Process aki in procs)
                    {
                        if (!aki.HasExited)
                        {
                            if (!aki.CloseMainWindow())
                            {
                                aki.Kill();
                                aki.WaitForExit();
                            }
                            else
                            {
                                aki.WaitForExit();
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine($"TERMINATION FAILURE OF SPT SERVER (IGNORE): {err.ToString()}");
            }

            Task.Delay(200);

            try
            {
                Process[] procs = Process.GetProcessesByName(akiLauncherProcess);
                if (procs != null && procs.Length > 0)
                {
                    foreach (Process aki in procs)
                    {
                        if (!aki.HasExited)
                        {
                            if (!aki.CloseMainWindow())
                            {
                                aki.Kill();
                                aki.WaitForExit();
                            }
                            else
                            {
                                aki.WaitForExit();
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine($"TERMINATION FAILURE OF SPT LAUNCHER (IGNORE): {err.ToString()}");
            }

            Task.Delay(200);

            try
            {
                Process[] procs = Process.GetProcessesByName(eftProcess);
                if (procs != null && procs.Length > 0)
                {
                    foreach (Process aki in procs)
                    {
                        if (!aki.HasExited)
                        {
                            if (!aki.CloseMainWindow())
                            {
                                aki.Kill();
                                aki.WaitForExit();
                            }
                            else
                            {
                                aki.WaitForExit();
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine($"TERMINATION FAILURE OF SPT LAUNCHER (IGNORE): {err.ToString()}");
            }

            Task.Delay(500);

            try
            {
                Process[] procs1 = Process.GetProcessesByName(akiServerProcess);
                if (procs1 != null && procs1.Length > 0)
                {
                }
                else
                {
                    akiServerTerminated = true;
                }

                Process[] procs2 = Process.GetProcessesByName(akiLauncherProcess);
                if (procs2 != null && procs2.Length > 0)
                {
                }
                else
                {
                    akiLauncherTerminated = true;
                }

                Process[] procs3 = Process.GetProcessesByName(eftProcess);
                if (procs3 != null && procs3.Length > 0)
                {
                }
                else
                {
                    eftTerminated = true;
                }

                if (akiServerTerminated && akiLauncherTerminated && eftTerminated)
                {
                    try
                    {
                        if (CheckServerWorker != null)
                            CheckServerWorker.Dispose();

                        if (TarkovEndDetector != null)
                            TarkovEndDetector.Dispose();

                        if (TarkovProcessDetector != null)
                            TarkovProcessDetector.Dispose();

                        if (isExit)
                            Application.Exit();
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine($"DISPOSE FAILURE (IGNORE): {err.ToString()}");

                    }
                }
                else
                {
                    MessageBox.Show("Failed to end one or more processs (SPT.Server, SPT.Launcher, Escape From Tarkov), cancelling exit.", this.Text, MessageBoxButtons.OK);
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine($"TERMINATION FAILURE (IGNORE): {err.ToString()}");
            }
        }

        private void runServer()
        {
            this.Hide();

            if (!string.IsNullOrEmpty(Properties.Settings.Default.fikaAddress) && isFikaInstalled)
            {
                runTarkov();
            }
            else
            {
                bool isRunning = isServerOn();
                Task.Delay(500);

                if (isRunning)
                {
                    checkWorker();
                }
                else
                {
                    Task.Delay(500);

                    Directory.SetCurrentDirectory(Path.Combine(currentDir, "SPT"));
                    Process server = new Process();

                    server.StartInfo.WorkingDirectory = Path.Combine(currentDir, "SPT");
                    server.StartInfo.FileName = "SPT.Server.exe";

                    switch (shouldServerOpen)
                    {
                        case true:
                            server.StartInfo.CreateNoWindow = false;

                            try
                            {
                                server.Start();
                                checkWorker();
                            }
                            catch (Exception err)
                            {
                                Debug.WriteLine($"ERROR: {err}");
                                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                            }

                            break;
                        case false:
                            server.StartInfo.CreateNoWindow = false;
                            server.StartInfo.UseShellExecute = true;

                            // server.StartInfo.RedirectStandardOutput = true;
                            // server.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                            // SetConsoleMode in the SPT server; TODO

                            server.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            server.OutputDataReceived += server_OutputDataReceived;
                            server.Exited += server_Exited;

                            try
                            {
                                serverOut = new StringBuilder();
                                server.Start();
                                // server.BeginOutputReadLine();
                                checkWorker();
                            }
                            catch (Exception err)
                            {
                                Debug.WriteLine($"ERROR: {err}");
                                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                            }

                            break;
                    }

                    isServerOpen = new BackgroundWorker();
                    isServerOpen.DoWork += isServerOpen_DoWork;
                    isServerOpen.RunWorkerCompleted += isServerOpen_RunWorkerCompleted;
                    isServerOpen.RunWorkerAsync();

                    Directory.SetCurrentDirectory(Environment.CurrentDirectory);
                }
            }
        }

        private void runTarkov()
        {
            string launcherProcess = "EscapeFromTarkov";
            Process[] launchers = Process.GetProcessesByName(launcherProcess);

            try
            {
                Process[] launcherprocs = Process.GetProcessesByName(launcherProcess);
                if (launcherprocs != null && launcherprocs.Length > 1)
                {
                    foreach (Process eft in launcherprocs)
                    {
                        if (!eft.HasExited)
                        {
                            if (!eft.CloseMainWindow())
                            {
                                eft.Kill();
                                eft.WaitForExit();
                            }
                            else
                            {
                                eft.WaitForExit();
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine($"TERMINATION FAILURE LEFTOVERS SPT LAUNCHER (IGNORE): {err.ToString()}");
            }

            Task.Delay(5000);

            ProcessStartInfo tarkovInfo = new ProcessStartInfo();

            string name = string.Empty;
            string AID = string.Empty;

            if (isFikaInstalled)
            {
                AID = Properties.Settings.Default.fikaAID;
            }
            else
            {
                name = Regex.Match(currentAID, @"^([\w\s]+)").Groups[1].Value.Trim();
                AID = displayAID(name) ?? "N/A";
            }

            if (AID == "N/A")
            {
                this.Show();
                MessageBox.Show($"Couldn\'t find the profile, it appears there is no profile with nickname `{currentAID}`\n\nPlease select a different profile and try again.", this.Text, MessageBoxButtons.OK);
            }
            else
            {
                if (isFikaInstalled)
                {
                    ipAddress = Properties.Settings.Default.fikaAddress;
                }

                tarkovInfo.FileName = Path.Combine(currentDir, "EscapeFromTarkov");
                tarkovInfo.Arguments = $"-force-gfx-jobs native -token={AID} -config={{'BackendUrl':'https://{ipAddress}:{akiPort}','Version':'live','MatchingVersion':'live'}}";

                Process gameProcess = new Process();
                gameProcess.StartInfo = tarkovInfo;

                try
                {
                    Debug.WriteLine(tarkovInfo.Arguments.ToString());
                    gameProcess.Start();

                    TarkovEndDetector = new BackgroundWorker();
                    TarkovEndDetector.DoWork += TarkovEndDetector_DoWork;
                    TarkovEndDetector.RunWorkerCompleted += TarkovEndDetector_RunWorkerCompleted;
                    TarkovEndDetector.RunWorkerAsync();
                }
                catch (Exception err)
                {
                    Debug.WriteLine($"ERROR: {err.ToString()}");
                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);

                    killProcesses(false);
                    this.Show();
                }
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            killProcesses(true);
        }

        private void btnViewPlaytime_Click(object sender, EventArgs e)
        {
            FAQ faq_w = new FAQ();
            faq_w.ShowDialog();

            /*
            bool playtimeExists = File.Exists(playtimeFile);
            if (playtimeExists)
            {
                FileInfo info = new FileInfo(playtimeFile);
                if (info.Length == 0)
                {
                    MessageBox.Show("No playtime has been recorded. Please start Escape From Tarkov with a profile and play for a bit, then try again.", this.Text, MessageBoxButtons.OK);
                }
                else
                {
                    string playtimeString = File.ReadAllText(playtimeFile);
                    if (int.TryParse(playtimeString, out int playtimeInt))
                    {
                        TimeSpan playtime = TimeSpan.FromSeconds(playtimeInt);
                        string formattedPlaytime = "";

                        if (playtime.TotalDays >= 1)
                        {
                            if (playtime.TotalHours >= 1 && playtime.Minutes == 0)
                            {
                                int days = (int)playtime.TotalDays;
                                int hours = playtime.Hours;
                                formattedPlaytime = $"{days} days and {hours} hours";
                            }
                            else if (playtime.TotalHours >= 1)
                            {
                                int days = (int)playtime.TotalDays;
                                int hours = playtime.Hours;
                                int minutes = playtime.Minutes;
                                formattedPlaytime = $"{days} days, {hours} hours and {minutes} minutes";
                            }
                            else
                            {
                                int days = (int)playtime.TotalDays;
                                int minutes = playtime.Minutes;
                                formattedPlaytime = $"{days} days and {minutes} hours";
                            }
                        }
                        else if (playtime.TotalHours >= 1)
                        {
                            if (playtime.Minutes == 0)
                            {
                                int hours = playtime.Hours;
                                formattedPlaytime = $"{hours} hours";
                            }
                            else
                            {
                                int hours = playtime.Hours;
                                int minutes = playtime.Minutes;
                                formattedPlaytime = $"{hours} hours and {minutes} minutes";
                            }
                        }
                        else
                        {
                            int minutes = playtime.Minutes;
                            formattedPlaytime = $"{minutes} minutes";
                        }

                        string formattedHour = string.Format("{0:#,##0} hours", playtime.TotalHours);
                        string quickLauncherFolder = Path.GetFileName(currentDir);
                        MessageBox.Show($"You have a total playtime of {formattedPlaytime} in:\n{quickLauncherFolder}\n\nHour count: {formattedHour}", this.Text, MessageBoxButtons.OK);
                    }
                    else
                    {
                        File.Create(playtimeFile).Close();
                    }
                }
            }
            */
        }

        private void chkToggleServer_Click(object sender, EventArgs e)
        {
            if (chkToggleServer.Tag.ToString().ToLower() == "inactive")
            {
                chkToggleServer.Tag = "active";
                shouldServerOpen = true;
                chkToggleServer.BackgroundImage = Resources.send;

                Properties.Settings.Default.serverToggle = true;
            }
            else
            {
                chkToggleServer.Tag = "inactive";
                shouldServerOpen = false;
                chkToggleServer.BackgroundImage = Resources.send_inactive;

                Properties.Settings.Default.serverToggle = false;
            }

            Properties.Settings.Default.Save();
        }

        private void chkToggleMenu_Click(object sender, EventArgs e)
        {
            if (panelBottom.Visible)
            {
                Properties.Settings.Default.menuToggle = false;
                panelBottom.Visible = false;
            }
            else
            {
                Properties.Settings.Default.menuToggle = true;
                panelBottom.Visible = true;
            }

            Properties.Settings.Default.Save();
        }

        private void btnClearPath_Click(object sender, EventArgs e)
        {
            setCurrentDir(true, null);
        }

        private void btnShowPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog cf = new OpenFileDialog();
            cf.Title = "Select the SPT.Server / SPT.Launcher / EscapeFromTarkov file in the folder you want to set";
            cf.InitialDirectory = currentDir;
            cf.Filter = "Executable files (*.exe)|*.exe";
            cf.FilterIndex = 1;
            cf.Multiselect = false;
            cf.RestoreDirectory = true;
            cf.CheckFileExists = true;
            cf.CheckPathExists = true;

            if (cf.ShowDialog() == DialogResult.OK)
            {
                string selectedPath = cf.FileName;
                string trimmedSelectedPath = Path.GetFileNameWithoutExtension(selectedPath);

                if (trimmedSelectedPath.ToLower() == "spt.server" ||
                    trimmedSelectedPath.ToLower() == "spt.launcher" ||
                    trimmedSelectedPath.ToLower() == "escapefromtarkov")
                {
                    string selectedParent = Directory.GetParent(selectedPath).ToString();
                    if (Directory.Exists(selectedParent))
                    {
                        string SPTData = Path.Combine(selectedParent, "SPT_Data");
                        bool dataExists = Directory.Exists(SPTData);
                        if (dataExists)
                        {
                            setCurrentDir(false, selectedParent);
                        }
                        else
                        {
                            MessageBox.Show($"QuickLauncher only supports SPT 3.9.0 and up due to compatibility reasons."
                                + Environment.NewLine + Environment.NewLine +
                                "Please select another SPT installation that is version 3.9.0 or higher.", Text, MessageBoxButtons.OK);
                        }
                    }
                }
            }
        }

        private void chkTogglePath_Click(object sender, EventArgs e)
        {
            if (panelPath.Visible)
                panelPath.Visible = false;
            else
                panelPath.Visible = true;
        }

        private void btnClearTempFiles_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear the temp files?", Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                clearTempFiles();
            }
        }

        private void chkOpenAddressPrompt_Click(object sender, EventArgs e)
        {
            AddressForm frm = new AddressForm();
            frm.ShowDialog();
        }
    }
}
