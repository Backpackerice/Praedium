using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bramble.Core;

namespace Praedium.Engine
{
    /// <summary>
    /// A Level is a scene that contains game objects and is composed of many tile layers.
    /// </summary>
    public abstract class Level
    {
        public SortedList<int, TileLayer> Layers = new SortedList<int,TileLayer>();

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
