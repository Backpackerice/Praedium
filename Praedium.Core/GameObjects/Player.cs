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
    [RequireComponent(typeof(CellRenderer))]
    [RequireComponent(typeof(ToolHandler))]
    public class Player : GameObject
    {
        private CellRenderer renderer;

        protected override void OnStart()
        {
            Name = "Player";

            renderer = (CellRenderer)GetComponentOfType(typeof(CellRenderer));

            renderer.Character = new Character((int)Glyph.CharacterInversed, TermColor.White, TermColor.Black);

            renderer.Position = Position;
        }

        public override Vector2D Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                base.Position = value;
                if(renderer != null)
                    renderer.Position = value;
            }
        }
    }
}
