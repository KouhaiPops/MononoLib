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
        public UITree UITree = new UITree();
        public MainMenu() : base()
        {
            //AddDrawable(new Background(GlobalState.GrphDevMngr.GraphicsDevice, new Vector2(250, 250), new Vector2(0, 0), Color.Red));
            //AddDrawable(new Text());

            //AddDrawable(new Button(400, 400));
            //var line = new StraightLine(new Core.Vector2i(0, 0), Core.Vector2i.Unit * 50, Color.Blue);
            //var line = new StraightLine(new Core.Vector2i(0, 0), Core.Vector2i.Unit * 50, Color.Blue);
            //AddDrawable(line);
            //base.OnMouseHold += (mousePos) =>
            //{
            //    line.SetEnd(mousePos);
            //};
            //var tex = new DrawableTexture(Game1.CurrentTexture, Vector2.Zero);
            //AddDrawable(tex);
            //OnMouseHold += (mousePos) => tex.Transform.Position = mousePos;
            //AddDrawable(new BaseText("Hello, world!"));
            var button = new Button(200, 150, Color.Orange);
            button.OnEnter += () => { Console.WriteLine("Entered"); return true; };
            button.OnClick += () => { Console.WriteLine("Click"); return true; };
            button.OnRelease += () => { Console.WriteLine("Release"); return true; };
            button.OnExit += () => { Console.WriteLine("Exit"); return true; };
            //button.OnEnter += () => { Console.WriteLine("Hover"); return true; };
            AddDrawable(
                UITree.AddControl(
                        button
                    )
                );
        }

   }
}
