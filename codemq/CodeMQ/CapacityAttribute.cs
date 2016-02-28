using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeMQ
{
    /// <summary>
    /// Used to set the maximum capacity of a queue.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class CapacityAttribute : Attribute
    {
        public CapacityAttribute(int capacity)
        {
            this.capacity = capacity;
        }

        public int Capacity
        {
            get { return capacity; }
        }
        
        private readonly int capacity;
    }
}
