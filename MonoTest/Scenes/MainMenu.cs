using MonoTest.Base.Scene;
using MonoTest.Base.State;
using MonoTest.Base.UI;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using MonoTest.Base.Primitives;

namespace MonoTest.Scenes
{
    class MainMenu : BaseScene
    {
        public MainMenu() : base()
        {
            AddDrawable(new Background(GlobalState.GrphDevMngr.GraphicsDevice, new Vector2(250, 250), new Vector2(0, 0), Color.Red));
            AddDrawable(new Text());
            //var line = new StraightLine(new Core.Vector2i(0, 0), new Core.Vector2i(255, 255), Color.Black);
            //var thickness = 1;
            //AddDrawable(line);
            //RectanglePrimv current = null;
            //Vector2 startPoint = default;
            //OnMouseClick += (v) =>
            //{
            //    startPoint = v;
            //};

            //OnMouseHold += (v) =>
            //{
            //    if (current == null)
            //    {
            //        current = new RectanglePrimv((int)(v.X - startPoint.X),  (int)(v.Y - startPoint.Y), Color.Black, startPoint, 1, Color.Black);
            //        AddDrawable(current);
            //    }
            //    else
            //    {
            //        current.SetDimension(v-startPoint);
            //    }
            //};

            //OnMouseRelease += (_) =>
            //{
            //    current = null;
            //};
            //AddDrawable(new RectanglePrimv(100, 100, Color.Blue, new Vector2(100, 100), 3, Color.Red));
            //OnMouseHold += (v) =>
            //{
            //    AddDrawable(new Circle(25, 5, Color.Violet, Color.Violet).SetPos(v));
            //};

            //OnScroll += (s) =>
            //{
            //    RemoveDrawable(line);
            //    thickness = s == ScrollEvent.Down ? thickness == 1 ? thickness : thickness - 1 : thickness + 1;
            //    line = new StraightLine(new Core.Vector2i(0, 0), new Core.Vector2i(255, 255), Color.Black, thickness);
            //    AddDrawable(line);
            //};
        }
    }
}
