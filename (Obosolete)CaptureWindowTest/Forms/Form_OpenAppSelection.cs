using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CaptureWindow
{
    public partial class Form_OpenAppSelection : Form
    {
        public IntPtr SelectedAppHandle { get; private set; }
        public string SelectedAppTitle { get; private set; }

        public Form_OpenAppSelection()
        {
            InitializeComponent();
            LoadOpenApplications();
        }

        private void LoadOpenApplications()
        {
            // Example method to load open applications into a ListBox
            listBox1.Items.Clear();
            List<IntPtr> appHandles = GetOpenApplicationHandles();
            foreach (var handle in appHandles)
            {
                //listBox1.Items.Add(new AppItem { Handle = handle, Title = GetWindowTitle(handle) });
                string title = GetWindowTitle(handle);
                if (!string.IsNullOrEmpty(title))
                {
                    //listBox1.Items.Add(title);
                    listBox1.Items.Add(new AppItem { Handle = handle, Title = title });
                }
            }
        }

        private void listBoxApps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is AppItem selectedItem)
            {
                SelectedAppHandle = selectedItem.Handle;
                SelectedAppTitle = selectedItem.Title;
            }
        }
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private List<IntPtr> GetOpenApplicationHandles()
        {
            var handles = new List<IntPtr>();
            EnumWindows((hWnd, lParam) =>
            {
                if (IsWindowVisible(hWnd))
                {
                    handles.Add(hWnd);
                }
                return true;
            }, IntPtr.Zero);
            return handles;
        }

        private string GetWindowTitle(IntPtr hWnd)
        {
            const int MAX_TITLE_LENGTH = 256;
            StringBuilder sb = new StringBuilder(MAX_TITLE_LENGTH);
            GetWindowText(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }

        private class AppItem
        {
            public IntPtr Handle { get; set; }
            public string Title { get; set; }

            public override string ToString() => Title;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        private void Form_OpenAppSelection_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (components != null)
            {
                components.Dispose();
                SelectedAppHandle = IntPtr.Zero;
            }
        }

    }
}
