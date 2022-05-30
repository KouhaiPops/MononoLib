
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Debug
{
    public struct FpsDebugger
    {
        const int size = 10;
        private unsafe fixed double rollingGameTime[size];
        private int rollingGameTimeCounter;
        private static double totalElapsed = 0;

        public void Add(double elapsed)
        {
            unsafe
            {
                totalElapsed -= rollingGameTime[rollingGameTimeCounter];
                rollingGameTime[rollingGameTimeCounter] = elapsed;
                totalElapsed += rollingGameTime[rollingGameTimeCounter];
                rollingGameTimeCounter = (rollingGameTimeCounter + 1) % size;
            }
        }
        public double FPS()
        {
            var e = Math.Round(1000f / (totalElapsed / size));
            return double.IsInfinity(e) ? 0 : e;
        }
    }
}
