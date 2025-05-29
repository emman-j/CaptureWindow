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
    public partial class Main : Form, INotifyPropertyChanged
    {
        private DockingMode _dockingMode;
        private readonly Client client;

        public DockingMode DockingMode
        {
            get => _dockingMode;
            set 
            {
                if (_dockingMode != value)
                { 
                    _dockingMode = value;
                    NotifyPropertyChanged();
                    DockingChanged(_dockingMode);
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        public Main()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.Text = string.Empty;
            this.ControlBox = false;
            client = new Client(tabControl1);
        }

        private void DockingChanged(DockingMode dockingMode)
        {
            void Enable(Control control)
            {
                control.Visible = true;
                control.Dock = DockStyle.Fill;
            }
            void Disable(Control control)
            {
                control.Dock = DockStyle.None;
                control.Visible = false;
            }

            if (dockingMode == DockingMode.Tab)
            {
                Enable(tabControl1);
            }
            else if (dockingMode == DockingMode.Window)
            {
                Disable(tabControl1);
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (components != null)
            {
                components.Dispose();
            }
            client.Close();
        }
        private void Main_SizeChanged(object sender, EventArgs e)
        {
            client.FormResized();
        }

        private void TitleBarPanel_MouseDown(object sender, MouseEventArgs e)
        {
            client.TitleBarMouseDown((Control)sender);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client.LauchAndDock();
        }
        private void dockOpenAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client.SelectOpenApp();
        }
        private void undockAppToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            client.UndockApp();
        }
        private void undockAllAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client.UndockAllApp();
        }
        private void chToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client.ChangeTabName();
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
                DockingMode = DockingMode.Tab;
            else if (clickedItem == WindowsModeToolStripMenuItem)
                DockingMode = DockingMode.Window;

           clickedItem.Checked = true;
        }
    }
}
