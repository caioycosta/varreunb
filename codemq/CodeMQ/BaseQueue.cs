using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;

namespace CodeMQ
{
    /// <summary>
    /// Base class for queues
    /// </summary>
    /// <typeparam name="T">The message type.</typeparam>
    public abstract class BaseQueue<T> : InternalBaseQueue
    {
        private QueueState<T> State
        {
            get
            {
                return CodeMQManager.GetQueueState<T>(this.GetType());
            }
        }

        /// <summary>
        /// Verify that the class is "known" to CodeMQ
        /// </summary>
        internal bool Loaded
        {
            get
            {
                return State.Loaded;
            }
            set
            {
                State.Loaded = value;
            }
        }

        /// <summary>
        /// The message queue.
        /// </summary>
        internal ConcurrentQueue<T> Queue
        {
            get
            {
                return State.Queue;
            }
            set {
                State.Queue = value;
            }
        }

        internal override int GetQueueDepth()
        {
            return Queue.Count;
        }

        /// <summary>
        /// Used for blocking on Get() if queue is empty.
        /// </summary>
        private Semaphore getSemaphore
        {
            get
            {
                return State.getSemaphore;
            }
            set
            {
                State.getSemaphore = value;
            }
        }

        /// <summary>
        /// Used for blocking on Put() if queue is full.
        /// </summary>
        private Semaphore putSemaphore
        {
            get
            {
                return State.putSemaphore;
            }
            set
            {
                State.putSemaphore = value;
            }
        }

        /// <summary>
        /// Capacity of the queue.
        /// </summary>
        private static int Capacity;

        public BaseQueue()
        {
           
        }

        /// <summary>
        /// Initialize and mark this queue as loaded.
        /// </summary>
        internal override void Load(int capacity)
        {
            Capacity = capacity;
            Queue = new ConcurrentQueue<T>();            
            getSemaphore = new Semaphore(0, Capacity);
            putSemaphore = new Semaphore(Capacity, Capacity);
            Loaded = true;
        }

        /// <summary>
        /// Puts a message in the queue.
        /// If the queue is full, blocks until a message is taken from the queue.
        /// </summary>
        /// <param name="message">The message to put in the queue.</param>
        public virtual void Put(T message)
        {
            if (!Loaded)
                throw new Exception("This queue is not initialized! It must be acknowledged by the CodeMQManager.");

            putSemaphore.WaitOne();
            Queue.Enqueue(message);
            getSemaphore.Release();
        }

        /// <summary>
        /// Gets a message from the queue.
        /// If the queue is empty, blocks until a message is put in the queue.
        /// </summary>
        /// <returns>The message from the top of the queue.</returns>
        public virtual T Get()
        {
            if (!Loaded)
                throw new Exception("This queue is not initialized! It must be acknowledged by the CodeMQManager.");

            getSemaphore.WaitOne();
            T result;
            bool successful = Queue.TryDequeue(out result);
            if (successful)
            {
                putSemaphore.Release();
                return result;
            }
            else throw new Exception("Tried to get message, but queue was empty! This is a bug.");
        }
    }
}
