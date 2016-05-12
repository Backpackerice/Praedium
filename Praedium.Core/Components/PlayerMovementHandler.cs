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
using SFML.Window;

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

            Game.Window.MouseButtonPressed += Window_MouseButtonPressed;
        }

        void Window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if(player.Selected && e.Button == Mouse.Button.Right)
            {
                targetPosition = Game.ToWorldPosition(Game.WindowPositionToViewportPosition(new Vector2D(e.X, e.Y)));

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
