using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace SH.IOTLIb
{
    /// <summary>
    /// 这里只是 DispatcherTimer  示例代码   可用
    /// </summary>
    public class Timer
    {
        private DispatcherTimer timer;
        public Timer(EventHandler<object> Timer_Tick, TimeSpan span)
        {
            timer = new DispatcherTimer();
            timer.Interval = span;
            timer.Tick += Timer_Tick;
            timer.Start();           
        }

        public void Stop()
        {
            timer.Stop();            
        }
      
    }
}
