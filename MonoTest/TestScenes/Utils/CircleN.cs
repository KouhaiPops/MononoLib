using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoTest.Base;
using MonoTest.Base.Component;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.TestScenes.Utils
{
    public class CircleN : BaseElement, Base.IDrawable, IUpdateable<CircleN>
    {
        private Renderer2D renderer;
        private int _thickness;
        private bool shouldInit;
        public int Radius { get; }
        public DrawActions.PreDrawAction PreDraw { get; set; }
        public DrawActions.PostDrawAction PostDraw { get; set; }
        public Action<CircleN> OnUpdate { get; set; }

        public List<IUpdateBehaviour<CircleN>> UpdateBehaviours { get; } = new List<IUpdateBehaviour<CircleN>>();

        public CircleN(int radius, int thickness, Vector2 position)
        {
            if (thickness <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(thickness), $"Argument {nameof(thickness)} should be in range 1 and MaxInt");
            }
            if (radius <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(radius), $"Argument {nameof(radius)} should be in range 1 and MaxInt");
            }

            Transform.Position = position;
            var referencePoint = (radius * 2) + 1;
            Transform.Size = new Vector2(referencePoint, referencePoint);
            renderer = new(this);
            _thickness = thickness;
            Radius = radius;
            shouldInit = true;
        }
        public CircleN(int radius, int thickness = 1) : this(radius, thickness, Vector2.Zero) {   }

        public void Draw(SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime)
        {
            renderer.Render(spriteBatch);
        }

        public override void Initialize()
        {
            if(!shouldInit)
            {
                return;
            }
            renderer.Begin();
            foreach (var (x, y) in GraphicPrimitives.Primitives.DrawCircleBuffer(0, 0, Radius, _thickness))
            {
                renderer.SetPixel(x, y, Color.Black);
            }
            renderer.End();

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            OnUpdate?.Invoke(this);
            foreach(var updateBehaviour in UpdateBehaviours)
            {
                updateBehaviour.OnUpdate(this);
            }
        }
    }
}
