using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Malison.Core;
using Praedium.Engine.UI;
using Bramble.Core;
using Praedium.Engine.Components;

namespace Praedium.Engine
{
    public class Game
    {
        private List<GameObject> entities;

        private TimeSpan accumulatedTime;
        private TimeSpan lastTime;
        private Stopwatch stopWatch;
        private ITerminal terminal;

        public UserInterface UI
        {
            get;
            private set;
        }

        public readonly TimeSpan TargetElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 60);
        public readonly TimeSpan MaxElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 10);

        public event EventHandler<KeyInfoEventArgs> KeyUp;
        public event EventHandler<KeyInfoEventArgs> KeyDown;        

        // TODO: Might need to replace with better random number generator
        public Random RNG
        {
            get;
            private set;
        }

        public ITerminal Terminal
        {
            get
            {
                return terminal;
            }
            set
            {
                // If previous terminal instance existed, resize the viewport to scale nicely within the window
                if(terminal != null)
                    ViewPort = new Rect(ViewPortOffset + (terminal.Size / 2 - value.Size / 2), value.Size);
                terminal = value;
            }
        }

        public Vector2D ViewPortOffset
        {
            get
            {
                return ViewPort.Position;
            }
        }

        public Rect ViewPort
        {
            get;
            private set;
        }

        public Level Level
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

        public void LoadLevel(Level targetLevel)
        {
            if(Level != null)
                Level.Unload();

            entities.Clear();

            targetLevel.Game = this;
            targetLevel.Load();

            Level = targetLevel;
        }

        public void CenterViewTo(Vector2D position)
        {
            ViewPort = new Rect(new Vector2D(position.X - Terminal.Size.X / 2, position.Y - Terminal.Size.Y / 2), Terminal.Size);
        }

        public void MoveViewBy(Vector2D distance)
        {
            ViewPort = new Rect(ViewPort.Position + distance, Terminal.Size);
        }

        public bool TileCollideable(Vector2D position)
        {
            foreach (var layer in Level.Layers.Values)
            {
                if (layer.Tiles[position.Y * 100 + position.X].Collideable)
                    return true;
            }

            return false;
        }

        public GameObject GetObjectAt(Vector2D position)
        {
            foreach(var obj in entities)
            {
                if (obj.Position == position)
                    return obj;
            }

            return null;
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

            bool updated = false;

            if (elapsedTime > MaxElapsedTime)
            {
                elapsedTime = MaxElapsedTime;
            }

            accumulatedTime += elapsedTime;

            while (accumulatedTime >= TargetElapsedTime)
            {
                Update();

                accumulatedTime -= TargetElapsedTime;
                updated = true;
            }

            if(updated)
                Render();
        }

        public void HandleInput(KeyInfo info)
        {
            UI.ApplyKeyInfo(info);

            if (info.Down)
                OnKeyDown(info);
            else
                OnKeyUp(info);
        }

        public void AddGameObject(GameObject gameObject)
        {
            entities.Add(gameObject);
            gameObject.Game = this;
            gameObject.Start();
        }

        private void OnKeyDown(KeyInfo info)
        {
            if (KeyDown != null)
            {
                KeyDown(this, new KeyInfoEventArgs(info));
            }
        }

        private void OnKeyUp(KeyInfo info)
        {
            if (KeyUp != null)
            {
                KeyUp(this, new KeyInfoEventArgs(info));
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

            Level.Render(Terminal);

            foreach(var gameObject in entities)
            {
                gameObject.Render(Terminal);
            }
        }
    }
}
