using Bramble.Core;
using Praedium.Engine.Components;
using Praedium.Engine.UI;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praedium.Core.Components
{
    public class CameraMovementHandler : Component
    {        
        // Limit camera movement speed to 10 tiles per second
        public double LAG = 0.1f;

        private double elaspedTime;

        protected override void OnStart()
        { }

        public override void Update()
        {
            elaspedTime += Game.DeltaTime;

            if (elaspedTime >= LAG)
            {
                elaspedTime = 0;
                Vector2D movement = Vector2D.Zero;
                if (Game.UI.IsKeyDown(Keyboard.Key.W))
                    movement += new Vector2D(0, -1);
                if (Game.UI.IsKeyDown(Keyboard.Key.A))
                    movement += new Vector2D(-1, 0);
                if (Game.UI.IsKeyDown(Keyboard.Key.S))
                    movement += new Vector2D(0, 1);
                if (Game.UI.IsKeyDown(Keyboard.Key.D))
                    movement += new Vector2D(1, 0);

                if (movement != Vector2D.Zero)
                    Game.MoveViewBy(movement);
            }
        }
    }
}
