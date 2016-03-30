using Bramble.Core;
using Malison.Core;
using Praedium.Core.GameObjects;
using Praedium.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace Praedium.Core.Levels
{
    public class FarmLevel : Level
    {
        private Tile GetTile(TmxLayerTile tile, TmxTileset tileset)
        {
            int code = int.Parse(tileset.Tiles[tile.Gid - 1].Properties["Code"]);
            TermColor foreground = TermColor.Parse(tileset.Tiles[tile.Gid - 1].Properties["Foreground"]);
            TermColor background = TermColor.Parse(tileset.Tiles[tile.Gid - 1].Properties["Background"]);

            Character character = new Character(code, foreground, background);
            Vector2D position = new Vector2D(tile.X, tile.Y);
            bool collideable = bool.Parse(tileset.Tiles[tile.Gid - 1].Properties["Collideable"]);

            return new Tile(character, position, collideable);
        }

        protected override void OnLoad()
        {
            TmxMap map = new TmxMap("Resources/farm.tmx");

            TmxTileset tileset = map.Tilesets[0];

            foreach (var layer in map.Layers)
            {
                int zIndex = 0;

                Tile[] tiles = layer.Tiles.Select(x => GetTile(x, tileset)).ToArray();

                AddTileLayer(new TileLayer(zIndex, tiles));
            }

            var spawn = map.ObjectGroups[0].Objects["Spawn"];

            Player player = new Player();
            player.Position = new Vector2D((int)(spawn.X / spawn.Width), (int)(spawn.Y / spawn.Height));

            AddGameObject(player);

            Game.CenterViewTo(player.Position);
        }

        protected override void OnUnload()
        {
            throw new NotImplementedException();
        }
    }
}
