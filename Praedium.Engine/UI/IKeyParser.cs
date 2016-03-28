using System;

namespace Praedium.Engine.UI
{
    public interface IKeyParser
    {
        bool Parse(out KeyInfo info);
    }
}
