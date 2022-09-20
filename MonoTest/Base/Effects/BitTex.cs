using Microsoft.Xna.Framework.Graphics;

using MonoTest.Base.State;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Effects
{
    public class BitTex : IShader
    {
        public Texture2D SubTex { get; set; }
        public Effect Effect { get; set; }
        public BitTex()
        {
            Effect = GlobalEffectState.BitTex;
            SubTex = Texture2D.FromFile(GlobalState.GrphDevMngr.GraphicsDevice, @"E:\GameDev\Assets\Images\element.png");
        }

        public void Apply()
        {
            Effect.Parameters["SpriteTexture"].SetValue(SubTex);
            Effect.Parameters["OverlayTexture"].SetValue(SubTex);
        }
    }
}
