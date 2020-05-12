using System;
using System.Runtime.InteropServices;
using System.Timers;

namespace ClarksContinuousClicker
{
    public class VirtualMouse
    {
        public double Interval;
        public MouseButton Button;
        public Vector2d Position;
        public bool Enabled { get; private set; }
        private Timer _timer = new Timer();
        public delegate void ActionHandler(object sender, EventArgs e);
        public event ActionHandler OnAction; 

        [DllImport("User32.dll")]
        private static extern void mouse_event(MouseEvent me, uint x, uint y, uint data, UIntPtr extraData);

        private enum MouseEvent
        {
            MOUSEEVENTF_ABSOLUTE = 0x8000,
            MOUSEEVENTF_LEFTDOWN = 0x0002,
            MOUSEEVENTF_LEFTUP = 0x0004,
            MOUSEEVENTF_MIDDLEDOWN = 0x0020,
            MOUSEEVENTF_MIDDLEUP = 0x0040,
            MOUSEEVENTF_MOVE = 0x0001,
            MOUSEEVENTF_RIGHTDOWN = 0x0008,
            MOUSEEVENTF_RIGHTUP = 0x0010,
            MOUSEEVENTF_XDOWN = 0x0080,
            MOUSEEVENTF_XUP = 0x0100,
            MOUSEEVENTF_WHEEL = 0x0800,
            MOUSEEVENTF_HWHEEL = 0x01000
        }

        public VirtualMouse()
        {
            _timer.Elapsed += (object sender, ElapsedEventArgs e) =>
            {
                MouseEvent[] mouseEvent = new MouseEvent[2];

                switch (Button)
                {
                    case MouseButton.LEFT:
                        mouseEvent[0] = MouseEvent.MOUSEEVENTF_LEFTDOWN;
                        mouseEvent[1] = MouseEvent.MOUSEEVENTF_LEFTUP;
                        break;
                    case MouseButton.MIDDLE:
                        mouseEvent[0] = MouseEvent.MOUSEEVENTF_MIDDLEDOWN;
                        mouseEvent[1] = MouseEvent.MOUSEEVENTF_MIDDLEUP;
                        break;
                    case MouseButton.RIGHT:
                        mouseEvent[0] = MouseEvent.MOUSEEVENTF_RIGHTDOWN;
                        mouseEvent[1] = MouseEvent.MOUSEEVENTF_RIGHTUP;
                        break;
                }

                mouse_event(mouseEvent[0], (uint)this.Position.X, (uint)this.Position.Y, 0, UIntPtr.Zero);
                mouse_event(mouseEvent[1], (uint)this.Position.X, (uint)this.Position.Y, 0, UIntPtr.Zero);

                OnAction?.Invoke(this, new EventArgs());
            };
        }

        public void SetButton(MouseButton mouseButton)
        {
            Button = mouseButton;
        }

        public void Enable()
        {
            _timer.Interval = Interval;
            _timer.AutoReset = true;
            _timer.Start();
            this.Enabled = true;
        }

        public void Disable()
        {
            if (_timer.Enabled)
            {
                _timer.Stop();
                this.Enabled = false;
            }
        }
    }

    public enum MouseButton
    {
        LEFT,
        MIDDLE,
        RIGHT
    }

    public struct Vector2d
    {
        public int X;
        public int Y;
    }
}
