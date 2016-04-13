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

            Game.MouseDown += Game_MouseDown;
            Game.MouseUp += Game_MouseUp;
            Game.MouseMove += Game_MouseMove;
        }

        void Game_MouseMove(object sender, MouseInfoEventArgs e)
        {
            if(Controller.ProcessingSelection && lastPosition != e.MouseInfo.Position)
            {
                lastPosition = e.MouseInfo.Position;
                Controller.ChangeSelection(lastPosition);
            }
        }

        void Game_MouseUp(object sender, MouseInfoEventArgs e)
        {
            if (e.MouseInfo.Button == MouseButton.Left && Controller.ProcessingSelection)
            {
                Controller.EndSelection(e.MouseInfo.Position);
            }
        }

        void Game_MouseDown(object sender, MouseInfoEventArgs e)
        {
            if (e.MouseInfo.Button == MouseButton.Left && !Controller.ProcessingSelection)
            {
                Controller.StartSelection(e.MouseInfo.Position);
            }
        }

        public override void Update()
        { }
    }
}
