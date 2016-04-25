using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Malison.Core;
using Bramble.Core;

namespace Praedium.Engine.Components
{
    public class BoxRenderer : Renderer
    {
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
            get;
            set;
        }

        public TermColor Foreground
        {
            get;
            set;
        }

        public TermColor Background
        {
            get;
            set;
        }

        public override void Render(Malison.Core.ITerminal terminal)
        {
            if (!Enabled)
                return;

            if (Size.Length == 0)
                return;

            if(Units == Components.Units.World)
            {

            }
            else
            {
                terminal[Foreground, Background][Position, Size].DrawBox(Options);
            }
        }

        protected override void OnStart()
        {
            Foreground = TermColor.White;
            Background = TermColor.Black;
        }

        public override void Update()
        { }
    }
}
