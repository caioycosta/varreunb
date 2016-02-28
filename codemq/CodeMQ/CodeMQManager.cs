using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;

namespace CodeMQ
{
    public static class CodeMQManager
    {
        private static List<InternalBaseQueue> loadedQueues = new List<InternalBaseQueue>();
        private static ConcurrentDictionary<Type, object> queueStates = new ConcurrentDictionary<Type, object>();

        internal static QueueState<T> GetQueueState<T>(Type type)
        {
            return (QueueState<T>) queueStates.GetOrAdd(type, new QueueState<T>());
        }

        public static void LoadQueue(Type queueType)
        {
            if (queueType.BaseType.BaseType != typeof(InternalBaseQueue))
            {
                throw new Exception("Queue to load does not inherit from BaseQueue.");
            }

            var attr = queueType.GetCustomAttributes(typeof(CapacityAttribute), true);
            if (attr.Length != 1)
            {
                throw new Exception("Queue capacity is not defined.");
            }

            if (((CapacityAttribute)attr[0]).Capacity <= 0)
            {
                throw new Exception("Invalid queue capacity specified.");
            }

            if (loadedQueues.Any(k => k.GetType() == queueType))
            {
                throw new Exception("Queue is already loaded.");
            }

            var instance = (InternalBaseQueue)Activator.CreateInstance(queueType);
            instance.Load(((CapacityAttribute)attr[0]).Capacity);

            loadedQueues.Add(instance);
        }

        public static Dictionary<Type, int> GetQueueDepths()
        {
            return loadedQueues.ToDictionary(n => n.GetType(), n => n.GetQueueDepth());
        }

        public static int GetQueueDepth(Type queueType)
        {
            return GetQueueDepths()[queueType];
        }
    }
}
