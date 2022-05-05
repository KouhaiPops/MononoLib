using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoTest.Base.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.UI
{
    public class Label : BaseElement, IUI
    {
        public DrawActions.PreDrawAction PreDraw { get; set; }
        public DrawActions.PostDrawAction PostDraw { get; set; }
        public string Text { get => textRenderer.Text; set => textRenderer.Text = value; }
        private readonly BaseText textRenderer = new();

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            textRenderer.Draw(spriteBatch, gameTime);
            // Apply UI transformation
        }

        public override void Update(GameTime gameTime)
        {
            if(Keys.Up.IsDown())
            {
                Transform.Position.Y--;
            }
            if(Keys.Down.IsDown())
            {
                Transform.Position.Y++;
            }
            if(Keys.Right.IsDown())
            {
                Transform.Position.X++;
            }
            if(Keys.Left.IsDown())
            {
                Transform.Position.X--;
            }
        }
    }
}
