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

namespace Praedium.App
{
    public partial class AppForm : TerminalForm
    {
        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PeekMessage(out NativeMessage message, IntPtr window, uint filterMin, uint filterMax, uint remove);

        NativeMessage message;

        Stopwatch stopWatch; //For game loop time calculations

        readonly TimeSpan TargetElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 60);
        readonly TimeSpan MaxElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 10);

        TimeSpan accumulatedTime;
        TimeSpan lastTime;

        Random random; //Just for the tests

        public AppForm()
        {
            InitializeComponent();

            stopWatch = Stopwatch.StartNew();
            random = new Random();

            Application.Idle += Application_Idle;
        }

        void Application_Idle(object sender, EventArgs e)
        {
            while (!PeekMessage(out message, IntPtr.Zero, 0, 0, 0))
            {
                GameTick();
            }
        }

        private void GameTick()
        {
            TimeSpan currentTime = stopWatch.Elapsed;
            TimeSpan elapsedTime = currentTime - lastTime;
            lastTime = currentTime;

            if (elapsedTime > MaxElapsedTime)
            {
                elapsedTime = MaxElapsedTime;
            }

            accumulatedTime += elapsedTime;

            bool updated = false;

            while (accumulatedTime >= TargetElapsedTime)
            {
                //Handle Input
                //Render
                //Update game logic
                TestTerminal(); //But for now test the rendering

                accumulatedTime -= TargetElapsedTime;
                updated = true;
            }

            if (updated)
            {
                Invalidate();
            }
        }

        private void TestTerminal()
        {
            Glyph[] glyphs = new Glyph[] //Let's get some fancy symbols, all of them is a bit too big of a collection
            {
                Glyph.ArrowDown,
                Glyph.ArrowLeft,
                Glyph.ArrowRight,
                Glyph.ArrowUp,
                Glyph.Box,
                Glyph.Bullet,
                Glyph.Dark,
                Glyph.DarkFill,
                Glyph.Dashes,
                Glyph.Door,
                Glyph.Face,
                Glyph.Grass,
                Glyph.Gray,
                Glyph.GrayFill,
                Glyph.Hill,
                Glyph.HorizontalBars,
                Glyph.HorizontalBarsFill,
                Glyph.Light,
                Glyph.LightFill,
                Glyph.Mountains,
                Glyph.Solid,
                Glyph.SolidFill,
                Glyph.Tombstone,
                Glyph.TreeConical,
                Glyph.TreeDots,
                Glyph.TreeRound,
                Glyph.TriangleDown,
                Glyph.TriangleLeft,
                Glyph.TriangleRight,
                Glyph.TriangleUp,
                Glyph.TwoDots,
                Glyph.VerticalBars,
                Glyph.VerticalBarsFill
            };

            TermColor[] colors = (TermColor[])Enum.GetValues(typeof(TermColor));

            for (int i = 0; i < Terminal.Size.X; i++)
            {
                for (int j = 0; j < Terminal.Size.Y; j++)
                {
                    //Random character with random foreground and background colors
                    Terminal[i, j][colors[random.Next(0, colors.Length)], colors[random.Next(0, colors.Length)]].Write(glyphs[random.Next(0, glyphs.Length)]);
                }
            }
        }
    }
}