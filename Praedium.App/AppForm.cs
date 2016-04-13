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
using Praedium.Engine.UI;
using Praedium.Engine;
using Praedium.Core.GameObjects;
using Praedium.Core.Levels;

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
            Terminal = new Terminal(70, 40, Encoding.GetEncoding(437));
            TerminalControl.GlyphSheet = new GlyphSheet(Resources.cp437_16x16, 16, 16);
            TerminalControl.HideCursor = false;

            _game = new Game(Terminal);

            _game.LoadLevel(new FarmLevel());

            Application.Idle += Application_Idle;

            TerminalControl.KeyDown += TerminalControl_KeyDown;
            TerminalControl.KeyUp += TerminalControl_KeyUp;
            TerminalControl.MouseMove += TerminalControl_MouseMove;
            TerminalControl.MouseDown += TerminalControl_MouseDown;
            TerminalControl.MouseUp += TerminalControl_MouseUp;

            m8x8ToolStripMenuItem.Click += m8x8ToolStripMenuItem_Click;
            m10x10ToolStripMenuItem.Click += m10x10ToolStripMenuItem_Click;
            m12x12ToolStripMenuItem.Click += m12x12ToolStripMenuItem_Click;
            m14x14ToolStripMenuItem.Click += m14x14ToolStripMenuItem_Click;
            m16x16ToolStripMenuItem.Click += m16x16ToolStripMenuItem_Click;
            m18x18ToolStripMenuItem.Click += m18x18ToolStripMenuItem_Click;
            m20x20ToolStripMenuItem.Click += m20x20ToolStripMenuItem_Click;
            TerminalControl.Resize += TerminalControler_Resize;
        }

        void TerminalControl_MouseUp(object sender, MouseEventArgs e)
        {
            MouseInfo info;

            IMouseParser parser = new WindowsMouseParser(e, TerminalControl);

            if(parser.Parse(out info))
            {
                info.Down = false;
                info.Type = MouseEventType.ButtonUp;
                _game.HandleMouse(info);
            }
        }

        void TerminalControl_MouseDown(object sender, MouseEventArgs e)
        {
            MouseInfo info;

            IMouseParser parser = new WindowsMouseParser(e, TerminalControl);

            if (parser.Parse(out info))
            {
                info.Down = true;
                info.Type = MouseEventType.ButtonDown;
                _game.HandleMouse(info);
            }
        }

        void TerminalControl_MouseMove(object sender, MouseEventArgs e)
        {
            MouseInfo info;

            IMouseParser parser = new WindowsMouseParser(e, TerminalControl);

            if (parser.Parse(out info))
            {
                info.Type = MouseEventType.Move;
                _game.HandleMouse(info);
            }
        }

        void TerminalControler_Resize(object sender, EventArgs e)
        {
            int paddingX = TerminalControl.Size.Width % TerminalControl.GlyphSheet.Width;
            int paddingY = TerminalControl.Size.Width % TerminalControl.GlyphSheet.Height;

            int width = (TerminalControl.Size.Width - paddingX) / TerminalControl.GlyphSheet.Width;
            int height = (TerminalControl.Size.Height - paddingY) / TerminalControl.GlyphSheet.Height;

            Terminal = new Terminal(width, height, Encoding.GetEncoding(437));
            _game.Terminal = Terminal;
        }

        protected override void FontToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            m8x8ToolStripMenuItem.Checked = TerminalControl.GlyphSheet.Bitmap.Size == Resources.cp437_8x8.Size;
            m10x10ToolStripMenuItem.Checked = TerminalControl.GlyphSheet.Bitmap.Size == Resources.cp437_10x10.Size;
            m12x12ToolStripMenuItem.Checked = TerminalControl.GlyphSheet.Bitmap.Size == Resources.cp437_12x12.Size;
            m14x14ToolStripMenuItem.Checked = TerminalControl.GlyphSheet.Bitmap.Size == Resources.cp437_14x14.Size;
            m16x16ToolStripMenuItem.Checked = TerminalControl.GlyphSheet.Bitmap.Size == Resources.cp437_16x16.Size;
            m18x18ToolStripMenuItem.Checked = TerminalControl.GlyphSheet.Bitmap.Size == Resources.cp437_18x18.Size;
            m20x20ToolStripMenuItem.Checked = TerminalControl.GlyphSheet.Bitmap.Size == Resources.cp437_20x20.Size;
        }

        void m20x20ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TerminalControl.GlyphSheet = new GlyphSheet(Resources.cp437_20x20, 16, 16);
            TerminalControler_Resize(this, new EventArgs());
        }

        void m18x18ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TerminalControl.GlyphSheet = new GlyphSheet(Resources.cp437_18x18, 16, 16);
            TerminalControler_Resize(this, new EventArgs());
        }

        void m16x16ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TerminalControl.GlyphSheet = new GlyphSheet(Resources.cp437_16x16, 16, 16);
            TerminalControler_Resize(this, new EventArgs());
        }

        void m14x14ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TerminalControl.GlyphSheet = new GlyphSheet(Resources.cp437_14x14, 16, 16);
            TerminalControler_Resize(this, new EventArgs());
        }

        void m12x12ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TerminalControl.GlyphSheet = new GlyphSheet(Resources.cp437_12x12, 16, 16);
            TerminalControler_Resize(this, new EventArgs());
        }

        void m10x10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TerminalControl.GlyphSheet = new GlyphSheet(Resources.cp437_10x10, 16, 16);
            TerminalControler_Resize(this, new EventArgs());
        }

        void m8x8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TerminalControl.GlyphSheet = new GlyphSheet(Resources.cp437_8x8, 16, 16);
            TerminalControler_Resize(this, new EventArgs());
        }
        
        void TerminalControl_KeyUp(object sender, KeyEventArgs e)
        {
            KeyInfo info;

            IKeyParser parser = new WindowsKeyParser(e);

            if (parser.Parse(out info))
            {
                info.Down = false;
                _game.HandleKeyboard(info);
            }
        }

        void TerminalControl_KeyDown(object sender, KeyEventArgs e)
        {
            KeyInfo info;

            IKeyParser parser = new WindowsKeyParser(e);

            if (parser.Parse(out info))
            {
                info.Down = true;
                _game.HandleKeyboard(info);
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
