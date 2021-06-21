using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.System.Threading;

namespace SH.IOTLIb
{
    public class HThread
    {
        WorkItemHandler workItem;
        public HThread(WorkItemHandler handler)
        {
            if (handler == null)
            {
                handler = call;
            }
            workItem = handler;
        }
        public async Task Start()
        {
            await Windows.System.Threading.ThreadPool.RunAsync(workItem, WorkItemPriority.High);
        }

        private void call(IAsyncAction operation)
        {
            Console.WriteLine("为实现方法,这也即是 IOT 实现的  线程");
        }
    }
}
