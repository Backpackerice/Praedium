using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bramble.Core;
using Malison.Core;

namespace Praedium.Core
{
    public abstract class GameObject
    {
        public Vector2D Position
        {
            get;
            private set;
        }

        public List<Component> Components
        {
            get;
            private set;
        }

        public abstract void Render(ITerminal terminal);
    }
}
