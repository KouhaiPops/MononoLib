using MonoTest.Base.Scene;
using MonoTest.Base.State;
using MonoTest.Base.UI;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoTest.Scenes
{
    class MainMenu : BaseScene
    {
        public MainMenu() : base()
        {
            AddDrawable(new Background(GlobalState.GrphDevMngr.GraphicsDevice, new Vector2(250, 250), new Vector2(0, 0), Color.Red));
            AddDrawable(new Text());
            var circle = new Base.Primitives.Circle(41, 12, Color.Green, Color.Red);
            bool shouldFill = false;
            AddDrawable(circle);
            OnMouseClick += (v) =>
            {
                RemoveDrawable(circle);
                shouldFill = !shouldFill;
                circle = shouldFill ? new Base.Primitives.Circle(41, 12, Color.Blue, Color.Red) : new Base.Primitives.Circle(41, 12, Color.Blue);
                AddDrawable(circle);
            };
        }
    }
}
