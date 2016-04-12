using Bramble.Core;
using Praedium.Core.Components;
using Praedium.Engine;
using Praedium.Engine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praedium.Core.GameObjects
{
    [RequireComponent(typeof(MouseSelectionHandler))]
    [RequireComponent(typeof(BoxRenderer))]
    public class MouseController : GameObject
    {
        public bool ProcessingSelection
        {
            get;
            private set;
        }

        private Vector2D startPosition;
        private Vector2D endPosition;

        private BoxRenderer renderer;

        protected override void OnStart()
        {
            startPosition = endPosition = Vector2D.Zero;
            ProcessingSelection = false;

            renderer = GetComponentOfType(typeof(BoxRenderer)) as BoxRenderer;

            renderer.Size = Vector2D.Zero;
            renderer.Units = Units.Viewport;
        }

        public void StartSelection(Vector2D startPos)
        {
            startPosition = startPos;
            endPosition = startPos;
                        
            ProcessingSelection = true;
        }

        public void ChangeSelection(Vector2D targetPos)
        {
            endPosition = targetPos;

            renderer.Position = new Vector2D(Math.Min(startPosition.X, endPosition.X), Math.Min(startPosition.Y, endPosition.Y));
            renderer.Size = new Vector2D(Math.Abs(startPosition.X - endPosition.X) + 1, Math.Abs(startPosition.Y - endPosition.Y) + 1);
        }

        public void EndSelection(Vector2D endPos)
        {
            endPosition = endPos;
            renderer.Size = Vector2D.Zero;

            ProcessingSelection = false;
        }
    }
}
