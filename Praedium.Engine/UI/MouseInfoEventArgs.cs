using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praedium.Engine.UI
{
    public class MouseInfoEventArgs : EventArgs
    {        
        public MouseInfoEventArgs(MouseInfo info)
        {
            MouseInfo = info;
        }

        public MouseInfo MouseInfo
        {
            get;
            private set;
        }
    }
}
