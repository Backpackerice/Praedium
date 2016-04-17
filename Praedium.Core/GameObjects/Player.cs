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
        private bool selected;

        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
                if(renderer != null)
                {
                    if(value)
                    {
                        renderer.Character = new Character(renderer.Character.Code, Colors.LightBlue, renderer.Character.BackColor);
                    }
                    else
                    {
                        renderer.Character = new Character(renderer.Character.Code, TermColor.White, renderer.Character.BackColor);
                    }
                }

            }
        }

        public Stack<Vector2D> MovementPath
        {
            get;
            set;
        }

        protected override void OnStart()
        {
            Name = "Player";

            MovementPath = new Stack<Vector2D>();

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
