using FontStashSharp;

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.State
{
    public class GlobalState
    {
        public static FontSystem FontManager { get; internal set; }
        public static GraphicsDeviceManager GrphDevMngr { get; set; }
        public static Rectangle WindowBounds { get; internal set; }
        public static float AnimationScale = 1;
    }
}
