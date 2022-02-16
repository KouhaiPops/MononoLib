using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoTest.Base;
using MonoTest.Base.Scene;
using MonoTest.Base.State;
using MonoTest.TestScenes.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.TestScenes
{
    internal class CircleScene : BaseScene
    {
        public CircleScene()
        {
            var centerX = GlobalState.WindowBounds.Width / 2;
            var centerY = GlobalState.WindowBounds.Height / 2;
            //for (int i = 20; i <= 350; i += 5)
            //{
            //    var radius = i + 10;
            //    var circle = new CircleN(radius, 8, new Vector2(centerX, centerY));
            //    circle.Transform.Origin = Vector2.One * radius;
            //    circle.UpdateBehaviours.Add(new CircleMouseBehaviour());
            //    AddDrawable(circle);
            //}
            var _circle = new CircleN(15, 10, new Vector2(centerX, centerY));
            AddDrawable(_circle);
            foreach(var circle in Drawables)
            {
                var element = circle as BaseElement;
                element.Initialize();
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            base.Draw(spriteBatch, gameTime);
            spriteBatch.End();
        }
    }
}
