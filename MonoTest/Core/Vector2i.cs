using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Core
{
    public struct Vector2i
    {
        private static Vector2i _zero = new(0);
        public static readonly Vector2i Zero = _zero;

        private static Vector2i _unit = new(1);
        public static readonly Vector2i Unit = _unit;


        public int X;
        public int Y;

        public void Abs()
        {
            if (X < 0)
                X *= -1;
            if (Y < 0)
                Y *= -1;
        }
        public Vector2i(int scalar)
        {
            X = scalar;
            Y = scalar;
        }

        public Vector2i(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector2i operator *(Vector2i v1, int scalar)
        {
            v1.X *= scalar;
            v1.Y *= scalar;
            return v1;
        }

        public static Vector2i operator +(Vector2i v1, int scalar)
        {
            v1.X += scalar;
            v1.Y += scalar;
            return v1;
        }

        public static Vector2i operator -(Vector2i v1, int scalar)
        {
            v1.X -= scalar;
            v1.Y -= scalar;
            return v1;
        }

        public static Vector2i operator +(Vector2i v1, Vector2i v2)
        {
            v1.X += v2.X;
            v1.Y += v2.Y;
            return v1;
        }

        public static Vector2i operator -(Vector2i v1, Vector2i v2)
        {
            v1.X -= v2.X;
            v1.Y -= v2.Y;
            return v1;
        }

        public static implicit operator Vector2(Vector2i current)
        {
            return new Vector2(current.X, current.Y);
        }

        // TODO change this to not create a new instance, instead copy the the data the new vector
        public static implicit operator Vector3(Vector2i self)
        {
            return new Vector3(self, 0);
        }
    }
}
