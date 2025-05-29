
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaptureWindow.Classes
{
    public class TabControlManager
    {
        private readonly TabControl _tabControl;

        public TabControlManager(TabControl tabControl)
        {
            _tabControl = tabControl;
        }


        public void AddNewTab(string tabName)
        {
            // Create a new TabPage
            TabPage newTab = new TabPage
            {
                Text = tabName 
            };

            // Add the new TabPage to the TabControl
            _tabControl.TabPages.Add(newTab);

            // Update the second-to-last tab's text
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

            // Ensure there are at least two tabs
            if (tabCount >= 2)
            {
                // Index of the second-to-last tab
                int secondToLastIndex = tabCount - 2;

                // Get and return the second-to-last tab
                return _tabControl.TabPages[secondToLastIndex];
            }

            // If there are fewer than two tabs, return null or handle as needed
            return null;
        }
    }
}
