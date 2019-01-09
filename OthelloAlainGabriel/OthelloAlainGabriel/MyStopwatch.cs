using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloAlainGabriel
{
    public class MyStopwatch

    {

        TimeSpan offsetTS;

        Stopwatch sw;

        public MyStopwatch()
        {
            sw = new Stopwatch();
        }
        public MyStopwatch(TimeSpan offsetElapsedTimeSpan)
        {
            offsetTS = offsetElapsedTimeSpan;

            sw = new Stopwatch();

        }
        public void Start()
        {
            sw.Start();
        }

        public void Stop()
        {
            sw.Stop();
        }
        public TimeSpan Elapsed
        {
            get
            {
                if (offsetTS == null)
                    return sw.Elapsed;
                return sw.Elapsed + offsetTS;
            }
            set
            {
                offsetTS = value;
            }
        }
    }
}
