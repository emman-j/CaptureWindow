using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace CaptureWindow.Forms
{
    public partial class Form_ChangeTabName : Form
    {
        public string TabName { get; set; }
        public Form_ChangeTabName()
        {
            InitializeComponent();
            if (TabName != null)
            { 
                textBox1.Text = TabName;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            TabName = textBox1.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Form_ChangeTabName_Load(object sender, EventArgs e)
        {
            if (TabName != null)
            {
                textBox1.Text = TabName;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                saveButton.PerformClick();
            }
        }
    }
}
