using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bramble.Core;
using Malison.Core;
using Praedium.Core.Attributes;
using Praedium.Core.Components;

namespace Praedium.Core
{
    [RequireComponent(typeof(TestMoveComponent))]
    public class Player : GameObject
    {
        public TermColor ForeColor;

        public override void Render(Malison.Core.ITerminal terminal)
        {
            terminal[Position][ForeColor, TermColor.Black].Write(Glyph.At);
        }

        protected override void OnStart()
        {
            Position = Vector2D.Zero;
            ForeColor = TermColor.White;
        }
    }
}
