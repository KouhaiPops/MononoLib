using Microsoft.Xna.Framework;

using MonoTest.Base;
using MonoTest.Base.Component;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Core.Collision
{
    public class BoundingBox<T> : IComponent where T : IElement
    {
        private T _parent;
        public BoundingBox(T parent)
        {
            _parent = parent;
        }
        public bool PointIntersecting(Vector2 point)
        {
            var position = _parent.Transform.ScenePosition;
            var size = _parent.Transform.Size;
            if(point.X >= position.X && point.Y >= position.Y &&
                point.X <= size.X+position.X && point.Y <= size.Y+position.Y)
            {
                return true;
            }
            return false;
        }
    }
}
