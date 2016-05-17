using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Malison.Core;
using Malison.SFML;
using Bramble.Core;
using SFML.Graphics;
using SFML.Window;

namespace Praedium.Engine.Components
{
    public class RectRenderer : Renderer
    {
        private RectangleShape shape = new RectangleShape();
        private Vector2D size;

        public DrawBoxOptions Options
        {
            get;
            set;
        }

        public Vector2D Position
        {
            get;
            set;
        }

        public Vector2D Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
                shape.Size = new Vector2f(value.X * Game.GlyphSheet.Width, value.Y * Game.GlyphSheet.Height);
            }
        }

        public TermColor Foreground
        {
            get;
            set;
        }

        public override void Render()
        {
            if (Size.Length == 0)
                return;

            if(Units == Components.Units.World)
            {
                if(Game.ViewPort.Contains(new Rect(Game.ToViewportPosition(Position + Offset), Size)))
                {
                    shape.Position = new Vector2f(Position.X * Game.GlyphSheet.Width, Position.Y * Game.GlyphSheet.Height);
                    shape.FillColor = Foreground.ToSFMLColor();
                    Window.Draw(shape);
                }

            }
            else
            {
                shape.Position = new Vector2f(Position.X * Game.GlyphSheet.Width, Position.Y * Game.GlyphSheet.Height);
                shape.FillColor = Foreground.ToSFMLColor();
                Window.Draw(shape);
            }
        }

        public override void Update()
        { }
    }
}
