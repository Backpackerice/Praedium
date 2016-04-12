using Bramble.Core;
using Malison.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praedium.Engine.Components
{
    public enum Units
    {
        World,
        Viewport
    }

    public abstract class Renderer : Component
    {
        public Units Units
        {
            get;
            set;
        }

        public Vector2D Offset
        {
            get;
            set;
        }

        public abstract void Render(ITerminal terminal);
    }
}
