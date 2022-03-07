using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoTest.Base.State;
using MonoTest.Base.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.UI
{
    class Button : BaseElement, IDrawable
    {
        private Texture2D buttonBackground;
        public DrawActions.PreDrawAction PreDraw { get; set; }
        public DrawActions.PostDrawAction PostDraw { get; set; }


        public Button(int width, int height) : this(width, height, Color.Black) { }
        public Button(int width, int height, Color fill)
        {
            Transform.Size = new Vector2(width, height);
            buttonBackground = new Texture2D(GlobalState.GrphDevMngr.GraphicsDevice, (int)Transform.Size.X, (int)Transform.Size.Y);
            Color[] colors = new Color[(int)(Transform.Size.X * Transform.Size.Y)];
            Array.Fill(colors, fill);
            buttonBackground.SetData(colors);
        }

        public override void Initialize()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //spriteBatch.Draw(buttonBackground, Transform.Position, null, Color.Transparent, 0, Transform.Origin, Transform.Scale, SpriteEffects.None, 0);
            spriteBatch.DrawGenericTexture(buttonBackground, this);
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
