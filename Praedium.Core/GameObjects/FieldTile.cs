using Malison.Core;
using Praedium.Engine;
using Praedium.Engine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praedium.Core.GameObjects
{
    [Flags]
    public enum FieldTileStatus
    {
        Default = 0,
        Tilled = 1,
        Seeded = 2,
        Watered = 4
    }

    [RequireComponent(typeof(CellRenderer))]
    public class FieldTile : GameObject
    {
        private CellRenderer renderer;

        public FieldTileStatus Status
        {
            get;
            set;
        }

        public bool Tilled
        {
            get
            {
                return Status.HasFlag(FieldTileStatus.Tilled);
            }
        }
        public bool Watered
        {
            get
            {
                return Status.HasFlag(FieldTileStatus.Watered);
            }
        }

        public override Bramble.Core.Vector2D Position
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

        protected override void OnStart()
        {
            Name = "FieldTile";

            renderer = (CellRenderer)GetComponentOfType(typeof(CellRenderer));

            renderer.Character = new Character((int)Glyph.Space, Colors.Soil, Colors.Soil);

            renderer.Position = Position;
        }

        public void Till()
        {
            if(!Status.HasFlag(FieldTileStatus.Tilled))
            {
                Status |= FieldTileStatus.Tilled;
                renderer.Character = new Character(renderer.Character.Code, renderer.Character.ForeColor, Colors.TilledSoil);
            }
        }

        public void Seed()
        {
            if(Status.HasFlag(FieldTileStatus.Tilled))
            {
                Status |= FieldTileStatus.Seeded;
                renderer.Character = new Character((int)Glyph.Bullet, TermColor.Parse("#1a140d"), renderer.Character.BackColor);
            }
        }

        public void Water()
        {
            if (Status.HasFlag(FieldTileStatus.Tilled))
            {
                Status |= FieldTileStatus.Watered;
                renderer.Character = new Character(renderer.Character.Code, renderer.Character.ForeColor, Colors.WateredSoil);
            }
        }
    }
}
