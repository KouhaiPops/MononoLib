using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoTest.Base.State;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Graphics
{
    public static class SharedBasicTexture
    {
        static Dictionary<Color, Texture2D> rectTextures = new();
        public static Texture2D GetSharedTexture(Color color)
        {
            if (rectTextures.TryGetValue(Color.Black, out var texture))
            {
                return texture;
            }
            var tex = new Texture2D(GlobalState.GrphDevMngr.GraphicsDevice, 1, 1);
            rectTextures.Add(Color.Black, tex);
            tex.SetData(new byte[] { 255, 255, 255, 255 });
            return tex;
        }
    }
}
