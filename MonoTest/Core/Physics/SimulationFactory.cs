using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Core.Physics
{
    public static class SimulationFactory
    {
        private static readonly ISimulation simulation = new Box2DSimulation();
        public static ISimulation GetDefault => simulation;
    }
}
