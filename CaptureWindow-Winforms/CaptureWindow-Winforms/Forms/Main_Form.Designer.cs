namespace CaptureWindow_Winforms.Forms
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabPage1 = new TabPage();
            panel2 = new Panel();
            label1 = new Label();
            menuStrip1 = new MenuStrip();
            menuToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            dockToolStripMenuItem = new ToolStripMenuItem();
            openAppToolStripMenuItem = new ToolStripMenuItem();
            dockOpenAppToolStripMenuItem = new ToolStripMenuItem();
            undockAppToolStripMenuItem1 = new ToolStripMenuItem();
            undockAllAppToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            chToolStripMenuItem = new ToolStripMenuItem();
            tabControl1 = new TabControl();
            tabPage2 = new TabPage();
            panel2.SuspendLayout();
            menuStrip1.SuspendLayout();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.FromArgb(56, 57, 60);
            tabPage1.Location = new Point(4, 27);
            tabPage1.Margin = new Padding(4, 3, 4, 3);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(4, 3, 4, 3);
            tabPage1.Size = new Size(792, 503);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "New tab";
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(43, 43, 43);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(menuStrip1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(800, 30);
            panel2.TabIndex = 4;
            panel2.MouseDown += TitleBarPanel_MouseDown;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Nirmala UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(209, 209, 209);
            label1.Location = new Point(773, -2);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(25, 30);
            label1.TabIndex = 0;
            label1.Text = "X";
            label1.Click += ExitButton_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Anchor = AnchorStyles.Left;
            menuStrip1.AutoSize = false;
            menuStrip1.BackColor = Color.FromArgb(43, 43, 43);
            menuStrip1.Dock = DockStyle.None;
            menuStrip1.Items.AddRange(new ToolStripItem[] { menuToolStripMenuItem, dockToolStripMenuItem, editToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 2, 0, 2);
            menuStrip1.Size = new Size(167, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            menuToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, exitToolStripMenuItem });
            menuToolStripMenuItem.ForeColor = Color.FromArgb(209, 209, 209);
            menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            menuToolStripMenuItem.Size = new Size(50, 24);
            menuToolStripMenuItem.Text = "&Menu";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.BackColor = Color.FromArgb(43, 43, 43);
            openToolStripMenuItem.ForeColor = Color.FromArgb(209, 209, 209);
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(128, 22);
            openToolStripMenuItem.Text = "&Open App";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.BackColor = Color.FromArgb(43, 43, 43);
            exitToolStripMenuItem.ForeColor = Color.FromArgb(209, 209, 209);
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(128, 22);
            exitToolStripMenuItem.Text = "&Exit";
            exitToolStripMenuItem.Click += ExitButton_Click;
            // 
            // dockToolStripMenuItem
            // 
            dockToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openAppToolStripMenuItem, dockOpenAppToolStripMenuItem, undockAppToolStripMenuItem1, undockAllAppToolStripMenuItem });
            dockToolStripMenuItem.ForeColor = Color.FromArgb(209, 209, 209);
            dockToolStripMenuItem.Name = "dockToolStripMenuItem";
            dockToolStripMenuItem.Size = new Size(46, 24);
            dockToolStripMenuItem.Text = "&Dock";
            // 
            // openAppToolStripMenuItem
            // 
            openAppToolStripMenuItem.BackColor = Color.FromArgb(43, 43, 43);
            openAppToolStripMenuItem.ForeColor = Color.FromArgb(209, 209, 209);
            openAppToolStripMenuItem.Name = "openAppToolStripMenuItem";
            openAppToolStripMenuItem.Size = new Size(180, 22);
            openAppToolStripMenuItem.Text = "&Open App";
            openAppToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // dockOpenAppToolStripMenuItem
            // 
            dockOpenAppToolStripMenuItem.BackColor = Color.FromArgb(43, 43, 43);
            dockOpenAppToolStripMenuItem.ForeColor = Color.FromArgb(209, 209, 209);
            dockOpenAppToolStripMenuItem.Name = "dockOpenAppToolStripMenuItem";
            dockOpenAppToolStripMenuItem.Size = new Size(180, 22);
            dockOpenAppToolStripMenuItem.Text = "&Dock Open App";
            dockOpenAppToolStripMenuItem.Click += dockOpenAppToolStripMenuItem_Click;
            // 
            // undockAppToolStripMenuItem1
            // 
            undockAppToolStripMenuItem1.BackColor = Color.FromArgb(43, 43, 43);
            undockAppToolStripMenuItem1.ForeColor = Color.FromArgb(209, 209, 209);
            undockAppToolStripMenuItem1.Name = "undockAppToolStripMenuItem1";
            undockAppToolStripMenuItem1.Size = new Size(180, 22);
            undockAppToolStripMenuItem1.Text = "&Undock App";
            undockAppToolStripMenuItem1.Click += undockAppToolStripMenuItem1_Click;
            // 
            // undockAllAppToolStripMenuItem
            // 
            undockAllAppToolStripMenuItem.BackColor = Color.FromArgb(43, 43, 43);
            undockAllAppToolStripMenuItem.ForeColor = Color.FromArgb(209, 209, 209);
            undockAllAppToolStripMenuItem.Name = "undockAllAppToolStripMenuItem";
            undockAllAppToolStripMenuItem.Size = new Size(180, 22);
            undockAllAppToolStripMenuItem.Text = "U&ndock All App";
            undockAllAppToolStripMenuItem.Click += undockAllAppToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { chToolStripMenuItem });
            editToolStripMenuItem.ForeColor = Color.FromArgb(209, 209, 209);
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 24);
            editToolStripMenuItem.Text = "&Edit";
            // 
            // chToolStripMenuItem
            // 
            chToolStripMenuItem.BackColor = Color.FromArgb(43, 43, 43);
            chToolStripMenuItem.ForeColor = Color.FromArgb(209, 209, 209);
            chToolStripMenuItem.Name = "chToolStripMenuItem";
            chToolStripMenuItem.Size = new Size(180, 22);
            chToolStripMenuItem.Text = "&Change Tab Name";
            chToolStripMenuItem.Click += chToolStripMenuItem_Click;
            // 
            // tabControl1
            // 
            tabControl1.Appearance = TabAppearance.Buttons;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 30);
            tabControl1.Margin = new Padding(4, 3, 4, 3);
            tabControl1.Multiline = true;
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(800, 534);
            tabControl1.TabIndex = 5;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.FromArgb(56, 57, 60);
            tabPage2.BorderStyle = BorderStyle.Fixed3D;
            tabPage2.Location = new Point(4, 27);
            tabPage2.Margin = new Padding(4, 3, 4, 3);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(4, 3, 4, 3);
            tabPage2.Size = new Size(792, 503);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "+";
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 564);
            Controls.Add(tabControl1);
            Controls.Add(panel2);
            Name = "Main";
            Text = "Main";
            FormClosing += Main_FormClosing;
            SizeChanged += Main_SizeChanged;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabPage tabPage1;
        private Panel panel2;
        private Label label1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem dockToolStripMenuItem;
        private ToolStripMenuItem openAppToolStripMenuItem;
        private ToolStripMenuItem dockOpenAppToolStripMenuItem;
        private ToolStripMenuItem undockAppToolStripMenuItem1;
        private ToolStripMenuItem undockAllAppToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem chToolStripMenuItem;
        private TabControl tabControl1;
        private TabPage tabPage2;
    }
}