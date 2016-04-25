using Bramble.Core;
using Malison.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praedium.Engine.UI;
using Praedium.Engine.Components;
using Praedium.Core.GameObjects;

namespace Praedium.Core.Components
{
    public class PlayerMovementHandler : Component
    {
        // Limit movement speed to 5 tiles per second
        private const double LAG = 0.2f;

        private double elaspedTime;

        private Farmer player;

        private bool processingMovement = false;

        private Vector2D targetPosition;

        protected override void OnStart()
        {
            player = (Farmer)GameObject;

            Game.MouseDown += Game_MouseDown;
        }

        void Game_MouseDown(object sender, MouseInfoEventArgs e)
        {
            if(player.Selected && e.MouseInfo.Button == MouseButton.Right)
            {
                targetPosition = Game.ToWorldPosition(e.MouseInfo.Position);

                var path = Game.Level.FindPath(player.Position, targetPosition);

                if (path != null)
                {
                    player.MovementPath.Clear();

                    foreach (var step in path.PreviousSteps)
                    {
                        player.MovementPath.Push(step);
                    }

                    processingMovement = true;
                }
            }
        }

        public override void Update()
        {
            if(processingMovement)
            {
                if (player.Position == targetPosition || player.MovementPath.Count == 0)
                {
                    processingMovement = false;
                    return;
                }

                if (elaspedTime < LAG)
                    elaspedTime += Game.DeltaTime;

                if (elaspedTime > LAG)
                {
                    elaspedTime -= LAG;

                    GameObject.Position = player.MovementPath.Pop();
                }
            }
        }
    }
}
