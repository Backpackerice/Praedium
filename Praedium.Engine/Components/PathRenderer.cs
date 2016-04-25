using Bramble.Core;
using Malison.Core;
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

        public override void Render(Malison.Core.ITerminal terminal)
        {
            if (!Enabled)
                return;

            foreach (var node in Nodes)
            {
                if (Units == Components.Units.Viewport)
                {
                    terminal[node].Write(Character);
                }
                else
                {
                    terminal[node - Game.ViewPortOffset].Write(Character);
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
