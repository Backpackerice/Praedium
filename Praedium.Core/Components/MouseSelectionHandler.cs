using Praedium.Core.GameObjects;
using Praedium.Engine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praedium.Engine.UI;
using Bramble.Core;
using SFML.Window;

namespace Praedium.Core.Components
{
    public class MouseSelectionHandler : Component
    {
        public MouseController Controller
        {
            get;
            private set;
        }

        private Vector2D lastPosition;

        protected override void OnStart()
        {
            Controller = (MouseController)GameObject;

            Game.Window.MouseButtonPressed += Window_MouseButtonPressed;
            Game.Window.MouseButtonReleased += Window_MouseButtonReleased;
            Game.Window.MouseMoved += Window_MouseMoved;
        }

        void Window_MouseMoved(object sender, MouseMoveEventArgs e)
        {
            var newPos = Game.WindowPositionToViewportPosition(new Vector2D(e.X, e.Y));
            if(Controller.ProcessingSelection && lastPosition != newPos)
            {
                lastPosition = newPos;
                Controller.ChangeSelection(lastPosition);
            }
        }

        void Window_MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left && Controller.ProcessingSelection)
            {
                Controller.EndSelection(Game.WindowPositionToViewportPosition(new Vector2D(e.X, e.Y)));
            }
        }

        void Window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left && !Controller.ProcessingSelection)
            {
                Controller.StartSelection(Game.WindowPositionToViewportPosition(new Vector2D(e.X, e.Y)));
            }
        }

        public override void Update()
        { }
    }
}
