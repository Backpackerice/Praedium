using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praedium.Engine
{
    public struct TileLayer
    {
        public int ZIndex;
        public Tile[] Tiles;

        public TileLayer(int zIndex, Tile[] tiles)
        {
            ZIndex = zIndex;
            Tiles = tiles;
        }
    }
}
