using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maze_Client
{
    public class Launch
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetDlgItem(IntPtr hWnd, int nIDDlgItem);
        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);
     
        /// <summary>
        /// Kontrolle ob MazeServer (Application laeuft), sonst Starten
        /// </summary>
        public void LaunchMazeServer(string _classname, string _appname)
        {

            IntPtr handle = IntPtr.Zero;
            Process[] localAll = Process.GetProcesses();
            foreach (Process p in localAll)
            {
                if (p.MainWindowHandle != IntPtr.Zero)
                {
                    ProcessModule pm = GetModule(p);
                    if (pm != null && p.MainModule.FileName.Contains(Constants.fn))
                        handle = p.MainWindowHandle;
                }
            }

            if (handle == IntPtr.Zero)
            {
                Console.WriteLine("Not found");

                // Initializes the variables to pass to the MessageBox.Show method.
                string message = "Maze-Server(Application) nicht gefunden. Wollen Sie den Server starten ?";
                string caption = "! Server nicht gestartet !";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                // Displays the MessageBox.
                result = MessageBox.Show(message, caption, buttons);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {

                    string toolpath = Application.StartupPath + Constants.fn;

                    Process proc = new Process();
                    proc.StartInfo = new ProcessStartInfo(toolpath);
                    proc.Start();

                    return;
                }                
            }                       
        }

        /// <summary>
        /// Get-Module: Suche ob App gestartet
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private static ProcessModule GetModule(Process p)
        {
            ProcessModule pm = null;
            try { pm = p.MainModule; }
            catch
            {
                return null;
            }

            Console.WriteLine(pm.FileName);
            return pm;
        }              

    }
}
