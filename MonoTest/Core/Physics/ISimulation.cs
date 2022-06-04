using Microsoft.Xna.Framework;

using MonoTest.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Core.Physics
{
    public interface ISimulation : IUpdate
    {
        public void AddDynamic(IElement element, bool movable = false);
        public void AddFixed(IElement element);
        public void Simulate(GameTime gameTime);
    }
}
