using MonoTest.Base.Scene;
using MonoTest.Base.State;
using MonoTest.Base.UI;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using MonoTest.Base.Primitives;
using MonoTest.Base.Graphics.Tests;

namespace MonoTest.Scenes
{
    class MainMenu : BaseScene
    {
        public UITree UITree = new UITree();
        public MainMenu() : base()
        {
            AddDrawable(new SpriteRendererTest());
        }

   }
}
