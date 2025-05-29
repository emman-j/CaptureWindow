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
    public partial class TabRename_Form : Form, INotifyPropertyChanged
    {
        private string _tabName;
        public event PropertyChangedEventHandler? PropertyChanged;
        public string TabName
        { 
            get => _tabName;
            set 
            {
                if (value != _tabName)
                { 
                    _tabName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyname = "")
        { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public TabRename_Form()
        {
            InitializeComponent();
            textBox1.DataBindings.Add("Text", this, nameof(TabName), false, DataSourceUpdateMode.OnPropertyChanged);
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
