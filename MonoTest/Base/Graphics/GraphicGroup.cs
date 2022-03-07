using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Graphics
{
    public class GraphicGroup<TDrawable> where TDrawable : IDrawable
    {
        public Action<SpriteBatch> OnBegin { get; set; }
        public Action<SpriteBatch> OnEnd { get; set; }
        public HashSet<TDrawable> Drawables { get; set; } = new HashSet<TDrawable>();
        public void Begin(SpriteBatch spriteBatch)
        {
            OnBegin?.Invoke(spriteBatch);
        }

        public void End(SpriteBatch spriteBatch)
        {
            OnEnd?.Invoke(spriteBatch);
        }
    }
}
