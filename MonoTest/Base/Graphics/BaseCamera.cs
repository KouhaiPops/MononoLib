using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Graphics
{
    public class BaseCamera : ICamera
    {
        public ref Vector2 Position { get => ref _Position; }

        private Vector2 _Position;

        public Matrix GetTransform()
        {
            return Matrix.Identity * Matrix.CreateTranslation(_Position.X, _Position.Y, 0);
        }
    }
}
