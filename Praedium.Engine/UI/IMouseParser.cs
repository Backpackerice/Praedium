using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praedium.Engine.UI
{
    public interface IMouseParser
    {
        bool Parse(out MouseInfo info);
    }
}
