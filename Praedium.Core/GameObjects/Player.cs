using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bramble.Core;
using Malison.Core;
using Praedium.Engine.Components;
using Praedium.Core.Components;
using Praedium.Engine;

namespace Praedium.Core.GameObjects
{
    [RequireComponent(typeof(PlayerMovementHandler))]
    public class Player : GameObject
    {
        public override void Render(Malison.Core.ITerminal terminal)
        {
            terminal[Position - Game.ViewPortOffset][TermColor.White, TermColor.Black].Write((int)Glyph.CharacterInversed);
        }

        protected override void OnStart()
        {
        }
    }
}
