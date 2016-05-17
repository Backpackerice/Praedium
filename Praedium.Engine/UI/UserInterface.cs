using Bramble.Core;
using System;
using System.Collections.Generic;
using SFML.Window;

namespace Praedium.Engine.UI
{
    public class UserInterface
    {
        private Dictionary<Keyboard.Key, bool> pressedKeys;
        private Dictionary<Mouse.Button, bool> pressedMouseButtons;

        public Vector2D MousePosition
        {
            get;
            private set;
        }

        public UserInterface()
        {
            pressedKeys = new Dictionary<Keyboard.Key, bool>();
            pressedMouseButtons = new Dictionary<Mouse.Button, bool>();
        }

        public void ApplyKeyUp(KeyEventArgs args)
        {
            pressedKeys[args.Code] = false;
        }

        public void ApplyKeyDown(KeyEventArgs args)
        {
            pressedKeys[args.Code] = true;
        }

        public void ApplyMouseDown(MouseButtonEventArgs args)
        {
            pressedMouseButtons[args.Button] = true;
        }

        public void ApplyMouseUp(MouseButtonEventArgs args)
        {
            pressedMouseButtons[args.Button] = false;
        }

        public void ApplyMouseMove(MouseMoveEventArgs args)
        {
            MousePosition = new Vector2D(args.X, args.Y);
        }

        public bool IsMouseButtonDown(Mouse.Button button)
        {
            bool returnValue;
            pressedMouseButtons.TryGetValue(button, out returnValue);
            return returnValue;
        }

        public bool IsMouseButtonUp(Mouse.Button button)
        {
            return !IsMouseButtonDown(button);
        }

        public bool IsKeyDown(Keyboard.Key key)
        {
            bool returnValue;
            pressedKeys.TryGetValue(key, out returnValue);
            return returnValue;
        }

        public bool IsKeyUp(Keyboard.Key key)
        {
            return !IsKeyDown(key);
        }
    }
}
