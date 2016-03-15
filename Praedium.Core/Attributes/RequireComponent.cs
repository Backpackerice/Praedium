using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praedium.Core.Attributes
{
    /// <summary>
    /// Used to require all required components for the game object component.
    /// Dependencies are resolved in a way to not require components that have been added to game object before.
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
    public class RequireComponent : Attribute
    {
        public Type[] ComponentTypes
        {
            get;
            set;
        }

        public RequireComponent(params Type[] componentTypes)
        {
            ComponentTypes = componentTypes;
        }
    }
}
