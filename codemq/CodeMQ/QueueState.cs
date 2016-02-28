using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;

namespace CodeMQ
{
    /// <summary>
    /// Used for storing each queue state in a central location.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class QueueState<T>
    {
        internal bool Loaded;
        internal ConcurrentQueue<T> Queue;
        internal Semaphore putSemaphore;
        internal Semaphore getSemaphore;
    }
}
