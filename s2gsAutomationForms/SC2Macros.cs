using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace s2gsAutomationForms
{
    static class SC2Macros
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;
        public const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        public const int MOUSEEVENTF_RIGHTUP = 0x10;

        public static void openMatchHistory(String name, String code)
        {
            // Super hacky macro to open match history.
            // TODO: Pull coordinates into named variables
            // TODO: Create a set of instructions in an array and just iterate
            //      through it rather than writing MouseClick/Thread.Sleep/SendKeys
            //      a million times each.
            // TODO: Use windows API to get size and position of SC2 window
            //      and adjust coordinates accordingly rather than assuming
            //      upper left positioning and constant resolution.
            // TODO: Run this in a seperate thread so it doesn't block the UI.
            //      (requires an event/callback/etc to let the UI know when it's
            //      done though).

            MouseClick(830, 765); //open friend list
            Thread.Sleep(100);
            MouseClick(860, 730); // add friend
            Thread.Sleep(100);
            MouseClick(640, 400); // character friend
            Thread.Sleep(100);
            MouseClick(500, 415); // name field focus
            Thread.Sleep(20);
            SendKeys.SendWait(name); // enter name
            Thread.Sleep(20);
            MouseClick(500, 470); // code field focus
            Thread.Sleep(20);
            SendKeys.SendWait(code); // enter code
            //Thread.Sleep(100);
            MouseClick(500, 530); // confirm add
            MouseClick(680, 300); // close add char friend window
            Thread.Sleep(300);
            MouseRightClick(900, 485); // friend menu
            Thread.Sleep(300);
            MouseClick(930, 555); // view profile
            Thread.Sleep(500);
            MouseClick(150, 450); // match history
            Thread.Sleep(100);
            MouseClick(830, 765); // open friend list
            Thread.Sleep(200);
            MouseRightClick(900, 485); // friend menu
            Thread.Sleep(100);
            MouseClick(920, 613); // more...
            Thread.Sleep(100);
            MouseClick(920, 613); // remove friend
            Thread.Sleep(200);
            MouseClick(415, 480); // confirm 
            Thread.Sleep(100);
            MouseClick(830, 765); // close friend list
        }

        private static void MouseClick(int x, int y)
        {
            Cursor.Position = new Point(x, y);
            mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);
        }

        private static void MouseRightClick(int x, int y)
        {
            Cursor.Position = new Point(x, y);
            mouse_event(MOUSEEVENTF_RIGHTDOWN, x, y, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, x, y, 0, 0);
        }
    }
}
