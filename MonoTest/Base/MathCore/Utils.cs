using Microsoft.Xna.Framework;

using MonoTest.Base.State;
using MonoTest.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.MathCore
{
    public static class Utils
    {
        static private Vector3 windowSpaceNormalizationVector = default;
        public static Vector3 MapToWindow(this Vector2 vector2)
        {
            if (windowSpaceNormalizationVector == default)
            {
                windowSpaceNormalizationVector = new Vector3(GlobalState.WindowBounds.Size.ToVector2() / 2f, 1);
            }
            var normalizedVector = (new Vector3(vector2, 1) - windowSpaceNormalizationVector) / windowSpaceNormalizationVector;
            normalizedVector.Y *= -1;
            return normalizedVector;
        }

        public static Vector3 MapToWindow(this Vector2i vector2)
        {
            if (windowSpaceNormalizationVector == default)
            {
                windowSpaceNormalizationVector = new Vector3(GlobalState.WindowBounds.Size.ToVector2() / 2f, 1);
            }
            var normalizedVector = (new Vector3(vector2, 1) - windowSpaceNormalizationVector) / windowSpaceNormalizationVector;
            normalizedVector.Y *= -1;
            return normalizedVector;
        }
    }
}
