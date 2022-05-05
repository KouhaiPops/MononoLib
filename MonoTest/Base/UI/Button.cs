using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoTest.Base.Input;
using MonoTest.Base.State;
using MonoTest.Base.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.UI
{
    class Button : BaseElement, IUI
    {
        private readonly Texture2D buttonBackground;
        public DrawActions.PreDrawAction PreDraw { get; set; }
        public DrawActions.PostDrawAction PostDraw { get; set; }
        private BaseText text;


        public Button(int width, int height) : this(width, height, Color.Black) { }
        public Button(int width, int height, Color fill, string label = "Button")
        {
            Transform.Size = new Vector2(width, height);
            buttonBackground = new Texture2D(GlobalState.GrphDevMngr.GraphicsDevice, (int)Transform.Size.X, (int)Transform.Size.Y);
            Color[] colors = new Color[(int)(Transform.Size.X * Transform.Size.Y)];
            Array.Fill(colors, fill);
            buttonBackground.SetData(colors);
            text = new BaseText();
            text.Text = label;
            text.Transform.Position = new Vector2((width / 2)-(text.Transform.Size.X/4), (height / 2)-(text.Transform.Size.Y*2));
            AddChild(text);
            AxisAlignedBoundingBox
        }
        public Button(int width, int height, Texture2D buttonImage, string label = "Button")
        {
            
        }

        public override void Initialize()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //spriteBatch.Draw(buttonBackground, Transform.Position, null, Color.Transparent, 0, Transform.Origin, Transform.Scale, SpriteEffects.None, 0);
            spriteBatch.DrawGenericTexture(buttonBackground, this);
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
