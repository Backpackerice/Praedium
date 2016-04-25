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
    public class MouseController : GameObject
    {
        public bool ProcessingSelection
        {
            get;
            private set;
        }

        private Vector2D startPosition;
        private Vector2D endPosition;
        private MouseSelection selection;

        protected override void OnStart()
        {
            startPosition = endPosition = Vector2D.Zero;
            ProcessingSelection = false;
        }

        public void StartSelection(Vector2D startPos)
        {
            startPosition = startPos;
            endPosition = startPos;
                        
            ProcessingSelection = true;
            if (selection != null)
                selection.Destroy();

            selection = Game.Instantiate<MouseSelection>() as MouseSelection;
        }

        public void ChangeSelection(Vector2D targetPos)
        {
            endPosition = targetPos;

            var pos = new Vector2D(Math.Min(startPosition.X, endPosition.X), Math.Min(startPosition.Y, endPosition.Y));
            var size = new Vector2D(Math.Abs(startPosition.X - endPosition.X) + 1, Math.Abs(startPosition.Y - endPosition.Y) + 1);
            selection.Rectangle = new Rect(pos, size);
        }

        public void EndSelection(Vector2D endPos)
        {
            endPosition = endPos;

            ProcessingSelection = false;

            var pos = new Vector2D(Math.Min(startPosition.X, endPosition.X), Math.Min(startPosition.Y, endPosition.Y));
            var size = new Vector2D(Math.Abs(startPosition.X - endPosition.X) + 1, Math.Abs(startPosition.Y - endPosition.Y) + 1);

            selection.Rectangle = new Rect(pos, size);

            selection.Commit();
        }
    }
}
