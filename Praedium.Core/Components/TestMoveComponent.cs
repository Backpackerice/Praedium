using Bramble.Core;
using Malison.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praedium.UI;

namespace Praedium.Core.Components
{
    public class TestMoveComponent : Component
    {
        private const double SPEED = 0.1f;

        private double elaspedTime;
        private Player player;

        protected override void OnStart()
        {
            player = GameObject as Player;
        }

        public override void Update()
        {
            elaspedTime += Game.DeltaTime;

            if(elaspedTime > SPEED)
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

                if(Game.UI.IsKeyDown(Key.R))
                    GameObject.Position = Vector2D.Zero;

                if(movement != Vector2D.Zero)
                {
                    TermColor[] colors = (TermColor[])Enum.GetValues(typeof(TermColor));
                    player.ForeColor = colors[Game.RNG.Next(0, colors.Length - 1)];

                    GameObject.Position += movement;
                }
            }
        }
    }
}
