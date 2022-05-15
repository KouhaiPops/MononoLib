using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoTest.Base.Input;
using MonoTest.Base.State;
using MonoTest.Base.Utils;
using MonoTest.Core.Collision;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.UI
{
    class Button : BaseUI
    {
        private readonly Texture2D buttonBackground;

        private BaseText text;
        private Color color;
        private Color tint;

        public Button(int width, int height) : this(width, height, Color.Black) { }
        public Button(int width, int height, Color fill, string label = "Button")
        {
            // TODO should move up to a parent, the caller might not remember to initialzie this value
            BoundingBox = new BoundingBox<IUI>(this);
            color = fill;
            Transform.Size = new Vector2(width, height);
            buttonBackground = new Texture2D(GlobalState.GrphDevMngr.GraphicsDevice, (int)Transform.Size.X, (int)Transform.Size.Y);
            Color[] colors = new Color[(int)(Transform.Size.X * Transform.Size.Y)];
            Array.Fill(colors, Color.White);
            buttonBackground.SetData(colors);
            text = new BaseText();
            text.Text = label;
            text.Transform.Position = new Vector2((width / 2)-(text.Transform.Size.X/4), (height / 2)-(text.Transform.Size.Y*2));
            AddChild(text);
        }
        public Button(int width, int height, Texture2D buttonImage, string label = "Button")
        {
            
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //spriteBatch.Draw(buttonBackground, Transform.Position, null, Color.Transparent, 0, Transform.Origin, Transform.Scale, SpriteEffects.None, 0);
            spriteBatch.DrawGenericTexture(buttonBackground, this, color.Add(tint));
            text.Draw(spriteBatch, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keys.Up.IsDown())
            {
                Transform.Position.Y--;
            }
            if (Keys.Down.IsDown())
            {
                Transform.Position.Y++;
            }
            if (Keys.Right.IsDown())
            {
                Transform.Position.X++;
            }
            if (Keys.Left.IsDown())
            {
                Transform.Position.X--;
            }
        }

    }
}
