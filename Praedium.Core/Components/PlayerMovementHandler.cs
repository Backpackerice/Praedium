using Bramble.Core;
using Malison.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praedium.Engine.UI;
using Praedium.Engine.Components;

namespace Praedium.Core.Components
{
    public class PlayerMovementHandler : Component
    {
        // Limit movement speed to 10 tiles per second
        private const double LAG = 0.1f;

        private double elaspedTime;

        protected override void OnStart()
        { }

        public override void Update()
        {
            if(elaspedTime < LAG)
                elaspedTime += Game.DeltaTime;

            if(elaspedTime > LAG)
            {
                elaspedTime = 0;
                Vector2D movement = Vector2D.Zero;

                if(Game.UI.IsKeyDown(Key.A))
                    movement += new Vector2D(-1, 0);

                if(Game.UI.IsKeyDown(Key.D))
                    movement += new Vector2D(1, 0);

                if(Game.UI.IsKeyDown(Key.W))
                    movement += new Vector2D(0, -1);

                if(Game.UI.IsKeyDown(Key.S))
                    movement += new Vector2D(0, 1);
                
                if(movement != Vector2D.Zero)
                {
                    if(!Game.TileCollideable(GameObject.Position + movement))
                    {
                        GameObject.Position += movement;
                        Game.MoveViewBy(movement);
                    }
                }
            }
        }
    }
}
