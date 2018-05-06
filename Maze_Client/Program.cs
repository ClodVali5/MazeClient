using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maze_Client
{
    static class Constants
    {
        public const string RequestURL = "http://Localhost:3000";
        public const string fn = @"\MazeServer.exe";
    }

    static class Program
    {      
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MazeClient());
        }       
    }
}
