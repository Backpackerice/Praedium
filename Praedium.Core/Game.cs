using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Malison.Core;

namespace Praedium.Core
{
    public class Game
    {
        private List<GameObject> _entities;

        private TimeSpan _accumulatedTime;
        private TimeSpan _lastTime;
        private Stopwatch _stopWatch;

        private bool _keyDataChanged;

        public KeyInfo CurrentKeyInfo
        {
            get;
            private set;
        }

        public readonly TimeSpan TargetElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 60);
        public readonly TimeSpan MaxElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 10);

        public bool DebugKeys
        {
            get;
            set;
        }        

        Random random; //Just for the tests

        public ITerminal Terminal
        {
            get;
            private set;
        }

        public Game(ITerminal terminal)
        {
            _stopWatch = Stopwatch.StartNew();
            Terminal = terminal;
            random = new Random();
            _entities = new List<GameObject>();
        }

        public double DeltaTime
        {
            get
            {
                return TargetElapsedTime.TotalSeconds;
            }
        }

        public void Tick()
        {
            TimeSpan currentTime = _stopWatch.Elapsed;
            TimeSpan elapsedTime = currentTime - _lastTime;
            _lastTime = currentTime;

            if (elapsedTime > MaxElapsedTime)
            {
                elapsedTime = MaxElapsedTime;
            }

            _accumulatedTime += elapsedTime;

            if(_keyDataChanged)
                HandleInput();

            while (_accumulatedTime >= TargetElapsedTime)
            {
                Update();

                Render();

                _accumulatedTime -= TargetElapsedTime;
            }
        }

        public void HandleInput()
        {
            if (CurrentKeyInfo.Key == Key.F1 && CurrentKeyInfo.Control && !CurrentKeyInfo.Down)
                DebugKeys = !DebugKeys;

            _keyDataChanged = false;
        }

        public void ApplyKeyData(KeyInfo info)
        {
            if (!(info.Equals(CurrentKeyInfo)) || info.Down)
            {
                CurrentKeyInfo = info;
                _keyDataChanged = true;
            }
        }

        private void Update()
        {
            foreach(var entity in _entities)
            {
                foreach(var component in entity.Components)
                {
                    component.Update();
                }
            }
        }

        private void Render()
        {
            Terminal.Clear();

            foreach(var entity in _entities)
            {
                entity.Render(Terminal);
            }

            TestRender();

            if(DebugKeys)
                RenderKeys();
        }

        private void RenderKeys()
        {
            if (CurrentKeyInfo.Down)
            {
                Terminal[0, 0].Write(CurrentKeyInfo.Key.ToString());

                List<string> line = new List<string>();

                if (CurrentKeyInfo.Control)
                {
                    line.Add("CTRL");
                }
                if (CurrentKeyInfo.Shift)
                {
                    line.Add("SHIFT");
                }
                if (CurrentKeyInfo.Alt)
                {
                    line.Add("ALT");
                }

                Terminal[0, 1].Write(string.Join("+", line.ToArray()));
            }
            Terminal[0, Terminal.Size.Y - 1][TermColor.Red].Write("KEYS DEBUG ON");
        }

        private void TestRender()
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

            Terminal[Terminal.Size.X / 4, 3, Terminal.Size.X - Terminal.Size.X/2, 3][TermColor.LightGreen].DrawBox(DrawBoxOptions.ContinueLines | DrawBoxOptions.DoubleLines);
            Terminal[Terminal.Size.X / 4 + 1, 4].Write("Press CTRL+F1 to turn on keys debugger");

            for (int i = Terminal.Size.X / 4; i < Terminal.Size.X / 2 + Terminal.Size.X / 4; i++)
            {
                for (int j = (Terminal.Size.Y + 1) / 4; j < Terminal.Size.Y / 2 + Terminal.Size.Y / 4; j++)
                {
                    //Random character with random foreground and background colors
                    Terminal[i, j][colors[random.Next(0, colors.Length)], colors[random.Next(0, colors.Length)]].Write(glyphs[random.Next(0, glyphs.Length)]);
                }
            }
        }
    }
}
