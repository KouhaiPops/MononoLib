using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoTest.Base.Component;
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
            end.Y *= -1;
            this.start = start;
            this.end = end;
            this.color = color;
            this.thickness = thickness;
            var delta = start - end;
            delta.Abs();
            Transform.Size = delta;
            Transform.Size.Y += thickness;
            normalizationVector = new Vector3(GlobalState.WindowBounds.Size.ToVector2()/2f, 1);
            var v1 = (start-normalizationVector) / normalizationVector * 1;
            v1.Z *= -1;
            v1.Y *= -1;
            var v2 = end / normalizationVector;
            Vertices[0] = new VertexPositionColor(v1, color);
            Vertices[1] = new VertexPositionColor(v2, color);
            //init = true;
            //renderer = new Renderer2D(this);
            //AddComponent(renderer);
            //Initialize();
        }

        public void SetEnd(Vector2 end)
        {
            var v2 = (new Vector3(end, 1) - normalizationVector)/normalizationVector;
            v2.Y *= -1;
            Vertices[1].Position = v2;
        }

        public void Draw(SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime)
        {
            //spriteBatch.GraphicsDevice.
            BasicEffect effect = new BasicEffect(spriteBatch.GraphicsDevice);
            effect.World = Matrix.CreateTranslation(new Vector3(0, 0, 0));
            //var viewMatrix = Matrix.Create(0, 0, 50, 100);
            //var v = new Viewport().
            //effect.Projection = viewMatrix;
            //effect.View = Matrix.CreateRotationX(1.5f);
            //effect.World = Matrix.CreateTranslation(-1f, -1f, 0) + Matrix.CreateRotationX(1.2f);
            //effect.World = Matrix.CreateRotationX(MathHelper.ToRadians(80)) * Matrix.CreateTranslation(-1, -1f, 0);
            effect.VertexColorEnabled = true;
            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                spriteBatch.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(
                    PrimitiveType.LineList, Vertices, 0, Vertices.Length/2);
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
