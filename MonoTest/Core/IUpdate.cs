using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Core
{
    public interface IUpdate
    {
        public void Initialize();
        public void Update(GameTime gameTime);
        public bool Enabled { get; set; }
    }
}
