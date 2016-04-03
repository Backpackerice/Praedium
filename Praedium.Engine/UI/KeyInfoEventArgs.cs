using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praedium.Engine.UI
{
    public class KeyInfoEventArgs : EventArgs
    {
        public KeyInfoEventArgs(KeyInfo keyInfo)
        {
            KeyInfo = keyInfo;
        }

        public KeyInfo KeyInfo
        {
            get;
            private set;
        }
    }
}
