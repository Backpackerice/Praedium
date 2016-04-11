using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praedium.Engine.UI;
using System.Windows.Forms;
using Malison.WinForms;

namespace Praedium.App
{
    public class WindowsMouseParser : IMouseParser
    {
        public MouseEventArgs ReceivedEventArgs
        {
            get;
            private set;
        }

        private TerminalControl control;

        public WindowsMouseParser(MouseEventArgs e, TerminalControl ctrl)
        {
            ReceivedEventArgs = e;
            control = ctrl;
        }

        public bool Parse(out MouseInfo info)
        {
            bool recognized = true;

            info = new MouseInfo();
            
            info.Position = new Bramble.Core.Vector2D((int)(ReceivedEventArgs.X * 1f / control.Width * control.Terminal.Size.X), (int)(ReceivedEventArgs.Y * 1f / control.Height * control.Terminal.Size.Y));

            switch(ReceivedEventArgs.Button)
            {
                case MouseButtons.Left:
                    info.Button = MouseButton.Left;
                    break;
                case MouseButtons.Middle:
                    info.Button = MouseButton.Middle;
                    break;
                case MouseButtons.Right:
                    info.Button = MouseButton.Right;
                    break;
                case MouseButtons.None:
                    info.Button = MouseButton.None;
                    break;
                default:
                    recognized = false;
                    break;
            }

            return recognized;
        }
    }
}
