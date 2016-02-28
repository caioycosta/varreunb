using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeMQ;

namespace CodeMQExample.Queues
{
    #region Queues used on first example
    [Capacity(3)]
    class ToUpperQueue : BaseQueue<string>
    {
    }

    [Capacity(2)]
    class NumberToTextQueue : BaseQueue<int>
    {
    }
    #endregion

    #region Classes and queues used on second example (tree traversal)
    class TreeNode : List<TreeNode>
    {
        public int Value;
        public TreeNode(int value)
        {
            this.Value = value;
        }
    }

    class TreeWorkerResultMessage
    {
        /// <summary>
        /// The result node. may be a new node to be traversed later, 
        /// or a node to be acknowleged as processed.
        /// </summary>
        /// <see cref="processed"/>
        public TreeNode result;

        /// <summary>
        /// If the result is a new node to be traversed later, then false.
        /// if the result is to mark a node as having completed processing, then true.
        /// </summary>
        public bool processed;
    }
    [Capacity(100)]
    class LogQueue : BaseQueue<string> { } // for printing results after execution
    [Capacity(10)]
    class TreeWorkerResultQueue : BaseQueue<TreeWorkerResultMessage> { } // workers write here
    [Capacity(10)]
    class TreeWorkerRequestQueue : BaseQueue<TreeNode> { } // workers read from here

    #endregion
}
