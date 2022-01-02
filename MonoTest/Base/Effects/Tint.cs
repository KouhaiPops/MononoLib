using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoTest.Base.State;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Effects
{
    public class Tint : IShader
    {
        public Effect Effect { get; set; }
        public Color Color { get; set; }
        public Tint()
        {
            Effect = GlobalEffectState.TintShader;
        }

        public void Apply()
        {
            var @params = Effect.Parameters;
            var v = Mouse.GetState().Position.ToVector2();
            var rect = GlobalState.WindowBounds;
            @params["Viewport"].SetValue(new Vector2(rect.Width, rect.Height));
            @params["MousePos"].SetValue(v);
            //Effect.Parameters.Skip(1).First().SetValue();
            //Effect.Parameters.First().SetValue(Color.ToVector4());
        }
    }
}
