using Praedium.Engine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praedium.Engine.UI;
using Bramble.Core;
using Praedium.Core.GameObjects;
using SFML.Window;

namespace Praedium.Core.Components
{
    public enum Tool
    {
        Hoe = 0,
        WateringCan,
        Seeds
    }

    public class ToolHandler : Component
    {
        public Tool CurrentTool = Tool.Hoe;

        protected override void OnStart()
        {
            Game.Window.KeyPressed += Window_KeyPressed;
        }

        void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            if(e.Code == Keyboard.Key.E)
            {
                CurrentTool = (Tool)((((int)CurrentTool) + 1) % 3);
            }
        }

        public override void Update()
        {
            Vector2D offset = Vector2D.Zero;

            if(Game.UI.IsKeyDown(Keyboard.Key.Up))
            {
                offset = new Vector2D(0, -1);
            }
            else if (Game.UI.IsKeyDown(Keyboard.Key.Right))
            {
                offset = new Vector2D(1, 0);
            }
            else if (Game.UI.IsKeyDown(Keyboard.Key.Down))
            {
                offset = new Vector2D(0, 1);
            }
            else if (Game.UI.IsKeyDown(Keyboard.Key.Left))
            {
                offset = new Vector2D(-1, 0);
            }

            if(offset != Vector2D.Zero)
            {
                var targetTile = Game.GetObjectAt(GameObject.Position + offset) as FieldTile;

                if(targetTile != null)
                {
                    switch(CurrentTool)
                    {
                        case Tool.Hoe:
                            targetTile.Till();
                            break;
                        case Tool.WateringCan:
                            targetTile.Water();
                            break;
                        case Tool.Seeds:
                            targetTile.Seed();
                            break;
                    }
                }
            }
        }
    }
}
