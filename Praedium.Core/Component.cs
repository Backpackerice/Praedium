using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praedium.Core
{
    public abstract class Component
    {
        public GameObject GameObject
        {
            get;
            private set;
        }

        public abstract void Update();
    }
}
