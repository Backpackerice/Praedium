using Bramble.Core;
using Malison.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praedium.Core.Components
{
    public class TestMoveComponent : Component
    {
        private double elaspedTime = 0f;
        private double speed = 0.1f;

        protected override void OnStart()
        {
            Game.KeyDown += Game_KeyDown;
        }

        void Game_KeyDown(object sender, UI.KeyInfoEventArgs e)
        {
            if(elaspedTime > speed)
            {
                elaspedTime = 0;
                Vector2D movement;
                TermColor[] colors = (TermColor[])Enum.GetValues(typeof(TermColor));

                (GameObject as Player).ForeColor = colors[Game.RNG.Next(0, colors.Length - 1)];

                switch (e.KeyInfo.Key)
                {
                    case UI.Key.A:
                        movement = new Vector2D(-1, 0);
                        break;
                    case UI.Key.D:
                        movement = new Vector2D(1, 0);
                        break;
                    case UI.Key.W:
                        movement = new Vector2D(0, -1);
                        break;
                    case UI.Key.S:
                        movement = new Vector2D(0, 1);
                        break;
                    case UI.Key.R:
                        GameObject.Position = Vector2D.Zero;
                        movement = Vector2D.Zero;
                        break;
                    default:
                        movement = Vector2D.Zero;
                        break;
                }
                GameObject.Position = GameObject.Position + movement;
            }
        }

        public override void Update()
        {
            elaspedTime += Game.DeltaTime;
        }
    }
}
