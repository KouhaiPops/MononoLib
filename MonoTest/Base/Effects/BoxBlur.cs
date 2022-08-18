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
    public class BoxBlur : IShader
    {
        public Effect Effect { get; set; }
        public Color Color { get; set; }
        public Vector2 Size { get; set; }
        public BoxBlur(Vector2 size)
        {
            Effect = GlobalEffectState.BoxBlurShader;
            Size = size;
        }

        public void Apply()
        {
            var @params = Effect.Parameters;
            try
            {
                //@params["TexSize"].SetValue(Size);
                //@params["Param"].SetValue(new Vector2(12, 12));
            }
            catch (Exception)
            {
                //throw;
            }
            var v = Mouse.GetState().Position.ToVector2();
            var rect = GlobalState.WindowBounds;
            @params["Param"].SetValue(new Vector2(0.2f, 0.9f));
            //@params["Viewport"].SetValue(new Vector2(rect.Width, rect.Height));
            //@params["MousePos"].SetValue(v);
            //Effect.Parameters.First().SetValue(Color.ToVector4());
            //Effect.Parameters.Skip(1).First().SetValue();
        }
    }
}
