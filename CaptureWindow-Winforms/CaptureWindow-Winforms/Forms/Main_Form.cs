using CaptureWindow_Winforms.Library;
using CaptureWindow_Winforms.Library.Common.Enums;
using CaptureWindow_Winforms.Library.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaptureWindow_Winforms.Forms
{
    public partial class Main : Form
    {
        private readonly Client client;

        public Main()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.Text = string.Empty;
            this.ControlBox = false;

            client = new Client
            (
                tabControl: tabControl1, 
                panelControl: panel1, 
                windowPanel: WindowViewPanel, 
                flowLayoutPanel: flowLayoutPanel1
            );;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (components != null)
            {
                components.Dispose();
            }
            client?.Close();
        }
        private void Main_SizeChanged(object sender, EventArgs e)
        {
            client?.FormResized();
        }

        private void TitleBarPanel_MouseDown(object sender, MouseEventArgs e)
        {
            client?.TitleBarMouseDown((Control)sender);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client?.LauchAndDock();
        }
        private void dockOpenAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client?.SelectOpenApp();
        }
        private void undockAppToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            client?.UndockApp();
        }
        private void undockAllAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client?.UndockAllApp();
        }
        private void chToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client?.ChangeTabName();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DockingModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;

            // Get the parent menu of the clicked item
            ToolStripItemCollection menuItems = clickedItem.GetCurrentParent().Items;

            // Uncheck all menu items in the same group
            foreach (ToolStripItem item in menuItems)
            {
                if (item is ToolStripMenuItem menuItem)
                {
                    menuItem.Checked = false;
                }
            }

            if (clickedItem == TabsModeToolStripMenuItem)
                client.DockingMode = DockingMode.Tab;
            else if (clickedItem == WindowsModeToolStripMenuItem)
                client.DockingMode = DockingMode.Window;

           clickedItem.Checked = true;
        }
    }
}
