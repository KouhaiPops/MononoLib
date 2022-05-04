using Microsoft.Xna.Framework;

using MonoTest.Base.Component;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Graphics.Vendor
{
    public struct Glyph
    {
        public Transform Transform { get; }
        public Rectangle? src;
        public Color color;
        public float depth;
        Vector2 middle;
        
        public Glyph(Vector2 pos, float rotation, Color color, Vector2 origin, Vector2 scale, float depth, Rectangle? src)
        {
            if(src != null)
            {
                var _src = src.Value;
                middle = new Vector2(_src.Width / 2, _src.Height / 2);
            }
            else
            {
                middle = default;
            }

            this.color = color;
            this.src = src;
            this.depth = depth;
            Transform = new Transform
            {
                Position = pos,
                Rotation = rotation,
                Scale = scale,
                Origin = origin,
            };
        }
    }
}
