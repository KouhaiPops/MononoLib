using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.UI
{
    public class UITree : IDrawable
    {
        public HashSet<IDrawable> UIControlles { get; set; } = new HashSet<IDrawable>();
        public DrawActions.PreDrawAction PreDraw { get; set; }
        public DrawActions.PostDrawAction PostDraw { get; set; }

        public void AddControl(IDrawable control)
        {
            UIControlles.Add(control);
        }

        public void Draw(SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
