using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoTest.Base.Component;
using MonoTest.Base.Effects;
using MonoTest.Base.State;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Primitives
{
    public class Circle : BaseElement, IDrawable
    {
        private BoxBlur blur { get; set;  }
        public ref int StrokeWidth { get => ref _StrokeWidth; }

        private int _StrokeWidth;
        public ref Color StrokeColor { get => ref _StrokeColor; }

        private Color _StrokeColor;

        public ref Color Fill { get => ref _Fill; }

        private Color _Fill;

        private Renderer2D renderer;
        private bool isConstructed = false;
        public DrawActions.PreDrawAction PreDraw { get; set; } = DrawBase.PreDraw;
        public DrawActions.PostDrawAction PostDraw { get; set; } = DrawBase.PostDraw;

        public void Draw(SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime)
        {
            PreDraw(spriteBatch, gameTime, blur);
            renderer.Render(spriteBatch);
            PostDraw(spriteBatch, gameTime);
        }

        public int Radius { get; }

        public Circle SetPos(Vector2 pos)
        {
            Transform.Position = pos;
            return this;
        }

        public Circle(int radius) : this(radius, 1)
        {
        }

        public Circle(int radius, int strokeWidth) : this(radius, strokeWidth, Color.Black)
        {
        }

        public Circle(int radius, int strokeWidth, Color strokeColor) : this(radius, strokeWidth, strokeColor, Color.Transparent)
        {
        }

        public Circle(int radius, int strokeWidth, Color strokeColor, Color fill)
        {
            blur = new BoxBlur(Vector2.Zero);
            _StrokeWidth = strokeWidth;
            _StrokeColor = strokeColor;
            _Fill = fill;

            renderer = new()
            {
                Parent = this
            };
            AddComponent(renderer);
            Radius = radius;
            isConstructed = true;
            Initialize();

        }

        public override void Initialize()
        {
            if (!isConstructed)
                return;

            var shouldFill = _Fill != Color.Transparent;
            var referencePoint = (Radius * 2) + 1;
            Transform.Size = new Vector2(referencePoint, referencePoint);
            blur.Size = Transform.Size;
            renderer.Begin();
            var center = new Vector2((referencePoint - 1) / 2, (referencePoint - 1) / 2);
            var currentPoint = new Vector2(0, Radius);
            var innerPoint = new Vector2(0, Radius - _StrokeWidth);
            var p = 1 - currentPoint.Y; // Radius or point.y
            var p1 = 1 - innerPoint.Y; // Radius or point.y

            //var plotPoint = center;


            while (currentPoint.X <= currentPoint.Y)
            {
                #region Comment
                // In an array, Y is larger when we go DOWN
                //              X is larger when we go RIGHT
                // To normalize that, and center around a point
                // We add the center's X to the current point's X
                // And that works correctly, because if the curernt point's X goes negative
                // Then adding it to the center point would cause it to to go to the LEFT
                // Which in a cartesian set is valid

                // We change the symbol of the current point's Y and THEN add the center's Y to it
                // This way, when the current point's Y goes UP, it actually goes DOWN, and vise versa
                // In fact, the logic is simplified by just subtracting the center's Y from the current point's Y
                /*
                 *
                 *
                 *
                     0   0  0  0
                     1   0  1  0
                     2   0  0  0

                         0  1  2
                ======================
                     1   0  0  0
                     0   0  1  0
                    -1   0  0  0
    
                        -1  0  1
                 *
                 *
                 */
                #endregion
                #region Draw outer Dots
                var drawPoint = new Vector2(currentPoint.X + center.X, center.Y - currentPoint.Y);
                var y2 = center.Y + currentPoint.Y;
                var x2 = -currentPoint.X + center.X;
                currentPoint.X++;
                #endregion

                #region Draw inner
                var drawPoint2 = new Vector2(innerPoint.X + center.X, center.Y - innerPoint.Y);
                var iY2 = center.Y + innerPoint.Y;
                #endregion

                #region Fill
                if (shouldFill)
                {
                    renderer.DrawHorizontalLine(drawPoint2.Y, iY2, drawPoint.X, Fill, shouldFill);
                    renderer.DrawHorizontalLine(drawPoint2.Y, iY2, x2, Fill, shouldFill);

                    renderer.DrawVerticalLine(drawPoint.X, drawPoint2.Y, iY2, Fill, shouldFill);
                    renderer.DrawVerticalLine(x2, drawPoint2.Y, iY2, Fill, shouldFill);

                }
                #endregion

                #region Draw Stroke
                renderer.DrawVerticalLine(drawPoint.X - 1, drawPoint.Y, drawPoint2.Y, StrokeColor); // 1
                renderer.DrawVerticalLine(drawPoint.X - 1, y2, iY2, StrokeColor); // 4
                renderer.DrawVerticalLine(x2, drawPoint.Y, drawPoint2.Y, StrokeColor); // 8
                renderer.DrawVerticalLine(x2, y2, iY2, StrokeColor); // 5

                renderer.DrawHorizontalLine(drawPoint.Y, drawPoint2.Y, drawPoint.X - 1, StrokeColor); // 6
                renderer.DrawHorizontalLine(y2, iY2, drawPoint.X - 1, StrokeColor); // 3
                renderer.DrawHorizontalLine(drawPoint.Y, drawPoint2.Y, x2, StrokeColor); // 7
                renderer.DrawHorizontalLine(y2, iY2, x2, StrokeColor); // 2
                #endregion



                #region Determinator
                if (p < 0)
                {
                    p += (2 * (int)currentPoint.X) + 1;
                }
                else
                {
                    currentPoint.Y--;
                    //p -= (2 * (int)currentPoint.Y) - (2 * (int)currentPoint.X) + 1;
                    p += 2 * (currentPoint.X - currentPoint.Y) + 1;
                }

                if (p1 < 0)
                {
                    p1 += (2 * (int)currentPoint.X) + 1;
                }
                else
                {
                    innerPoint.Y--;
                    p1 += 2 * (currentPoint.X - innerPoint.Y) + 1;
                }

                // TODO moving this under innerPoint.Y solves double slits problem
                if (currentPoint.X >= innerPoint.Y)
                {
                    innerPoint.Y = currentPoint.X - 1;
                }
                #endregion
            }
            renderer.End();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

        }
    }
}