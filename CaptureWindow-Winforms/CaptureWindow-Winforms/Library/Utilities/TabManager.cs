using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaptureWindow_Winforms.Library.Utilities
{
    public class TabManager
    {
        private readonly TabControl _tabControl = null;
        private readonly WindowManager _windowManager = null;

        internal TabPage selectedTab;

        public TabManager(TabControl tabControl)
        {
            _tabControl = tabControl;
            _windowManager = new WindowManager();
            selectedTab = _tabControl.TabPages[0];
            _tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
            _tabControl.DoubleClick += TabControl_DoubleClick;
        }
        public TabManager(TabControl tabControl, WindowManager windowManager)
        {
            _tabControl = tabControl;
            _windowManager = windowManager;
            selectedTab = _tabControl.TabPages[0];
            _tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
            _tabControl.DoubleClick += TabControl_DoubleClick;
        }

        private void TabControl_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (sender is TabControl tabControl)
            {
                selectedTab = tabControl.SelectedTab;
                if (selectedTab != null && _windowManager != null)
                    _windowManager.ResizeAndDockApp(selectedTab);

                bool isLastTab = tabControl.SelectedIndex == tabControl.TabCount - 1;
                if (isLastTab)
                {
                    AddNewTab("+");
                    if (_windowManager != null)
                        _windowManager.LaunchAndDockApp(selectedTab);
                }
            }
        }
        private void TabControl_DoubleClick(object? sender, EventArgs e)
        {
            if (_windowManager != null)
                _windowManager.CloseTabHandle(selectedTab);
            _tabControl.TabPages.Remove(selectedTab);

            TabPage secondToLastTab = GetSecondToLastTab();
            _tabControl.SelectedTab = secondToLastTab;
        }

        public void AddNewTab(string tabName)
        {
            TabPage newTab = new TabPage
            {
                Text = tabName
            };

            _tabControl.TabPages.Add(newTab);

            int tabCount = _tabControl.TabPages.Count;
            TabPage secondToLastTab = GetSecondToLastTab();
            if (secondToLastTab != null)
            {
                secondToLastTab.Text = $"New tab {tabCount - 1}";
                secondToLastTab.BackColor = System.Drawing.Color.FromArgb(56, 57, 60);
            }
        }

        public TabPage GetSecondToLastTab()
        {
            int tabCount = _tabControl.TabPages.Count;

            if (tabCount >= 2)
            {
                int secondToLastIndex = tabCount - 2;

                return _tabControl.TabPages[secondToLastIndex];
            }

            return null;
        }
    }
}
