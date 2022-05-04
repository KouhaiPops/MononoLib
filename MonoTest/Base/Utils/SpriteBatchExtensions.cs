using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Utils
{
    public static class SpriteBatchExtensions
    {
        public static void DrawGenericTexture(this SpriteBatch spriteBatch, Texture2D texture, BaseElement element = null)
        {
            if(element == null)
            {
                spriteBatch.Draw(texture,
                    Vector2.Zero,
                    null,
                    Color.White,
                    0,
                    Vector2.Zero,
                    Vector2.One,
                    SpriteEffects.None,
                    0);
            }
            else
            {
                spriteBatch.Draw(texture,
                    element.Transform.Position,
                    null,
                    Color.White,
                    element.Transform.Rotation,
                    element.Transform.Origin,
                    element.Transform.Scale,
                    SpriteEffects.None,
                    0);

            }
        }
    }
}
