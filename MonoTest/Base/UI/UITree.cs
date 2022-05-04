using Microsoft.Xna.Framework.Graphics;

using MonoTest.Base.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.UI
{
    public class UITree : GraphicGroup<IUI>
    {

        public UITree AddControl(IUI control)
        {
            Drawables.Add(control);
            return this;
        }

    }
}
