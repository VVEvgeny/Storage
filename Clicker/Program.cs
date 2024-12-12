using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

var acts = new List<IActionBase>();

//task ms 
int repeat = 35;
while(repeat-- >0)
{



//windows
    bool favorites = false; //only julia has

    acts.Add(new ActionWait(5));
    acts.Add(new ActionMouseClick(645,131 + (favorites ? 36 :0)));
    acts.Add(new ActionWait(1));
    acts.Add(new ActionKeyClick(getRandomAvailableChar("qwertyuiopasdfghjklzxcvbnm0123456789 ")));
    acts.Add(new ActionMouseClick(780,129 + (favorites ? 36 :0)));


/*
//DOESNT WORK

//mobile sams 20+ 100%
    acts.Add(new ActionWait(5));
    acts.Add(new ActionMouseClick(1059,220));
    acts.Add(new ActionWait(1));
    acts.Add(new ActionKeyClick(getRandomAvailableChar("qwertyuiopasdfghjklzxcvbnm0123456789 ")));
    acts.Add(new ActionKeyClick(KeyboardOperations.VK_RETURN));

*/

}





/*
acts.Add(new ActionWait(5));
acts.Add(new ActionKeyClick('a'));
acts.Add(new ActionKeyClick('!'));
acts.Add(new ActionKeyClick('0'));
acts.Add(new ActionKeyClick('c'));
acts.Add(new ActionKeyClick('G'));
//acts.Add(new ActionMouseClick(100,500,false));
acts.Add(new ActionWait(2));
*/


Console.WriteLine("Start: acts:"+acts.Count);

foreach(var i in acts)
{
    i.Work();
}




char getRandomAvailableChar(string s)
{
    var r = new Random();
    return s[r.Next(0, s.Length - 1)];
}

    
interface IActionBase
{
    void Work();
}

class ActionMouseClick: IActionBase
{
    int _x,_y;
    bool _left;
    public ActionMouseClick(int x, int y, bool left = true)
    {
        _x = x;
        _y = y;
        _left = left;
    }

    public void Work()
    {
        Console.WriteLine("ActionMouseClick start x:"+_x+" y:"+_y+" button:"+(_left?"left":"right"));
        
        //mouse go to
        //mouse click
        MouseOperations.SetCursorPosition(_x, _y);
        MouseOperations.MouseEvent(_left ? MouseOperations.MouseEventFlags.LeftDown : MouseOperations.MouseEventFlags.RightDown);
        Thread.Sleep(100);
        MouseOperations.MouseEvent(_left ? MouseOperations.MouseEventFlags.LeftUp : MouseOperations.MouseEventFlags.RightUp);

        //Console.WriteLine("ActionMouseClick end");
    }
}

class ActionKeyClick: IActionBase
{   
    string _s;
    uint _c;

    public ActionKeyClick(uint c)
    {
        _c = c;
    }

    public ActionKeyClick(char c)
    {
        _s = c.ToString();
    }

    public ActionKeyClick(string c)
    {
        _s = c;
    }

    public void Work()
    {
        if(!string.IsNullOrEmpty(_s))
        {
            Console.WriteLine("ActionKeyClick key:"+_s);
            foreach(var c in _s)
            {
                KeyboardOperations.SendKey(c);
            }
        }
        else
        {
            Console.WriteLine("ActionKeyClick spec key:"+_c);
            KeyboardOperations.SendKeyD(_c);
        }
    }
}

class ActionWait: IActionBase
{
    private int _sec;
    public ActionWait(int sec)
    {
        _sec = sec;
    }

    public void Work()
    {
        Console.WriteLine("ActionWait start:"+_sec+" sec");
        Thread.Sleep(_sec*1000);
        //Console.WriteLine("ActionWait end");
    }
}


        public class KeyboardOperations
        {
            [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            public static extern void keybd_event(uint bVk, uint bScan, uint dwFlags, uint dwExtraInfo);


            //https://learn.microsoft.com/ru-ru/windows/win32/inputdev/virtual-key-codes?redirectedfrom=MSDN

            public const uint VK_RETURN = 0x0D;
            public const uint VK_LSHIFT = 0xA0;
            public const uint VK_SPACE = 0x20;

            public const int KEYEVENTF_KEYDOWN = 0x0000; // New definition
            public const int KEYEVENTF_EXTENDEDKEY = 0x0001; //Key down flag
            public const int KEYEVENTF_KEYUP = 0x0002; //Key up flag

            private const uint VK_KEY_0 = 0x30;
            private const uint VK_KEY_A = 0x41;

            static uint charToKey(char c)
            {
                if(c>='0' && c<='9')
                    return VK_KEY_0 + (uint)(c - '0');
                if(c>='a' && c<='z')
                    return VK_KEY_A + (uint)(c - 'a');
                if(c>='A' && c<='Z')
                    return VK_KEY_A + (uint)(c - 'A');
      
                Console.WriteLine("Dont know how to convert this char:"+c);
                return VK_SPACE; //default
            }

            static bool isShifted(char c)
            {
                return (c>='A' && c<='Z');
            }

            public static void SendKey(char key)
            {
                var c = charToKey(key);
                if(isShifted(key))
                    keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0);

                keybd_event(c, 0, KEYEVENTF_KEYDOWN, 0);
                keybd_event(c, 0, KEYEVENTF_KEYUP, 0);

                if(isShifted(key))
                    keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
            }
            
            public static void SendKeyD(uint c)
            {
                keybd_event(c, 0, KEYEVENTF_KEYDOWN, 0);
                keybd_event(c, 0, KEYEVENTF_KEYUP, 0);
            }
        }

        public class MouseOperations
        {
            [Flags]
            public enum MouseEventFlags
            {
                LeftDown = 0x00000002,
                LeftUp = 0x00000004,
                MiddleDown = 0x00000020,
                MiddleUp = 0x00000040,
                Move = 0x00000001,
                Absolute = 0x00008000,
                RightDown = 0x00000008,
                RightUp = 0x00000010
            }

            [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool SetCursorPos(int x, int y);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool GetCursorPos(out MousePoint lpMousePoint);

            [DllImport("user32.dll")]
            private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

            public static void SetCursorPosition(int x, int y)
            {
                SetCursorPos(x, y);
            }

            public static void SetCursorPosition(MousePoint point)
            {
                SetCursorPos(point.X, point.Y);
            }

            public static MousePoint GetCursorPosition()
            {
                MousePoint currentMousePoint;
                var gotPoint = GetCursorPos(out currentMousePoint);
                if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
                return currentMousePoint;
            }

            public static void MouseEvent(MouseEventFlags value)
            {
                MousePoint position = GetCursorPosition();

                mouse_event
                    ((int)value,
                        position.X,
                        position.Y,
                        0,
                        0)
                    ;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct MousePoint
            {
                public int X;
                public int Y;

                public MousePoint(int x, int y)
                {
                    X = x;
                    Y = y;
                }
            }
        }