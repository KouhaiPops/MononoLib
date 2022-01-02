using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoTest.Base.Effects;

using System;
using System.Collections.Generic;
using System.Text;

namespace MonoTest.Base
{
    public static class DrawBase
    {
        public static void PreDraw(SpriteBatch spriteBatch, GameTime gameTime, params IShader[] shaders)
        {
            if(shaders.Length > 0)
            {
                spriteBatch.Begin(effect: shaders[0].Effect, samplerState: SamplerState.LinearClamp);
                shaders[0].Apply();
            }
            else
            {
                spriteBatch.Begin();
            }
        }

        public static void PostDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.End();
        }
    }
}
