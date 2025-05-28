using CaptureWindow_Winforms.Library;
using CaptureWindow_Winforms.Library.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.Text = string.Empty;
            this.ControlBox = false;
            client = new Client(tabControl1);
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

        }
        private void dockOpenAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client.SelectOpenApp();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
