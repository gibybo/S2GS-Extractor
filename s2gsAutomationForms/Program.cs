using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace s2gsAutomationForms
{
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

            //Thread monitor = new Thread(new ThreadStart(monitorCursorPos));
            //monitor.Start();

            Application.Run(new Form1());

            
        }

        static void monitorCursorPos()
        {
            while (true)
            {
                Console.WriteLine("Cursor position: " + Cursor.Position.ToString());
                Thread.Sleep(100);
            }
        }

    }
}
