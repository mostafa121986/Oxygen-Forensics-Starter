using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;



namespace Oxygen_Starter
{
    public partial class Form1 : Form
    {

        const int closeTimeout = 120000;  // 6 sek
        int currentTimeoutTimer;

        const string regOxyKeyName = "HKEY_CURRENT_USER\\Software\\Oxygen\\Oxy3\\";
        string oxyInstallPath;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // load paths from registry
            oxyInstallPath = (string)Registry.GetValue(regOxyKeyName + "\\OxyDetective", "InstallPath", "");
            if (oxyInstallPath == "") MessageBox.Show("Error: Cant read installpath from registry.");

            currentTimeoutTimer = closeTimeout;
        }

        private void pictureBox_ofd_DoubleClick(object sender, EventArgs e)
        {
            StartOxy(oxyInstallPath + "\\OFD12\\OxyDetective.exe",checkBox_logging.Checked);
        }

        private void pictureBox_extractor_Click(object sender, EventArgs e)
        {
            StartOxy(oxyInstallPath + "\\OFD12\\Extractor\\OxyExtractor.exe",checkBox_logging.Checked);
        }

        private void pictureBox_sqlite_Click(object sender, EventArgs e)
        {
            StartOxy(oxyInstallPath + "\\OxySQLiteViewer64.exe", checkBox_logging.Checked);
        }

        private void pictureBox_maps_Click(object sender, EventArgs e)
        {
            StartOxy(oxyInstallPath + "\\Maps\\OxyMaps64.exe", checkBox_logging.Checked);
        }
        private void pictureBox_cloud_Click(object sender, EventArgs e)
        {
            StartOxy(oxyInstallPath + "\\OFEC\\OxyCloudExtractor.exe", checkBox_logging.Checked);
        }

        private void pictureBox_cde_Click(object sender, EventArgs e)
        {
            StartOxy(oxyInstallPath + "\\CDR\\OxyCDRExpert64.exe", checkBox_logging.Checked);
        }

        private void pictureBox_keyscout_Click(object sender, EventArgs e)
        {
            StartOxy(oxyInstallPath + "\\KeyScout\\OxyKeyScout.Windows.exe", checkBox_logging.Checked);
        }

        private void pictureBox_plist_Click(object sender, EventArgs e)
        {
            StartOxy(oxyInstallPath + "\\OxyPlistViewer.exe", checkBox_logging.Checked);
        }


        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://oxygen-support.zendesk.com");
        }


        private void linkLabel_about_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.d-forensik.de");
        }

        private void linkLabel_opendatabase_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string databaseFolder;
            databaseFolder = (string)Registry.GetValue(regOxyKeyName + "CommonSettings\\", "DatabaseFolder", "");
            Process.Start(@databaseFolder);
        }

        private void linkLabel_openlog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string userFolder;
            userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            Process.Start(@userFolder + "\\Oxygen Forensic\\Logs");
        }


        private void StartOxy(string filePathAndName, Boolean logEnabled)
        {
            
            if (File.Exists(filePathAndName))
            {
                Process P = new Process();

                P.StartInfo.FileName = filePathAndName;

                if (logEnabled)
                {
                    P.StartInfo.Arguments = "-log";
                }

                P.Start();
            }
            else { MessageBox.Show("Error: File not found. " + filePathAndName); }
            
            

        }

      

        private void timerCountTimer_Tick(object sender, EventArgs e)
        { 
            currentTimeoutTimer = currentTimeoutTimer-1000;

            labelTimer.Text = "This tool is freeware by d-forensik.de and closed automatically in " + (currentTimeoutTimer/1000).ToString() + " sec.";

            if (currentTimeoutTimer < 999) Application.Exit();
        }

        private void labelTimer_Click(object sender, EventArgs e)
        {
            if(timerCountTimer.Enabled)
            {
                timerCountTimer.Stop();
            } else
            {
                timerCountTimer.Start();
            }
            
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.oxygen-forensic.com/download/whatsnew/OFD/WhatsNew.html");
        }
    }
}
