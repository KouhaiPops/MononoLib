using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoTest.Base.Component;
using MonoTest.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Primitives
{
    public class StraightLine : BaseElement, IDrawable
    {
        private Renderer2D renderer;
        private bool init = false;
        private Vector2i start;
        private Vector2i end;
        private Color color;
        private int thickness;

        public DrawActions.PreDrawAction PreDraw { get; set; } = DrawBase.PreDraw;
        public DrawActions.PostDrawAction PostDraw { get; set; } = DrawBase.PostDraw;

        public StraightLine(Vector2i start, Vector2i end, Color color, int thickness = 1)
        {
            this.start = start;
            this.end = end;
            this.color = color;
            this.thickness = thickness;
            var delta = start - end;
            delta.Abs();
            Transform.Size = delta;
            Transform.Size.Y += thickness;
            
            init = true;
            renderer = new Renderer2D(this);
            AddComponent(renderer);
            Initialize();
        }


        public void Draw(SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime)
        {
            PreDraw(spriteBatch, gameTime);
            renderer.Render(spriteBatch);
            PostDraw(spriteBatch, gameTime);
        }

        public override void Initialize()
        {
            if (!init)
                return;
            renderer.Begin();
            var deltaX = Math.Abs(end.X - start.X);
            var deltaY = -Math.Abs(end.Y - start.Y);

            var strideX = start.X < end.X ? 1 : -1;
            var strideY = start.Y < end.Y ? 1 : -1;

            var err = deltaX + deltaY;


            var x = start.X;
            var y = start.Y;
            while(true)
            {
                if (x == end.X && y == end.Y) break;
                for(int i = 0; i < thickness; i++)
                {
                    renderer.SetPixel(x, y+i, color);
                }
                var errorTimeTwos = err * 2;
                if(errorTimeTwos >= deltaY)
                {
                    err += deltaY;
                    x += strideX;
                }
                if(errorTimeTwos <= deltaX)
                {
                    err += deltaX;
                    y += strideY;
                }

            }
            renderer.End();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

        }
    }
}
