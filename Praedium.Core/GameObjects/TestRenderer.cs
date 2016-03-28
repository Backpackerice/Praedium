using Malison.Core;
using Praedium.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praedium.Core.GameObjects
{
    public class TestRenderer : GameObject
    {
        protected override void OnStart()
        {
        }

        public override void Render(Malison.Core.ITerminal terminal)
        {
            Glyph[] glyphs = (Glyph[])Enum.GetValues(typeof(Glyph));

            TermColor[] colors = TermColor.Analogous(TermColor.Blue.Lighter(0.2), 16).ToArray();

            for (int i = terminal.Size.X / 4; i < terminal.Size.X / 2 + terminal.Size.X / 4; i++)
            {
                for (int j = (terminal.Size.Y + 1) / 4; j < terminal.Size.Y / 2 + terminal.Size.Y / 4; j++)
                {
                    //Random character with random foreground and background colors
                    terminal[i, j][colors[Game.RNG.Next(0, colors.Length)], colors[Game.RNG.Next(0, colors.Length)]].Write((int)glyphs[Game.RNG.Next(0, glyphs.Length)]);
                }
            }
        }
    }
}
