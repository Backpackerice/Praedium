using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praedium.Engine
{
    /// <summary>
    /// A structure representing a tile layer. It contains an array of tiles.
    /// Each tile map can have many tile layers, ordered by their Z indexes.
    /// </summary>
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
