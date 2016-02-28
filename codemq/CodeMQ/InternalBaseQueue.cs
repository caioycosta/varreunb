using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeMQ
{
    public abstract class InternalBaseQueue
    {
        internal abstract int GetQueueDepth();
        internal abstract void Load(int capacity);
    }
}
