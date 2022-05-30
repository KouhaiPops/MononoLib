using Microsoft.Xna.Framework.Graphics;

using MonoTest.Base.State;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Graphics
{
    public class Sprite : ISprite
    {
        private Texture2D texture;
        private string diskPath;
        public Sprite(Texture2D texture)
        {
            Texture = texture;
        }

        public Texture2D Texture { get; }

        public static Sprite FromFile(string path)
        {
            return new(Texture2D.FromFile(GlobalState.GrphDevMngr.GraphicsDevice, path)) { diskPath = path };
        }
    }
}
