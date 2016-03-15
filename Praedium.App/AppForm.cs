using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Malison.Core;
using Malison.WinForms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Praedium.Core;
using Praedium.UI;

namespace Praedium.App
{
    public partial class AppForm : TerminalForm
    {
        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PeekMessage(out NativeMessage message, IntPtr window, uint filterMin, uint filterMax, uint remove);

        NativeMessage message;
        Game _game;

        public AppForm()
        {
            InitializeComponent();
            _game = new Game(Terminal);

            _game.Setup();

            Application.Idle += Application_Idle;
            TerminalControl.KeyDown += TerminalControl_KeyDown;
            TerminalControl.KeyUp += TerminalControl_KeyUp;
        }
        
        void TerminalControl_KeyUp(object sender, KeyEventArgs e)
        {
            KeyInfo info;

            IKeyParser parser = new WindowsKeyParser(e);

            if (parser.Parse(out info))
            {
                info.Down = false;
                _game.HandleInput(info);
            }
        }

        void TerminalControl_KeyDown(object sender, KeyEventArgs e)
        {
            KeyInfo info;

            IKeyParser parser = new WindowsKeyParser(e);

            if (parser.Parse(out info))
            {
                info.Down = true;
                _game.HandleInput(info);
            }
        }

        void Application_Idle(object sender, EventArgs e)
        {
            while (!PeekMessage(out message, IntPtr.Zero, 0, 0, 0))
            {
                _game.Tick();
            }
        }
    }
}
