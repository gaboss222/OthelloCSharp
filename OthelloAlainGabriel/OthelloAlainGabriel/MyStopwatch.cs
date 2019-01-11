using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloAlainGabriel
{
    /// <summary>
    /// redefined class for StopWatch. 
    /// Stopwatch is the timer used for each player's turn
    /// </summary>
    public class MyStopwatch
    {

        private TimeSpan offsetTS;
        private Stopwatch sw;

        /// <summary>
        /// Constructor
        /// </summary>
        public MyStopwatch()
        {
            sw = new Stopwatch();
        }

        /// <summary>
        /// Constructor with an offset as parameter. Used to set Start time for the stopwatch !
        /// Constructor used when a game is loaded. 2 timer need to be created.
        /// Their starttime correspond to the values ​​of the xml document
        /// </summary>
        /// <param name="startTime">Start time</param>
        public MyStopwatch(TimeSpan startTime)
        {
            offsetTS = startTime;

            sw = new Stopwatch();
        }

        /// <summary>
        /// Start function
        /// </summary>
        public void Start()
        {
            sw.Start();
        }

        /// <summary>
        /// Stop function
        /// </summary>
        public void Stop()
        {
            sw.Stop();
        }

        /// <summary>
        /// Elapsed function.
        /// Return the value of stopwatch (from 0) + their startTime (as timeSpan)
        /// </summary>
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
