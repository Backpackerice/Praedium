﻿using Bramble.Core;
using Malison.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praedium.Engine
{
    public struct Tile
    {
        public Character Character;
        public Vector2D Position;
        public bool Collideable;

        public Tile(Character character, Vector2D position, bool collideable = false)
        {
            Character = character;
            Position = position;
            Collideable = collideable;
        }
    }
}
