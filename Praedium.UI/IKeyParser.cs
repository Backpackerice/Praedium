using System;

namespace Praedium.UI
{
    public interface IKeyParser
    {
        bool Parse(out KeyInfo info);
    }
}
