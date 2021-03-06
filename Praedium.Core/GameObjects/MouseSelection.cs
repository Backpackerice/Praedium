﻿using Bramble.Core;
using Praedium.Engine;
using Praedium.Engine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praedium.Core.GameObjects
{
    [RequireComponent(typeof(RectRenderer))]
    public class MouseSelection : GameObject
    {
        private RectRenderer renderer;
        private Rect rectangle;

        public Rect Rectangle
        {
            get
            {
                return rectangle;
            }
            set
            {
                rectangle = value;
                if (renderer != null)
                {
                    renderer.Position = rectangle.Position;
                    renderer.Size = rectangle.Size;
                }
            }
        }

        public List<GameObject> SelectedObjects
        {
            get;
            private set;
        }

        protected override void OnStart()
        {
            Rectangle = new Rect();
            SelectedObjects = new List<GameObject>();
            renderer = GetComponent<RectRenderer>() as RectRenderer;
            renderer.Units = Units.Viewport;
            renderer.Foreground = new Malison.Core.TermColor(0, 0, 255, 100);

            foreach (var item in Game.GetObjectsByName("Player"))
            {
                (item as Farmer).Selected = false;
            }
        }

        public void Commit()
        {
            SelectedObjects = Game.GetObjectsWithin(new Rect(Game.ToWorldPosition(Rectangle.Position), Rectangle.Size)).OfType<Farmer>().ToList<GameObject>();

            foreach (var item in SelectedObjects)
            {
                (item as Farmer).Selected = true;
            }

            renderer.Enabled = false;
        }

        protected override void OnDestroy()
        {
            foreach (var item in SelectedObjects)
            {
                (item as Farmer).Selected = false;
            }
        }
    }
}
