using Bramble.Core;
using Malison.Core;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praedium.Engine.Components
{
    /// <summary>
    /// Basic Renderer which renders a single Character in it's GameObject's position.
    /// You can set the Offset value to alter the positioning.
    /// </summary>
    public class CellRenderer : Renderer
    {
        public Character Character
        {
            get;
            set;
        }

        public Vector2D Position
        {
            get;
            set;
        }

        public override void Render()
        {
            if(Units == Components.Units.World)
            {
                if (Game.ViewPort.Contains(GameObject.Position + Offset))
                {
                    GlyphSheet.Draw(Window, GameObject.Position.X + Offset.X - Game.ViewPortOffset.X, GameObject.Position.Y + Offset.Y - Game.ViewPortOffset.Y, Character);
                }
            }
            else
            {
                GlyphSheet.Draw(Window, GameObject.Position.X, GameObject.Position.Y, Character);
            }
        }

        protected override void OnStart()
        { }

        public override void Update()
        { }
    }
}
