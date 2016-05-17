using Malison.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praedium.Engine.Components
{
    public class TerminalRenderer : Renderer
    {
        public ITerminal Terminal
        {
            get;
            set;
        }

        public override void Render()
        {
            if (Terminal == null)
                return;

            if(Units == Components.Units.Viewport)
            {
                for (int i = 0; i < Terminal.Size.X; i++)
                {
                    for (int j = 0; j < Terminal.Size.Y; j++)
                    {
                        GlyphSheet.Draw(Window, i, j, Terminal.Get(i, j));
                    }
                }
            }
            else
            {
                for (int i = 0; i < Terminal.Size.X; i++)
                {
                    for (int j = 0; j < Terminal.Size.Y; j++)
                    {
                        if (Game.ViewPort.Contains(new Bramble.Core.Vector2D(i, j)))
                        {
                            GlyphSheet.Draw(Window, i, j, Terminal.Get(i, j));
                        }
                    }
                }
            }

            Terminal.Clear();
        }

        public override void Update()
        { }
    }
}
