using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Maze_Client
{
    public class SolveThread
    {
        private bool running = true;
        private Thread th;

        public SolveThread()
        {
            this.th = new Thread(Run);
            this.th.Start();
        }

        public void Run()
        {
            int i = 0;
            while (running)
            {
                Console.WriteLine("runnin' " + ++i);
                // some work...
            }
            Console.WriteLine("end Run");
        }

        public void stopThread()
        {
            running = false;
        }



    }
}

