using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeMQ;
using System.Threading;
using System.Runtime.CompilerServices;
using CodeMQExample.Queues;

namespace CodeMQExample
{
    class Program
    {
        static void QueueDepthsWorker()
        {
            try
            {
                for (; ; )
                {
                    Utilities.PrintQueueDepths();
                    Thread.Sleep(50);
                }
            }
            catch { }
        }

        static void NumberToTextWorker()
        {
            var qnum = new Queues.NumberToTextQueue();
            var qup = new Queues.ToUpperQueue();

            int num;
            while ((num = qnum.Get()) >= 0)
            {
                Utilities.Write(ConsoleColor.Magenta, "Consumed " + num);

                Thread.Sleep(1000);
                string converted = Utilities.NumberToText(num);

                qup.Put(converted);
                Utilities.Write(ConsoleColor.Magenta, "Produced " + converted);
            }

            Utilities.Write(ConsoleColor.Magenta, "Consumed end request");

            qup.Put(null);
            Utilities.Write(ConsoleColor.Magenta, "Produced end request");
        }

        static void ToUpperWorker()
        {
            var qup = new Queues.ToUpperQueue();

            string str;
            while ((str = qup.Get()) != null)
            {
                Utilities.Write(ConsoleColor.Green, "Consumed " + str);

                Thread.Sleep(2000);
                Utilities.Write(ConsoleColor.Gray, "Output: " + str.ToUpper());
            }

            Utilities.Write(ConsoleColor.Green, "Consumed end request");
        }

        static void Main(string[] args)
        {
            CodeMQManager.LoadQueue(typeof(Queues.NumberToTextQueue));
            CodeMQManager.LoadQueue(typeof(Queues.ToUpperQueue));

            var t = new Thread(() => NumberToTextWorker());
            var t1 = new Thread(() => ToUpperWorker());
            var t2 = new Thread(() => QueueDepthsWorker());

            t.Start();
            t1.Start();
            t2.Start();

            var qnum = new Queues.NumberToTextQueue();
            var qup = new Queues.ToUpperQueue();

            qnum.Put(1);
            Utilities.Write(ConsoleColor.Yellow, "Produced " + 1);

            qup.Put("abc");
            Utilities.Write(ConsoleColor.Yellow, "Produced abc");

            qnum.Put(2);
            Utilities.Write(ConsoleColor.Yellow, "Produced " + 2);

            qnum.Put(3);
            Utilities.Write(ConsoleColor.Yellow, "Produced " + 3);

            qnum.Put(4);
            Utilities.Write(ConsoleColor.Yellow, "Produced " + 4);

            qnum.Put(5);
            Utilities.Write(ConsoleColor.Yellow, "Produced " + 5);

            qnum.Put(10);
            Utilities.Write(ConsoleColor.Yellow, "Produced " + 10);

            qup.Put("test");
            Utilities.Write(ConsoleColor.Yellow, "Produced test");

            qnum.Put(-1);
            Utilities.Write(ConsoleColor.Yellow, "Produced end request");

            t.Join();
            t1.Join();
            
            Utilities.Write(ConsoleColor.Yellow, "Execution ended, next example will start in 3 sec.");
            Thread.Sleep(3000);

            CodeMQManager.LoadQueue(typeof(LogQueue));
            CodeMQManager.LoadQueue(typeof(TreeWorkerResultQueue));
            CodeMQManager.LoadQueue(typeof(TreeWorkerRequestQueue));

            // build a tree.
            var node =
            new TreeNode(1)
            {
                new TreeNode(2)
                {
                    new TreeNode(4)
                    {
                        new TreeNode(8),
                        new TreeNode(9)
                    }
                },
                new TreeNode(3)
                {
                    new TreeNode(6),
                    new TreeNode(5),
                    new TreeNode(7)
                }
            };

            var tC = new Thread(() => ControlWorker());
            var tw1 = new Thread(() => TreeWorker());
            var tw2 = new Thread(() => TreeWorker());
            var tw3 = new Thread(() => TreeWorker());
            var tw4 = new Thread(() => TreeWorker());
            var tw5 = new Thread(() => TreeWorker());
            var tw6 = new Thread(() => TreeWorker());
            var tw7 = new Thread(() => TreeWorker());
            var tw8 = new Thread(() => TreeWorker());

            tC.Start();
            tw1.Start(); tw2.Start(); tw3.Start(); tw4.Start();

            Utilities.Write(ConsoleColor.Gray, "Traversal in progress...");

            var w = new TreeWorkerResultQueue();
            // request to visit first tree node, to start the chain reaction
            w.Put(new TreeWorkerResultMessage()
            {
                 processed = false,
                 result = node
            });

            tC.Join();
            tw1.Join(); tw2.Join(); tw3.Join(); tw4.Join();
            
            Utilities.Write(ConsoleColor.Gray,"Traversal ended. Results:");
            var l = new LogQueue();
            while (CodeMQManager.GetQueueDepth(typeof(LogQueue)) > 0)
            {
                Utilities.Write(ConsoleColor.Gray, l.Get());
            }

            Utilities.Write(ConsoleColor.Gray, "Execution ended. ENTER to exit.");
            
            t2.Abort();

            Console.ReadLine();
        }

        #region Tree example workers
        static void ControlWorker()
        {
            var h = new HashSet<int>(); // keeps track of in-progress traversals.
            // if no more traversals are pending, it means we are done with the tree
            // and may terminate the workers by sending termination messages (null, in this case)

            var r = new TreeWorkerResultQueue();
            var w = new TreeWorkerRequestQueue();

            while (true)
            {
               TreeWorkerResultMessage rst = r.Get();
               if (rst.processed == false)
               {
                   h.Add(rst.result.Value);
                   w.Put(rst.result);
               }
               else
               {
                   h.Remove(rst.result.Value);
               }
               if (h.Count == 0)
               {
                   // tree was entirely traversed. terminate all 8 workers
                   w.Put(null); // 1
                   w.Put(null); // 2
                   w.Put(null); // 3
                   w.Put(null); // 4
                   return;
               }
            }
        }

        static void TreeWorker()
        {
            var r = new TreeWorkerRequestQueue();
            var w = new TreeWorkerResultQueue();
            var l = new LogQueue();
            while (true)
            {
                TreeNode nodeRequested = r.Get();                
                if (nodeRequested == null) return;
                Utilities.Write(ConsoleColor.Gray, nodeRequested.Value.ToString());
                // processing...
                Thread.Sleep(10000);
                foreach (var child in nodeRequested)
                    w.Put(new TreeWorkerResultMessage()
                    {
                        processed = false,
                        result = child
                    });
                l.Put("Visited node " + nodeRequested.Value);
                w.Put(new TreeWorkerResultMessage()
                {
                    processed = true,
                    result = nodeRequested
                });
            }
        }
        #endregion


    }
}
