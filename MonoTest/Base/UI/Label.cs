using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        public string Text { get => textRenderer.StringText; set => textRenderer.StringText = value; }
        private readonly Text textRenderer = new();

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Apply UI transformation
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
