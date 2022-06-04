﻿using MonoTest.Base.Scene;
using MonoTest.Base.State;
using MonoTest.Base.UI;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using MonoTest.Base.Primitives;
using MonoTest.Base.Graphics.Tests;
using MonoTest.Core.Physics;
using MonoTest.Base.Input;
using Microsoft.Xna.Framework.Input;

namespace MonoTest.Scenes
{
    class MainMenu : BaseScene
    {
        public UITree UITree = new UITree();
        public MainMenu() : base()
        {
            //AddDrawable(new SpriteRendererTest());
            var rect1 = new RectanglePrimv(Vector2.One * 50, Color.Orange);
            var rect2 = new RectanglePrimv(new Vector2(1500, 30), Color.Black);
            rect2.Transform.Position.Y = 700;

            var rect3 = new RectanglePrimv(new Vector2(20, 100), Color.Red);
            rect3.Transform.Position = new Vector2(230, 600);

            var rect4 = new RectanglePrimv(new Vector2(600, 30), Color.Blue);
            rect4.Transform.Position = new Vector2(-30, 570);

            var rect5 = new RectanglePrimv(new Vector2(10, 1000), Color.DarkGoldenrod);
            rect5.Transform.Position.X += 700;
            AddDrawable(rect5);
            SimulationFactory.GetDefault.AddFixed(rect5);

            AddDrawable(rect1);
            AddDrawable(rect2);
            AddDrawable(rect3);
            AddDrawable(rect4);

            Camera.Zoom = 0.5f;
            SimulationFactory.GetDefault.AddDynamic(rect1, true);
            KeyboardController.AddHandler(Keys.Q, (_) => rect1.Transform.Rotation -= 0.01f);
            KeyboardController.AddHandler(Keys.E, (_) => rect1.Transform.Rotation += 0.01f);
            KeyboardController.AddHandler(Keys.Left, (_) => rect1.Transform.Translate(-1, 0));
            KeyboardController.AddHandler(Keys.Right, (_) => rect1.Transform.Translate(1, 0));
            KeyboardController.AddHandler(Keys.Up, (_) => rect1.Transform.Translate(0, -1));
            KeyboardController.AddHandler(Keys.Down, (_) => rect1.Transform.Translate(0, 1));
            SimulationFactory.GetDefault.AddFixed(rect2);
            SimulationFactory.GetDefault.AddDynamic(rect3);
            SimulationFactory.GetDefault.AddDynamic(rect4);
            KeyboardController.AddHandler(Keys.A, (_) => Camera.Position.X += 4);
            KeyboardController.AddHandler(Keys.D, (_) => Camera.Position.X -= 4);
            KeyboardController.AddHandler(Keys.S, (_) => Camera.Position.Y -= 4);
            KeyboardController.AddHandler(Keys.W, (_) => Camera.Position.Y += 4);
        }

   }
}
