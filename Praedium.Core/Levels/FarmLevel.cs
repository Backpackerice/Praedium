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

            Size = new Vector2D(map.Width, map.Height);

            TmxTileset tileset = map.Tilesets[0];

            foreach (var layer in map.Layers)
            {
                int zIndex = int.Parse(layer.Properties["ZIndex"]);

                Tile[] tiles = layer.Tiles.Where(x => x.Gid > 0).Select(x => GetTile(x, tileset)).ToArray();

                AddTileLayer(new TileLayer(zIndex, tiles));
            }

            foreach(var field in map.ObjectGroups[0].Objects.Where(x => x.Name == "Field Block"))
            {
                int width = (int)field.Width / map.TileWidth;
                int height = (int)field.Height / map.TileHeight;
                int x = (int)field.X / map.TileWidth;
                int y = (int)field.Y / map.TileHeight;

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        FieldTile tile = new FieldTile();
                        tile.Position = new Vector2D(x + i, y + j);
                        AddGameObject(tile);
                    }
                }
            }

            var spawn = map.ObjectGroups[0].Objects["Spawn"];

            Farmer player = new Farmer();
            player.Position = new Vector2D((int)(spawn.X / map.TileWidth), (int)(spawn.Y / map.TileHeight));

            AddGameObject(player);
            AddGameObject(new MouseController());
            AddGameObject(new CameraController());

            Game.CenterViewTo(player.Position);
        }

        protected override void OnUnload()
        {
            throw new NotImplementedException();
        }
    }
}
