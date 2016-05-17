using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bramble.Core;
using Malison.Core;
using SFML.Graphics;

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
        private readonly double DIAGONAL_COST = Math.Sqrt(2);
        private readonly int STRAIGHT_COST = 1;

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
        public void Render()
        {
            foreach (var layer in Layers.Values)
            {
                foreach (var tile in layer.Tiles)
                {
                    if(Game.ViewPort.Contains(tile.Position))
                    {
                        Game.GlyphSheet.Draw(Game.Window, tile.Position.X - Game.ViewPortOffset.X, tile.Position.Y - Game.ViewPortOffset.Y, tile.Character);
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
            return Path<Vector2D>.FindPath(start, end, NeighbourDistance, Heuristic, GetNeighbours);
        }

        private double NeighbourDistance(Vector2D a, Vector2D b)
        {
            if (a.X != b.X && a.Y != b.Y)
                return DIAGONAL_COST;
            else
                return STRAIGHT_COST;
        }

        private double Heuristic(Vector2D a, Vector2D b)
        {
            var dx = Math.Abs(a.X - b.X);
            var dy = Math.Abs(a.Y - b.Y);
            return STRAIGHT_COST * (dx + dy) + (DIAGONAL_COST - 2 * STRAIGHT_COST) * Math.Min(dx, dy);
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

        public void Load()
        {
            OnLoad();
        }

        public void Unload()
        {
            OnUnload();
        }

        protected abstract void OnLoad();

        protected abstract void OnUnload();
    }
}
