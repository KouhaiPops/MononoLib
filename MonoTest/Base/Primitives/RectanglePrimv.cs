using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoTest.Base.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Primitives
{
    public class RectanglePrimv : BaseElement, IDrawable
    {
        public DrawActions.PreDrawAction PreDraw { get; set; }
        public DrawActions.PostDrawAction PostDraw { get; set; }
        private readonly Texture2D texture;
        public Color Color { get; set; }
        public RectanglePrimv(Vector2 size, Color color)
        {
            Transform.Size = size;
            Color = color;
            texture = SharedBasicTexture.GetSharedTexture(color);
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(
                texture,
                Transform.Position,
                null,
                Color,
                Transform.Rotation,
                Transform.Origin,
                Transform.Scale * Transform.Size,
                SpriteEffects.None,
                0);
        }
    }
}
