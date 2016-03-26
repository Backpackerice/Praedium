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
    [RequireComponent(typeof(PlayerMovementHandler))]
    public class Player : GameObject
    {
        public override void Render(Malison.Core.ITerminal terminal)
        {
            terminal[Position][TermColor.White, TermColor.Black].Write('☻');
        }

        protected override void OnStart()
        {
            Position = Vector2D.Zero;
        }
    }
}
