using Bramble.Core;
using Malison.SFML;
using SFML.Graphics;
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

        public RenderWindow Window
        {
            get
            {
                return Game.Window;
            }
        }

        public GlyphSheet GlyphSheet
        {
            get
            {
                return Game.GlyphSheet;
            }
        }

        public abstract void Render();
    }
}
