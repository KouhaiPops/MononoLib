using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoTest.Base.Input;
using MonoTest.Base.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Primitives
{
    public class DrawableTexture : BaseElement, IDrawable
    {
        public Texture2D Texture { get; private set; }
        public float Speed { get; set; } = 0.2f;

        public void SetTexture(Texture2D texture)
        {
            Texture = texture;
            UpdateTransform();
        }


        private void UpdateTransform()
        {
            Transform.Size = new Vector2(Texture.Width, Texture.Height);
        }
        public DrawableTexture(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Transform.Position = position;
            UpdateTransform();
        }

        public DrawActions.PreDrawAction PreDraw { get; set; }
        public DrawActions.PostDrawAction PostDraw { get; set; }

        public void Draw(SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime)
        {
            spriteBatch.DrawGenericTexture(Texture, this);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

        }

        public override void Initialize()
        {
            KeyboardController.AddHandler(Microsoft.Xna.Framework.Input.Keys.A, (time) => Transform.Position.X -= (float)(Speed * time.ElapsedGameTime.TotalMilliseconds));
            KeyboardController.AddHandler(Microsoft.Xna.Framework.Input.Keys.D, (time) => Transform.Position.X += (float)(Speed * time.ElapsedGameTime.TotalMilliseconds));
            KeyboardController.AddHandler(Microsoft.Xna.Framework.Input.Keys.W, (time) => Transform.Position.Y -= (float)(Speed * time.ElapsedGameTime.TotalMilliseconds));
            KeyboardController.AddHandler(Microsoft.Xna.Framework.Input.Keys.S, (time) => Transform.Position.Y += (float)(Speed * time.ElapsedGameTime.TotalMilliseconds));
        }
    }
}
