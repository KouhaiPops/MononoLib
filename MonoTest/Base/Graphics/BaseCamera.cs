using Microsoft.Xna.Framework;

using MonoTest.Base.State;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Graphics
{
    public class BaseCamera : ICamera
    {
        private Vector2 _Position;
        public ref Vector2 Position { get => ref _Position; }
        public ref float Zoom { get => ref _zoom; }
        private float _zoom = 1;
        public BaseCamera()
        {
            GlobalState.MainCamera = this;
        }
        public Matrix GetTransform()
        {
            return Matrix.Identity * Matrix.CreateTranslation(_Position.X, _Position.Y, 0) * Matrix.CreateScale(_zoom);
        }
    }
}
