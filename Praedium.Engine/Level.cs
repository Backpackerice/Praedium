using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bramble.Core;
using Malison.Core;

namespace Praedium.Engine
{
    /// <summary>
    /// A Level is a scene that contains game objects and is composed of many tile layers.
    /// </summary>
    public abstract class Level
    {
        public SortedList<int, TileLayer> Layers = new SortedList<int,TileLayer>();

        private Vector2D size;
        private Array2D<bool> movementMap;

        public Vector2D Size
        {
            get
            {
                return size;
            }
            protected set
            {
                movementMap = new Array2D<bool>(value);
                size = value;
            }

        }

        public Game Game
        {
            get;
            set;
        }

        /// <summary>
        /// Rendering a level means rendering all tiles from all tilemaps
        /// </summary>
        public void Render(Malison.Core.ITerminal terminal)
        {
            foreach (var layer in Layers.Values)
            {
                foreach (var tile in layer.Tiles)
                {
                    if(Game.ViewPort.Contains(tile.Position))
                    {
                        terminal[tile.Position - Game.ViewPortOffset].Write(tile.Character);
                    }
                }
            }
        }

        public void AddGameObject(GameObject obj)
        {
            Game.AddGameObject(obj);
        }

        public void AddTileLayer(TileLayer layer)
        {
            Layers.Add(layer.ZIndex, layer);
            foreach (var tile in layer.Tiles)
            {
                if (tile.Collideable)
                    movementMap[tile.Position.X, tile.Position.Y] = true;
            }
        }

        public Path<Vector2D> FindPath(Vector2D start, Vector2D end)
        {
            return Path<Vector2D>.FindPath(start, end, GetDistance, GetEstimateCost, GetNeighbours);
        }

        public void Load()
        {
            OnLoad();
        }

        public void Unload()
        {
            OnUnload();
        }

        private double GetDistance(Vector2D a, Vector2D b)
        {
            return (a - b).Length;
        }

        private IEnumerable<Vector2D> GetNeighbours(Vector2D tile)
        {
            var levelRect = new Rect(0, 0, Size);

            var neighbourRect = levelRect.Intersect(new Rect(tile - new Vector2D(1, 1), new Vector2D(3, 3)));

            foreach(var vector in neighbourRect)
            {
                if (vector != tile && !movementMap[vector])
                    yield return vector;
            }
        }

        private double GetEstimateCost(Vector2D a, Vector2D b)
        {
            return GetDistance(a, b);
        }

        protected abstract void OnLoad();

        protected abstract void OnUnload();
    }
}
