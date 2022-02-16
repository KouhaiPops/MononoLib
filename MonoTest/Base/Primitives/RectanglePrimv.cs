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
    public class RectanglePrimv : BaseElement, IDrawable
    {
        private Renderer2D renderer;
        private bool init = false;
        private int height;
        private int width;
        private Color strokeColor;
        private int thickness;
        private Color fill;

        public RectanglePrimv(int height, int width, Color strokeColor) : this(height, width, strokeColor, 1)
        {

        }

        public RectanglePrimv(int height, int width, Color strokeColor, Vector2 position, int thickness = 1, Color fill = default) : this(height, width, strokeColor, thickness, fill)
        {
            Transform.Position = position;
        }

        public RectanglePrimv(int height, int width, Color strokeColor, int thickness) : this(height, width, strokeColor, thickness, Color.Transparent)
        {

        }

        public RectanglePrimv(int height, int width, Color strokeColor, int thickness, Color fill)
        {
            this.height = height;
            this.width = width;
            this.strokeColor = strokeColor;
            this.thickness = thickness;
            this.fill = fill;
            Transform.Size = new Vector2(width+1, height+1);
            renderer = new(this);
            AddComponent(renderer);
            init = true;
            Initialize();
        }

        internal void SetDimension(Vector2 vector2)
        {
            height = (int)vector2.Y;
            width = (int)vector2.X;
            init = true;
            Transform.Size = new Vector2(width + 1, height + 1);
            Initialize();
        }

        public DrawActions.PreDrawAction PreDraw { get; set; } = DrawBase.PreDraw;
        public DrawActions.PostDrawAction PostDraw { get; set; } = DrawBase.PostDraw;

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
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
            var x = 0;
            var y = 0;

            for(int i = 0; i < thickness; i++)
            {
                renderer.DrawHorizontalLine(x + i, x + width - i, y + i, strokeColor);
                renderer.DrawHorizontalLine(x + i, x + (width) - i, y + height - i, strokeColor);

                renderer.DrawVerticalLine(x+i, y + i, y + height - i, strokeColor);
                renderer.DrawVerticalLine(x + width - i, y + i, y + height - i, strokeColor);
            }
            if(fill != Color.Transparent)
            {
                x = thickness;
                y = thickness;
                var innerRectWidth = width - thickness;
                var innerRectHeight = height - thickness;
                if (innerRectWidth <= 0 || innerRectHeight <= 0)
                    return;
                while (true)
                {
                    renderer.SetPixel(x, y, fill);
                    x++;
                    if (x > innerRectWidth)
                    {
                        if (y == innerRectHeight) break;

                        y++;
                        x = thickness;
                    }
                }
            }
            renderer.End();


        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
