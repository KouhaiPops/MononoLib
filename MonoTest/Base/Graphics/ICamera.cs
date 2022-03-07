using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Graphics
{
    public interface ICamera
    {
        public Matrix GetTransform();
    }
}
