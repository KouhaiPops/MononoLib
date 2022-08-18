using Microsoft.Xna.Framework;

using MonoTest.Base.State;
using MonoTest.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.MathCore
{
    public static class Utils
    {
        public const int PixelPerMeter = 50;
        public const float PixelToMeterFactor = 1f / PixelPerMeter;
        // Redundant
        public const float MeterToPixelFactor = 1f * PixelPerMeter;

        static private Vector3 windowSpaceNormalizationVector = default;

        /// <summary>
        /// Map a pixel vector to screen space vector
        /// </summary>
        /// <param name="vector2"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Map a pixel vector to screen space vector
        /// </summary>
        /// <param name="vector2"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Reference cast the provided vector to a system vector
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static System.Numerics.Vector2 ToSystem(this Vector2 v)
        {
            return Unsafe.As<Vector2, System.Numerics.Vector2>(ref v);
        }

        /// <summary>
        /// Refernce cast the provided vector to a XNA vector
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector2 ToXNA(this System.Numerics.Vector2 v)
        {
            return Unsafe.As<System.Numerics.Vector2, Vector2>(ref v);
        }

        /// <summary>
        /// COnvert a vector of pixel values to a meter vector
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector2 ToMeter(this Vector2 v)
        {
            return v * PixelToMeterFactor;
        }
        /// <summary>
        /// Convert a vector of meter values to a pixel vector
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector2 ToPixel(this Vector2 v)
        {
            return v * MeterToPixelFactor;
        }

        /// <summary>
        /// COnvert a float pixel value to a float meter value
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static float ToMeter(float f)
        {
            return f * PixelToMeterFactor;
        }

        /// <summary>
        /// Convert a float meter value to a float pixel value
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static float ToPixel(float f)
        {
            return f * MeterToPixelFactor;
        }
    }
}
