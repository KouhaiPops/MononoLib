using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Debug
{
    public static class Utils
    {
        public static Microsoft.Xna.Framework.Vector2 ToXnaVector(this System.Numerics.Vector2 vector)
        {
            unsafe
            {
                return Unsafe.As<System.Numerics.Vector2, Microsoft.Xna.Framework.Vector2>(ref vector);
            }
        }

    }
}
