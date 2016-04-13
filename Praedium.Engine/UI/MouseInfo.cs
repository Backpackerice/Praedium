using Bramble.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praedium.Engine.UI
{
    public struct MouseInfo
    {
        public MouseButton Button;
        public MouseEventType Type;
        public Vector2D Position;
        public bool Down;
    }
}
