using CaptureWindow_Winforms.Forms;
using CaptureWindow_Winforms.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaptureWindow_Winforms.Library
{
    public class Client
    {
        internal TabControl tabControl;
        internal TabManager tabManager;
        internal WindowManager windowManager;

        public Client(TabControl tabControl)
        {
            windowManager = new WindowManager();
            tabManager = new TabManager(tabControl, windowManager);
            this.tabControl = tabControl;
        }

        public void Close()
        {
            windowManager.CleanUp();
            windowManager.CloseAllEmbeddedApps();
            Application.Exit();
        }

        public void FormResized()
        {
            if (tabManager.selectedTab == null) return;
            windowManager.ResizeAndDockApp(tabManager.selectedTab);
        }

        public void TitleBarMouseDown(Control control)
        {
            Form parentForm = control.Parent.FindForm();
            if (parentForm != null)
            {
                windowManager.ReleaseCapture();
                windowManager.SendMessage(parentForm.Handle, 0x112, 0xf012, 0);
            }
        }

        public void LauchAndDock(Form form)
        { 
            windowManager.LaunchAndDockApp(form);
        }
        public void LauchAndDock()
        {
            windowManager.LaunchAndDockApp(tabManager.selectedTab);
        }
        public void UndockApp()
        {
            windowManager.UndockApp(tabManager.selectedTab);
        }
        public void UndockAllApp()
        {
            windowManager.UndockAllApp();
        }

        public void SelectOpenApp()
        {
            using (OpenAppSelection_Form selectionForm = new OpenAppSelection_Form(this))
            {
                if (selectionForm.ShowDialog() == DialogResult.OK)
                {
                    IntPtr selectedAppHandle = selectionForm.SelectedApp.Handle;
                    string selectedAppTitle = selectionForm.SelectedApp.Title;

                    if (selectedAppHandle != IntPtr.Zero && tabManager.selectedTab != null)
                    {
                        windowManager.EmbedSelectedApp(selectedAppHandle, tabManager.selectedTab);
                        tabManager.selectedTab.Text = selectedAppTitle;
                    }
                }
            }
        }

        public void ChangeTabName()
        {

            if (tabManager.selectedTab != null)
            {
                using (TabRename_Form changeTabNameForm = new TabRename_Form())
                {

                    changeTabNameForm.TabName = tabManager.selectedTab.Text;

                    if (changeTabNameForm.ShowDialog() == DialogResult.OK)
                    {
                        tabManager.selectedTab.Text = changeTabNameForm.TabName;
                    }
                }
            }
            else
            {
                MessageBox.Show("No tab selected to rename!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
