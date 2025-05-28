using CaptureWindow.Classes;
using CaptureWindow.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaptureWindow
{
    public partial class Form_Main : Form
    {
        // This is my test commit

        public TabPage selectedTab;
        TabControlManager tabControlManager;
        public Form_Main()
        {
            InitializeComponent();
            selectedTab = tabPage1;
            tabControlManager = new TabControlManager(tabControl1);

            //this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.Text = string.Empty;
            this.ControlBox = false;

        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            WindowAppManager.ResizeAndDockApp(selectedTab);
        } 
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (components != null)
            { 
                components.Dispose();
            }
            WindowAppManager.CleanUp();
            WindowAppManager.CloseAllEmbeddedApps();
            Application.Exit();

        } 
        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            if (sender is TabControl tabControl)
            {
                selectedTab = tabControl.SelectedTab;
                if (selectedTab != null)
                {
                    WindowAppManager.ResizeAndDockApp(selectedTab); 
                }
                bool isLastTab = tabControl.SelectedIndex == tabControl.TabCount - 1;
                if (isLastTab)
                {
                    tabControlManager.AddNewTab("+");
                    WindowAppManager.LaunchAndDockApp(selectedTab);
                }
            }
        } 
        private void tabControl1_DoubleClick(object sender, EventArgs e)
        {
            WindowAppManager.CloseTabHandle(selectedTab);
            tabControl1.TabPages.Remove(selectedTab);

            TabPage secondToLastTab = tabControlManager.GetSecondToLastTab();
            tabControl1.SelectedTab = secondToLastTab; 
        }  
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (components != null)
            {
                components.Dispose();
            }
            WindowAppManager.CleanUp();
            WindowAppManager.CloseAllEmbeddedApps();
            this.Close();
        } 
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            WindowAppManager.LaunchAndDockApp(selectedTab);
        } 
        private void selectOpenAppToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            using (Form_OpenAppSelection selectionForm = new Form_OpenAppSelection())
            {
                if (selectionForm.ShowDialog() == DialogResult.OK)
                {   
                    IntPtr selectedAppHandle = selectionForm.SelectedAppHandle;
                    string selectedAppTitle = selectionForm.SelectedAppTitle;
                    if (selectedAppHandle != IntPtr.Zero && tabControl1.SelectedTab != null)
                    {
                        WindowAppManager.EmbedSelectedApp(selectedAppHandle, tabControl1.SelectedTab);

                    }
                    tabControl1.SelectedTab.Text = selectedAppTitle;
                }
            }
        } 
        private void undockAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowAppManager.UndockApp(selectedTab);
        } 
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            Form parentForm = this.FindForm();
            if (parentForm != null)
            {
                WindowAppManager.ReleaseCapture();
                WindowAppManager.SendMessage(parentForm.Handle, 0x112, 0xf012, 0);
            }
        }

        bool isbuttonpressed = false;
        private void label1_MouseUpDown(object sender, MouseEventArgs e)
        {
            if (sender is Label label)
            {
                if (isbuttonpressed == false) 
                { 
                    label.ForeColor = SystemColors.HotTrack; 
                    isbuttonpressed = true; 
                }
                else
                {
                    label.ForeColor = System.Drawing.Color.FromArgb(209, 209, 209); 
                    isbuttonpressed = false;
                }
            }
        } 
        private void undockAllAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowAppManager.UndockAllApp();
            // Check if there are more than one tabs
            if (tabControl1.TabPages.Count > 1)
            {
                // Remove tabs starting from the second-last to the first
                for (int i = tabControl1.TabPages.Count - 2; i >= 0; i--)
                {
                    tabControl1.TabPages.RemoveAt(i);
                }
            }
        }
        private void chToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            if (tabControl1.SelectedTab != null)
            {
                using (Form_ChangeTabName changeTabNameForm = new Form_ChangeTabName())
                {

                    changeTabNameForm.TabName = tabControl1.SelectedTab.Text;

                    if (changeTabNameForm.ShowDialog() == DialogResult.OK)
                    {
                        tabControl1.SelectedTab.Text = changeTabNameForm.TabName;
                    }
                }
            }
            else
            {
                MessageBox.Show("No tab selected to rename!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } 
        private void openAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowAppManager.LaunchAndDockApp(selectedTab);
        } 
        private void Form_Main_Load(object sender, EventArgs e)
        {  

        }
    }
}

