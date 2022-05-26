using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoTest.Base.Component;
using MonoTest.Base.MathCore;
using MonoTest.Base.State;
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
        private VertexPositionColor[] Vertices = new VertexPositionColor[2];
        private Renderer2D renderer;
        private bool init = false;
        private Vector2i start;
        private Vector2i end;
        private Color color;
        private int thickness;
        private Vector3 normalizationVector;

        public DrawActions.PreDrawAction PreDraw { get; set; }
        public DrawActions.PostDrawAction PostDraw { get; set; }

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

            Vertices[0] = new VertexPositionColor(start.MapToWindow(), color);
            Vertices[1] = new VertexPositionColor(end.MapToWindow(), color);
        }

        public void SetEnd(Vector2 end)
        {
            Vertices[1].Position = end.MapToWindow();
        }

        public void Draw(SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime)
        {
            BasicEffect effect = new BasicEffect(spriteBatch.GraphicsDevice)
            {
                World = Matrix.CreateTranslation(new Vector3(0, 0, 0)),
                VertexColorEnabled = true
            };
            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                spriteBatch.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(
                    PrimitiveType.LineList, Vertices, 0, Vertices.Length / 2);
            }
        }

        public override void Initialize()
        {

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

        }
    }
}
