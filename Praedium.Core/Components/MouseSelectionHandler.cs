using Praedium.Core.GameObjects;
using Praedium.Engine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praedium.Engine.UI;
using Bramble.Core;

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
        }

        public override void Update()
        {
            if(Game.UI.IsMouseButtonDown(MouseButton.Left))
            {
                if(!Controller.ProcessingSelection)
                {
                    Controller.StartSelection(Game.UI.MousePosition);
                }
                else if(lastPosition != Game.UI.MousePosition)
                {
                    lastPosition = Game.UI.MousePosition;
                    Controller.ChangeSelection(Game.UI.MousePosition);
                }
            }
            else
            {
                if (Controller.ProcessingSelection)
                    Controller.EndSelection(Game.UI.MousePosition);
            }
        }
    }
}
