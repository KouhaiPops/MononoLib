using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.UI
{
    public class StatefullUI<T> : IDrawable where T : IUI
    {
        public T Element { get; }
        public bool Clicked { get; set; }
        public bool Hovered { get; set; }
        public DrawActions.PreDrawAction PreDraw { get; set; }
        public DrawActions.PostDrawAction PostDraw { get; set; }

        public StatefullUI(T element)
        {
            Element = element;
            PreDraw = element.PreDraw;
            PostDraw = element.PostDraw;
        }

        public void Draw(SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime)
        {
            Element.Draw(spriteBatch, gameTime);
        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Element.Update(gameTime);
        }
    }
}
