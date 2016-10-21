using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;

namespace PointApp.Helper
{

    public class MouseAndKeyControl
    {
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002; //模拟鼠标左键按下
        private const int MOUSEEVENTF_LEFTUP = 0x0004; //模拟鼠标左键抬起 
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008; //模拟鼠标右键按下 
        private const int MOUSEEVENTF_RIGHTUP = 0x0010; //模拟鼠标右键抬起



        private int mouseSpeed = 0;
        public int MouseSpeed
        {
            get
            {
                return mouseSpeed;
            }
            set
            {
                mouseSpeed = value;
            }
        }
        private System.Timers.Timer timer = new System.Timers.Timer();

        //调用系统函数 将鼠标移动到相应位置
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        public extern static bool SetCursorPos(int x, int y);

        //调用系统函数 鼠标事件函数
        [DllImport("user32", EntryPoint = "mouse_event")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        public MouseAndKeyControl()
        {
            timer.Interval = 10;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Point nowPoint = GetMousePoint();
            int targetX = nowPoint.X;
            int targetY = nowPoint.Y;
            bool isMove = false;
            if (nowPoint.X - targetPoint.X > mouseSpeed)
            {
                //SetCursorPos(nowPoint.X - 15, nowPoint.Y);
                targetX = nowPoint.X - mouseSpeed;
                isMove = true;
            }

            else if (nowPoint.X - targetPoint.X < mouseSpeed * -1)
            {
                //SetCursorPos(nowPoint.X + 15, nowPoint.Y);
                targetX = nowPoint.X + mouseSpeed;
                isMove = true;
            }

            if (nowPoint.Y - targetPoint.Y > mouseSpeed)
            {
                targetY = nowPoint.Y - mouseSpeed;
                isMove = true;
            }
            if (nowPoint.Y - targetPoint.Y < mouseSpeed * -1)
            {
                targetY = nowPoint.Y + mouseSpeed;
                isMove = true;
            }

            if (isMove)
            {
                SetCursorPos(targetX, targetY);
            }
            else
            {
                timer.Stop();
            }
        }

        public Point GetMousePoint()
        {
            Point screenPoint = Control.MousePosition;//鼠标相对于屏幕左上角的坐标
            return screenPoint;
        }


        private Point targetPoint = new Point();
        public void MouseMove(int x, int y)
        {
            //targetPoint = new Point(x, y);
            //timer.Start();
            SetCursorPos(x, y);
        }

        public void StopControl()
        {
            timer.Stop();
        }

        static public void MouseClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public void MouseDown()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        }

        public void MouseClick(int x, int y)
        {
            MouseMove(x, y);
            MouseClick();
        }

        public void MouseDoubleClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public void MouseDoubleClick(int x, int y)
        {
            MouseMove(x, y);
            MouseDoubleClick();
        }

        public void reduceOper(int x, int y)
        {
            SetCursorPos(x, y);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        
    }
}
