using Bramble.Core;
using Malison.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praedium.Engine.Components
{
    public class CellRenderer : Renderer
    {
        public Character Character
        {
            get;
            set;
        }

        public override void Render(ITerminal terminal)
        {
            if (Game.Viewport.Contains(GameObject.Position + Offset))
            {
                terminal[GameObject.Position + Offset - Game.ViewPortOffset].Write(Character);
            }
        }

        protected override void OnStart()
        { }

        public override void Update()
        { }
    }
}
