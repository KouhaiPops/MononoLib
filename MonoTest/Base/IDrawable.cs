using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static MonoTest.Base.DrawActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base
{
    public interface IDrawable : IGameElement//, Microsoft.Xna.Framework.IDrawable
    {
        public PreDrawAction PreDraw { get; set; }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime);
        public PostDrawAction PostDraw { get; set; }
    }
}
