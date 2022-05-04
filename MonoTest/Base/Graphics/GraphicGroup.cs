using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Graphics
{
    public class GraphicGroup<TDrawable> : IDrawable where TDrawable : IDrawable
    {
        public Action<SpriteBatch> OnBegin { get; set; }
        public Action<SpriteBatch> OnEnd { get; set; }
        public HashSet<TDrawable> Drawables { get; set; } = new HashSet<TDrawable>();
        public DrawActions.PreDrawAction PreDraw { get; set; }
        public DrawActions.PostDrawAction PostDraw { get; set; }

        public virtual void Begin(SpriteBatch spriteBatch)
        {
            OnBegin?.Invoke(spriteBatch);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime)
        {
            foreach(var drawable in Drawables)
            {
                drawable.Draw(spriteBatch, gameTime);
            }
        }

        public virtual void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            foreach(var drawable in Drawables)
            {
                drawable.Update(gameTime);
            }
        }

        public virtual void End(SpriteBatch spriteBatch)
        {
            OnEnd?.Invoke(spriteBatch);
        }

    }
}
