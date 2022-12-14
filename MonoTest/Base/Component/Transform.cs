using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Component
{
    [Flags]
    internal enum _originPivot
    {
        Left = 2,
        Top = 4,
        Right = 8,
        Bottom = 16,
    }

    public enum OriginPivot
    {
        Center = 1,
        Default = TopLeft,
        TopLeft = _originPivot.Top | _originPivot.Left,
        TopRight = _originPivot.Top | _originPivot.Right,
        BottomLeft = _originPivot.Bottom | _originPivot.Left,
        BottomRight = _originPivot.Bottom | _originPivot.Right,
    }


    public static class Extensions
    {
        public static float ToMeter(this float pixel)
        {
            return pixel * Transform.PixelToMeter;
        }

        public static float ToPixel(this float meter)
        {
            return 50f * meter;
        }
    }
    public class Transform : IComponent
    {
        public void Translate(float x, float y)
        {
            const float rad90 = 1.5708f;
            // Consider two lines perpendicular to the 
            // Rotate these two vectors be the angel
            var xPrime = new Vector2(MathF.Cos(Rotation), MathF.Sin(Rotation));
            var yPrime = new Vector2(MathF.Cos(Rotation+rad90), MathF.Sin(Rotation+rad90));
            var yP = y * MathF.Cos(Rotation) + x * MathF.Sin(Rotation);
            var xP = x * MathF.Cos(Rotation) - y * MathF.Sin(Rotation);
            Position.X += xP;
            Position.Y += yP;
        }

        internal const float PixelToMeter = 1f / 50f;
        public OriginPivot OriginPivot
        {
            get => _originPivot; 
            set
            {
                _originPivot = value;
                UpdateOrigin();
            }
        }


        private OriginPivot _originPivot = OriginPivot.Default;

        public ref Vector2 Position { get => ref _Position; }
        private Vector2 _Position;

        public Vector2 ScenePosition
        {
            get
            {
                if(Parent != null)
                {
                    return _Position + Parent.ScenePosition;
                }
                return _Position;
            }
        }

        public ref Vector2 Size { get => ref _Size; }

        private Vector2 _Size;

        public ref Vector2 Scale { get => ref _Scale; }

        private Vector2 _Scale = Vector2.One;

        public Transform? Parent { get; internal set; }

        public ref float Rotation { get => ref _Rotation; }
        private float _Rotation;


        // TODO, with the current design origin should be immutable
        // But this limits the user to only 4 axis to pivot around (by updating the originPivot)
        // A work around for now is making the origin a ref value type
        // This should be removed
        public ref Vector2 Origin { get => ref _origin;}
        private Vector2 _origin = Vector2.Zero;

        private void UpdateOrigin()
        {
            switch (_originPivot)
            {
                case OriginPivot.Center:
                    _origin = Size/-2;
                    break;
                case OriginPivot.TopLeft:
                    _origin = Vector2.Zero;
                    break;
                case OriginPivot.TopRight:
                    _origin = new Vector2(-Size.X, 0);
                    break;
                case OriginPivot.BottomLeft:
                    _origin = new Vector2(0, -Size.Y);
                    break;
                case OriginPivot.BottomRight:
                    _origin = -Size;
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine("Invalid origin pivot");
                    _origin = Vector2.Zero;
                    break;
            }
        }

        public Vector2 GetOriginRelativeTo(OriginPivot pivot)
        {
            return pivot switch
            {
                OriginPivot.Center => Size / -2,
                OriginPivot.TopLeft => Vector2.Zero,
                OriginPivot.TopRight => new Vector2(-Size.X, 0),
                OriginPivot.BottomLeft => new Vector2(0, -Size.Y),
                OriginPivot.BottomRight => -Size,
                _ => Vector2.Zero,
            };
        }
    }
}
        
        