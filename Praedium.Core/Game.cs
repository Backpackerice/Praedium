using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Malison.Core;
using Praedium.UI;

namespace Praedium.Core
{
    public class Game
    {
        private List<GameObject> entities;

        private TimeSpan accumulatedTime;
        private TimeSpan lastTime;
        private Stopwatch stopWatch;

        public UserInterface UI
        {
            get;
            private set;
        }

        public KeyInfo CurrentKeyInfo
        {
            get;
            private set;
        }

        public readonly TimeSpan TargetElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 60);
        public readonly TimeSpan MaxElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 10);

        public event EventHandler<KeyInfoEventArgs> KeyUp;
        public event EventHandler<KeyInfoEventArgs> KeyDown;

        public bool DebugKeys
        {
            get;
            set;
        }

        // TODO: Might need to replace with better random number generator
        public Random RNG
        {
            get;
            private set;
        }

        public ITerminal Terminal
        {
            get;
            private set;
        }

        public Game(ITerminal terminal)
        {
            stopWatch = Stopwatch.StartNew();
            Terminal = terminal;
            RNG = new Random();
            UI = new UserInterface();
            entities = new List<GameObject>();
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
            TimeSpan currentTime = stopWatch.Elapsed;
            TimeSpan elapsedTime = currentTime - lastTime;
            lastTime = currentTime;

            if (elapsedTime > MaxElapsedTime)
            {
                elapsedTime = MaxElapsedTime;
            }

            accumulatedTime += elapsedTime;

            while (accumulatedTime >= TargetElapsedTime)
            {
                Update();

                accumulatedTime -= TargetElapsedTime;
            }

            Render();
        }

        public void HandleInput(KeyInfo info)
        {
            UI.ApplyKeyInfo(info);
        }

        public void Setup()
        {
            AddGameObject(new Player());

            foreach (var entity in entities)
            {
                entity.Start();
            }
        }

        public void AddGameObject(GameObject gameObject)
        {
            entities.Add(gameObject);
            gameObject.Game = this;
        }

        private void OnKeyDown()
        {
            if (KeyDown != null)
            {
                KeyDown(this, new KeyInfoEventArgs(CurrentKeyInfo));
            }
        }

        private void OnKeyUp()
        {
            if (KeyUp != null)
            {
                KeyUp(this, new KeyInfoEventArgs(CurrentKeyInfo));
            }
        }

        private void Update()
        {
            foreach (var gameObject in entities)
            {
                gameObject.Update();
            }
        }

        private void Render()
        {
            Terminal.Clear();

            TestRender();

            foreach(var entity in entities)
            {
                entity.Render(Terminal);
            }
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

            for (int i = Terminal.Size.X / 4; i < Terminal.Size.X / 2 + Terminal.Size.X / 4; i++)
            {
                for (int j = (Terminal.Size.Y + 1) / 4; j < Terminal.Size.Y / 2 + Terminal.Size.Y / 4; j++)
                {
                    //Random character with random foreground and background colors
                    Terminal[i, j][colors[RNG.Next(0, colors.Length)], colors[RNG.Next(0, colors.Length)]].Write(glyphs[RNG.Next(0, glyphs.Length)]);
                }
            }
        }
    }
}
