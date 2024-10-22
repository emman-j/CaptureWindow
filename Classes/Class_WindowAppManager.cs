using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaptureWindow.Classes
{
    public class WindowAppManager
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        private static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool IsWindowVisible(IntPtr hWnd); 
        
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")] 
        private static extern bool SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public POINT ptMinPosition;
            public POINT ptMaxPosition;
            public RECT rcNormalPosition;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        public extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        public extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        private static readonly Dictionary<TabPage, IntPtr> _tabAppHandles = new Dictionary<TabPage, IntPtr>();

        private const int SW_SHOW = 5;
        private const int SW_MAXIMIZE = 3;
        private const int SW_HIDE = 0;
        private const uint WM_CLOSE = 0x0010; 
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MAXIMIZE = 0xF030;
        private const int SC_RESTORE = 0xF120;

        private static IntPtr _embeddedAppHandle;
        private static IntPtr dllHandle = IntPtr.Zero;
        private static CancellationTokenSource cancellationTokenSource;
        private static Thread backgroundThread;

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        private static void ReleaseExternalDLLs()
        {
            // Free any loaded DLLs
            if (dllHandle != IntPtr.Zero)
            {
                FreeLibrary(dllHandle);
                dllHandle = IntPtr.Zero;
            }
        }
        private static void CleanupEmbeddedWindows()
        {
            if (_embeddedAppHandle != IntPtr.Zero)
            {
                // Optionally hide or close the embedded window
                ShowWindow(_embeddedAppHandle, SW_HIDE);
                // Release the handle if necessary
                _embeddedAppHandle = IntPtr.Zero;
            }
        }
        private static void StopBackgroundThreads()
        {
            // Example: Cancel any ongoing tasks or threads
            cancellationTokenSource?.Cancel();
            backgroundThread?.Join(); // Ensure the thread has stopped
        }
        public static void CleanUp()
        {
            StopBackgroundThreads();
            ReleaseExternalDLLs();
            CleanupEmbeddedWindows();
        }
        public static void LaunchAndDockApp(string exePath, Panel panel)
        {
            // Start the external application
            Process process = Process.Start(exePath);
            process.WaitForInputIdle(); // Wait for the process to be ready

            // Find the window handle of the most recent window for the process
            _embeddedAppHandle = GetMainWindowHandle(process.Id);
            if (_embeddedAppHandle == IntPtr.Zero)
            {
                MessageBox.Show("Failed to find the application window.");
                return;
            }

            // Set the parent of the external application window to the panel
            SetParent(_embeddedAppHandle, panel.Handle);
            ShowWindow(_embeddedAppHandle, SW_SHOW);

            // Adjust size and position of the embedded window
            ResizeAndDockApp(panel);
        } 
        public static void LaunchAndDockApp(Panel panel)
        {
            string selectedFile = "";

            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = @"C:\Users\admin\Documents\Training";
                    openFileDialog.Title = "Select App";
                    openFileDialog.Filter = "Executable files (*.exe)|*.exe|Application shortcuts (*.lnk)|*.lnk|All files (*.*)|*.*";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        selectedFile = openFileDialog.FileName;
                    }
                    else
                    {
                        CleanUp();
                        return;
                    }
                }

                if (string.IsNullOrWhiteSpace(selectedFile))
                {
                    CleanUp();
                    return;
                }

                // Start the external application
                Process process = Process.Start(selectedFile);
                if (!WaitForMainWindow(process))
                {
                    MessageBox.Show("Failed to find the application window.");
                    return;
                }
                //process.WaitForInputIdle(); // Wait for the process to be ready

                // Find the window handle of the most recent window for the process
                _embeddedAppHandle = GetMainWindowHandle(process.Id);
                if (_embeddedAppHandle == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to find the application window.");
                    return;
                }

                // Set the parent of the external application window to the panel
                SetParent(_embeddedAppHandle, panel.Handle);
                ShowWindow(_embeddedAppHandle, SW_SHOW);

                // Adjust size and position of the embedded window
                ResizeAndDockApp(panel);

                //SendKeys.SendWait("{F11}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                CleanUp();
            }
        }
        public static void LaunchAndDockApp(TabPage tabpage)
        {
            string selectedFile = ""; 
            string fileNameWithoutExtension = "";

            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = @"C:\Users\admin\Documents\Training";
                    openFileDialog.Title = "Select App";
                    openFileDialog.Filter = "Executable files (*.exe)|*.exe|Application shortcuts (*.lnk)|*.lnk|All files (*.*)|*.*";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        selectedFile = openFileDialog.FileName; 
                        fileNameWithoutExtension = Path.GetFileNameWithoutExtension(selectedFile);
                    }
                    else
                    {
                        CleanUp();
                        return;
                    }
                }

                if (string.IsNullOrWhiteSpace(selectedFile))
                {
                    CleanUp();
                    return;
                }

                // Start the external application
                Process process = Process.Start(selectedFile);
                if (!WaitForMainWindow(process))
                {
                    MessageBox.Show("Failed to find the application window.");
                    return;
                }
                //process.WaitForInputIdle(); // Wait for the process to be ready

                // Find the window handle of the most recent window for the process
                _embeddedAppHandle = GetMainWindowHandle(process.Id);
                if (_embeddedAppHandle == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to find the application window.");
                    return;
                }

                // Set the parent of the external application window to the panel
                //SendKeys.SendWait("{F11}");
                SetParent(_embeddedAppHandle, tabpage.Handle);
                ShowWindow(_embeddedAppHandle, SW_SHOW);

                // Adjust size and position of the embedded window
                tabpage.Text = fileNameWithoutExtension;

                ResizeAndDockApp(tabpage); 
                _tabAppHandles[tabpage] = _embeddedAppHandle;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                CleanUp();
            }
        }
        private static bool WaitForMainWindow(Process process, int timeout = 10000)
        {
            int waited = 0;
            const int interval = 500;  // Time to wait between checks (in milliseconds)

            // Loop until the process's main window handle is set or until we exceed the timeout
            while (process.MainWindowHandle == IntPtr.Zero)
            {
                if (waited >= timeout)
                {
                    return false; // Timeout reached, return false
                }
                Thread.Sleep(interval); // Wait for a short period before checking again
                waited += interval;
                process.Refresh(); // Refresh the process object to get the updated MainWindowHandle
            }

            return true; // Main window is available
        }
        private static IntPtr GetMainWindowHandle(int processId)
        {
            IntPtr appWindowHandle = IntPtr.Zero;

            // Callback function for EnumWindows
            EnumWindowsProc enumProc = (hWnd, lParam) =>
            {
                GetWindowThreadProcessId(hWnd, out uint windowProcessId);

                // Check if the window belongs to the given process and is visible
                
                if (windowProcessId == processId && IsWindowVisible(hWnd))
                {
                    appWindowHandle = hWnd; // Get the handle of the most recent window
                    return false; // Stop enumerating windows once we find it
                }

                return true; // Continue enumerating
            };

            EnumWindows(enumProc, IntPtr.Zero);

            return appWindowHandle;
        }
        public static void ResizeAndDockApp(Panel panel)
        {
            if (_embeddedAppHandle != IntPtr.Zero)
            {
                MoveWindow(_embeddedAppHandle, 0, 0, panel.ClientSize.Width, panel.ClientSize.Height, true);
            }
        }
        //public static void ResizeAndDockApp(TabPage tabPage)
        //{
        //    if (_embeddedAppHandle != IntPtr.Zero)
        //    {
        //        MoveWindow(_embeddedAppHandle, 0, 0, tabPage.ClientSize.Width, tabPage.ClientSize.Height, true);
        //    }
        //}
        public static void ResizeAndDockApp(TabPage tabPage)
        {
            //if (_tabAppHandles.TryGetValue(tabPage, out IntPtr appHandle) && appHandle != IntPtr.Zero)
            //{
            //    // Resize and position the embedded application window to match the TabPage's dimensions
            //    MoveWindow(appHandle, 0, 0, tabPage.ClientSize.Width, tabPage.ClientSize.Height, true);

            //    // Optional: maximize the window if not already maximized
            //    SendMessage(appHandle, WM_SYSCOMMAND, (IntPtr)SC_MAXIMIZE, IntPtr.Zero);
            //}

            WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
            placement.length = Marshal.SizeOf(placement);

            if (_tabAppHandles.TryGetValue(tabPage, out IntPtr appHandle) && appHandle != IntPtr.Zero)
            {
                // Resize and position the embedded application window to match the TabPage's dimensions
                MoveWindow(appHandle, 0, 0, tabPage.ClientSize.Width, tabPage.ClientSize.Height, true);

                //// Check if the window is already maximized 
                //if (GetWindowPlacement(appHandle, ref placement) && placement.showCmd != SW_MAXIMIZE)
                //{
                //   // Maximize the window if it's not already maximized
                //   //SendMessage(appHandle, WM_SYSCOMMAND, (IntPtr)SC_MAXIMIZE, IntPtr.Zero);
                //}
            }
        }
        public static void CloseAllEmbeddedApps()
        {
            foreach (var handle in _tabAppHandles.Values)
            {
                if (handle != IntPtr.Zero)
                {                
                    UndockApp(handle);
                    SendMessage(handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                }
            }

            _tabAppHandles.Clear();
        }
        public static void CloseTabHandle(TabPage tabPage)
        {
            if (_tabAppHandles.TryGetValue(tabPage, out IntPtr appHandle) && appHandle != IntPtr.Zero)
            {
                // Send WM_CLOSE message to the handle to close the window
                UndockApp(tabPage);
                SendMessage(appHandle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);

                // Remove the handle from the dictionary
                _tabAppHandles.Remove(tabPage);
            }
        } 
        public static void UndockApp(TabPage tabPage)
        {
            // Check if the TabPage contains an embedded app
            if (_tabAppHandles.TryGetValue(tabPage, out IntPtr appHandle) && appHandle != IntPtr.Zero)
            {
                // Remove the app from the dictionary
                _tabAppHandles.Remove(tabPage);

                // Set the parent of the app to IntPtr.Zero (detach from the TabPage)
                SetParent(appHandle, IntPtr.Zero);

                // Restore the window to normal size and show it
                ShowWindow(appHandle, SW_SHOW); // SW_SHOW to make it visible (adjust this if needed)
                MoveWindow(appHandle, 100, 100, 800, 600, true); // Optionally move to a default position and size

                // Optionally, refresh the window style (e.g., remove maximization, restore)
                SendMessage(appHandle, WM_SYSCOMMAND, (IntPtr)SC_RESTORE, IntPtr.Zero);
            }
        }
        public static void UndockApp(IntPtr handle)
        {
            SetParent(handle, IntPtr.Zero);

            ShowWindow(handle, SW_SHOW); 
            MoveWindow(handle, 100, 100, 800, 600, true); 

            SendMessage(handle, WM_SYSCOMMAND, (IntPtr)SC_RESTORE, IntPtr.Zero);
        } 
        public static void UndockAllApp()
        {
            foreach (var handle in _tabAppHandles.Values)
            {
                if (handle != IntPtr.Zero)
                {
                    UndockApp(handle);
                } 
                //SendKeys.SendWait("{F11}");
            }
            _tabAppHandles.Clear();
        }







        private List<IntPtr> _openAppHandles = new List<IntPtr>();

        private void GetOpenApplications()
        {
            _openAppHandles.Clear();

            EnumWindows((hWnd, lParam) =>
            {
                GetWindowThreadProcessId(hWnd, out uint processId);
                if (IsWindowVisible(hWnd) && !string.IsNullOrEmpty(GetWindowTitle(hWnd)))
                {
                    _openAppHandles.Add(hWnd);
                    return true; // Continue enumerating
                }
                return true; // Continue enumerating
            }, IntPtr.Zero);
        }

        private string GetWindowTitle(IntPtr hWnd)
        {
            const int MAX_TITLE_LENGTH = 256;
            StringBuilder sb = new StringBuilder(MAX_TITLE_LENGTH);
            GetWindowText(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }
        public static void EmbedSelectedApp(IntPtr selectedAppHandle, TabPage tabPage)
        {
            // Set the parent of the selected application window to the TabPage 
            //SendKeys.SendWait("{F11}");
            SetParent(selectedAppHandle, tabPage.Handle);
            ShowWindow(selectedAppHandle, SW_SHOW);

            // Adjust size and position of the embedded window
            ResizeAndDockApp(tabPage, selectedAppHandle);
            _tabAppHandles[tabPage] = selectedAppHandle;
        } 
        private static void ResizeAndDockApp(TabPage tabPage, IntPtr appHandle)
        {
            if (appHandle != IntPtr.Zero)
            {
                MoveWindow(appHandle, 0, 0, tabPage.ClientSize.Width, tabPage.ClientSize.Height, true);
            }
        }

    }

}

