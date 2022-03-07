using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Core.Collision
{
    public interface ICollider<ICollidable>
    {
        public ICollidable Parent { get; }
        public void StartDebug();
        public void StopDebug();
    }
}
