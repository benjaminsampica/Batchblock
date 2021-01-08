using System;
using System.Threading.Tasks.Dataflow;

namespace Batchblock
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var batchBlock = new BatchBlock<int>(50);
            var actionBlock = new ActionBlock<int[]>(request =>
            {
                foreach (var i in request)
                {
                    Console.WriteLine(i);
                }
            });

            batchBlock.LinkTo(actionBlock, new DataflowLinkOptions { PropagateCompletion = true });

            for (var i = 0; i < 10; i++)
            {
                actionBlock.Post(new int[] { i });
            }

            actionBlock.Completion.Wait();
        }
    }
}
