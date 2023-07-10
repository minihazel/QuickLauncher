using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml.Linq;

namespace QuickLauncher
{
    public partial class mainForm : Form
    {
        public Color listBackcolor = Color.FromArgb(255, 28, 28, 28);
        public Color listSelectedcolor = Color.FromArgb(255, 40, 40, 40);
        public Color listHovercolor = Color.FromArgb(255, 35, 35, 35);

        public string currentDir = $"E:\\SPT Iterations\\SPT-AKI 3.5.8";
        public string ipAddress;
        public int akiPort;

        public Process server;
        public Process launcher;
        private List<string> globalProcesses;

        // background working
        BackgroundWorker CheckServerWorker;
        public BackgroundWorker TarkovProcessDetector;
        public BackgroundWorker TarkovEndDetector;
        public StringBuilder akiServerOutputter;

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
            if (Directory.Exists(currentDir))
            {
                lblLimit1.Select();

                string userFolder = Path.Combine(currentDir, "user");
                if (Directory.Exists(userFolder))
                {
                    string profilesFolder = Path.Combine(userFolder, "profiles");
                    if (Directory.Exists(profilesFolder))
                    {
                        listProfiles(profilesFolder);
                    }
                    else
                    {
                        exitApp("Couldn\'t detect the `profiles` folder.\n" +
                            "Please place this app in your SPT-AKI folder (where Aki.Server.exe is located)");
                    }
                }
                else
                {
                    exitApp("Couldn\'t detect the `user` folder.\n" +
                        "Please place this app in your SPT-AKI folder (where Aki.Server.exe is located)");
                }
            }
            else
            {
                exitApp("Couldn\'t detect the main folder folder.\n" +
                    "Please place this app in your SPT-AKI folder (where Aki.Server.exe is located)");
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
                lbl.MouseDown += new MouseEventHandler(lbl_MouseDown);
                lbl.MouseDoubleClick += new MouseEventHandler(lbl_MouseDoubleClick);
                lbl.MouseUp += new MouseEventHandler(lbl_MouseUp);
                this.Controls.Add(lbl);
            }

            string portFile = Path.Combine(currentDir, "Aki_Data");
            portFile = Path.Combine(portFile, "Server");
            portFile = Path.Combine(portFile, "database");
            portFile = Path.Combine(portFile, "server.json");
            bool portExists = File.Exists(portFile);

            if (portExists)
            {
                string read = File.ReadAllText(portFile);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var json = serializer.DeserializeObject(read);
                Dictionary<string, object> profile = (Dictionary<string, object>)json;

                string ip = profile["ip"].ToString();

                ipAddress = ip;
                akiPort = Convert.ToInt32(profile["port"]);
            }
            else
            {
                akiPort = 6969;
            }
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

        private void lbl_MouseDown(object sender, EventArgs e)
        {
            System.Windows.Forms.Label label = (System.Windows.Forms.Label)sender;

            if (label.Text != "")
            {
                if (label.BackColor == listHovercolor)
                {
                    label.BackColor = listSelectedcolor;
                    checkWorker();
                }
                else
                {
                    label.BackColor = listHovercolor;
                }
            }
        }

        private void lbl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.Label label = (System.Windows.Forms.Label)sender;

            if (label.Text != "")
            {
                Color activeColor = Color.DodgerBlue;
                Button associatedBtn = null;

                foreach (Control component in this.Controls)
                {
                    if (component is Button btn && btn.FlatAppearance.BorderColor == Color.DodgerBlue)
                    {
                        associatedBtn = btn;
                        break;
                    }
                }

                if (associatedBtn != null)
                {
                    label.BackColor = listSelectedcolor;
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
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var json = serializer.DeserializeObject(read);
                Dictionary<string, object> profile = (Dictionary<string, object>)json;
                Dictionary<string, object> characters = (Dictionary<string, object>)profile["characters"];
                Dictionary<string, object> PMC = (Dictionary<string, object>)characters["pmc"];
                Dictionary<string, object> Info = (Dictionary<string, object>)PMC["Info"];

                string Nickname = Info["Nickname"].ToString();
                int Level = Convert.ToInt32(Info["Level"]);
                string Side = Info["Side"].ToString();
                string GameVersion = Info["GameVersion"].ToString();

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
                }
                result = $"{Nickname}  [{Side.ToUpper()}] [Lvl {Level.ToString()}] [{GameVersion.ToString()}]";
            }

            return result;
        }

        private bool CheckPort(int port)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    if (CheckServerWorker != null)
                        CheckServerWorker.Dispose();

                    client.Connect(ipAddress, port);

                    runTarkov();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
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

            while (!CheckPort(port))
            {
                if (elapsed >= timeout)
                {
                    // port was not opened within the timeout period, so cancel the operation
                    e.Cancel = true;
                    if (CheckServerWorker != null)
                        CheckServerWorker.Dispose();

                    MessageBox.Show("We could not detect the Aki Launcher after 5 minutes.\n" +
                              "\n" +
                              "Max duration reached, launching SPT-AKI.");

                    runTarkov();
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
                    killProcesses();

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

        // BACKGROUND PROCESSES



        private void killProcesses()
        {
            string akiServerProcess = "Aki.Server";
            string akiLauncherProcess = "Aki.Launcher";
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
                Debug.WriteLine($"TERMINATION FAILURE OF AKI SERVER (IGNORE): {err.ToString()}");
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
                Debug.WriteLine($"TERMINATION FAILURE OF AKI LAUNCHER (IGNORE): {err.ToString()}");
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
                Debug.WriteLine($"TERMINATION FAILURE OF AKI LAUNCHER (IGNORE): {err.ToString()}");
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

                        Application.Exit();
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine($"DISPOSE FAILURE (IGNORE): {err.ToString()}");

                    }
                }
                else
                {
                    MessageBox.Show("Failed to end one or more processs (Aki.Server, Aki.Launcher, Escape From Tarkov), cancelling exit.", this.Text, MessageBoxButtons.OK);
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine($"TERMINATION FAILURE (IGNORE): {err.ToString()}");
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
                Debug.WriteLine($"TERMINATION FAILURE LEFTOVERS AKI LAUNCHER (IGNORE): {err.ToString()}");
            }

            Task.Delay(5000);

            TarkovEndDetector = new BackgroundWorker();
            TarkovEndDetector.DoWork += TarkovEndDetector_DoWork;
            TarkovEndDetector.RunWorkerCompleted += TarkovEndDetector_RunWorkerCompleted;
            TarkovEndDetector.RunWorkerAsync();
        }
    }
}
