using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoTest.Base.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base
{
    public static class DrawActions
    {
        public delegate void PreDrawAction(SpriteBatch spriteBatch, GameTime gameTime, params IShader[] shaders);
        public delegate void PostDrawAction(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
