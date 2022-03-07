
using MonoTest.Base;
using MonoTest.Debug;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Core.Collision
{
    public class AABB : ICollider<BaseElement>
    {
        public BaseElement Parent { get; }

        public AABB(BaseElement parent)
        {
            Parent = parent;
            if(GraphicsDebugger.ShouldDebug)
            {
                GraphicsDebugger.DebugAABB(this);
            }
        }

        public void StartDebug()
        {

        }

        public void StopDebug()
        {

        }
        //public AABB(IElement drawable)
        //{
        //    drawable.Transform.
        //}
    }
}
