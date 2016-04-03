using System;
using System.Collections.Generic;

namespace Praedium.Engine.UI
{
    public class UserInterface
    {
        private Dictionary<Key, bool> pressedKeys;

        public UserInterface()
        {
            pressedKeys = new Dictionary<Key, bool>();
        }

        public void ApplyKeyInfo(KeyInfo info)
        {
            pressedKeys[info.Key] = info.Down;
        }

        public bool IsKeyDown(Key key)
        {
            bool returnValue;
            pressedKeys.TryGetValue(key, out returnValue);
            return returnValue;
        }

        public bool IsKeyUp(Key key)
        {
            bool returnValue;
            pressedKeys.TryGetValue(key, out returnValue);
            return !returnValue;
        }
    }
}
