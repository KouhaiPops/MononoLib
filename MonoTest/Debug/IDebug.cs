using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Debug
{
    public interface IDebug
    {
        public void DebugDraw(SpriteBatch spriteBatch, GameTime gameTime);
        public void UpdateDebug(GameTime gameTime);
    }
}
