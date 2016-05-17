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
    public class PathRenderer : Renderer
    {
        public IEnumerable<Vector2D> Nodes
        {
            get;
            set;
        }

        public Character Character
        {
            get;
            set;
        }

        public override void Render()
        {
            foreach (var node in Nodes)
            {
                if (Units == Components.Units.Viewport)
                {
                    GlyphSheet.Draw(Window, node.X + Offset.X, node.Y + Offset.Y, Character);
                }
                else
                {
                    if(Game.ViewPort.Contains(node + Offset))
                    {
                        GlyphSheet.Draw(Window, node.X + Offset.X - Game.ViewPortOffset.X, node.Y + Offset.Y - Game.ViewPortOffset.Y, Character);
                    }
                }
            }
        }

        protected override void OnStart()
        {
            if (Nodes == null)
                Nodes = new List<Vector2D>();
        }

        public override void Update()
        { }
    }
}
