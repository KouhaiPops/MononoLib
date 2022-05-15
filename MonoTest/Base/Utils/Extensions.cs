using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Utils
{
    public static class Extensions
    {
        public static Color Add(this Color firstColor, Color secondColor)
        {
            return new Color(firstColor.R + secondColor.R, firstColor.G + secondColor.G, firstColor.B + secondColor.B);
        }
    }
}
