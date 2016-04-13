using Bramble.Core;
using System;
using System.Collections.Generic;

namespace Praedium.Engine.UI
{
    public class UserInterface
    {
        private Dictionary<Key, bool> pressedKeys;
        private Dictionary<MouseButton, bool> pressedMouseButtons;

        public Vector2D MousePosition
        {
            get;
            private set;
        }

        public UserInterface()
        {
            pressedKeys = new Dictionary<Key, bool>();
            pressedMouseButtons = new Dictionary<MouseButton, bool>();
        }

        public void ApplyKeyInfo(KeyInfo info)
        {
            pressedKeys[info.Key] = info.Down;
        }

        public void ApplyMouseInfo(MouseInfo info)
        {
            pressedMouseButtons[info.Button] = info.Down;
            MousePosition = info.Position;
        }

        public void ApplyMousePosition(Vector2D position)
        {
            MousePosition = position;
        }

        public bool IsMouseButtonDown(MouseButton button)
        {
            bool returnValue;
            pressedMouseButtons.TryGetValue(button, out returnValue);
            return returnValue;
        }

        public bool IsMouseButtonUp(MouseButton button)
        {
            return !IsMouseButtonDown(button);
        }

        public bool IsKeyDown(Key key)
        {
            bool returnValue;
            pressedKeys.TryGetValue(key, out returnValue);
            return returnValue;
        }

        public bool IsKeyUp(Key key)
        {
            return !IsKeyDown(key);
        }
    }
}
