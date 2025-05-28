using CaptureWindow_Winforms.Library;
using CaptureWindow_Winforms.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaptureWindow_Winforms.Forms
{
    public partial class OpenAppSelection_Form : Form
    {
        private readonly Client _client;
        public OpenApp SelectedApp { get; private set; }

        public OpenAppSelection_Form(Client client)
        {
            InitializeComponent();
            _client = client;
            LoadAllOpenApplications();
        }

        private void LoadAllOpenApplications()
        {
            var apps = _client.windowManager.GetOpenApplications();
            foreach (var kvp in apps)
            {
                listBox1.Items.Add(new OpenApp { Handle = kvp.Value, Title = kvp.Key });
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is OpenApp selectedItem)
            {
                SelectedApp = selectedItem;
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
