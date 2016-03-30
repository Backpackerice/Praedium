using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bramble.Core;

namespace Praedium.Engine
{
    public abstract class Level
    {
        public SortedList<int, TileLayer> Layers = new SortedList<int,TileLayer>();

        public List<GameObject> Entities = new List<GameObject>();

        public Game Game
        {
            get;
            set;
        }

        public void Render(Malison.Core.ITerminal terminal)
        {
            foreach (var layer in Layers.Values)
            {
                foreach (var tile in layer.Tiles)
                {
                    if(Game.Viewport.Contains(tile.Position))
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
