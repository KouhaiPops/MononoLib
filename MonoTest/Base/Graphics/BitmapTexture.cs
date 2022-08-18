using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoTest.Base.Effects;
using MonoTest.Base.State;
using MonoTest.Base.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Graphics
{
    public class BitmapTexture : BaseElement, IDrawable
    {
        public DrawActions.PreDrawAction PreDraw { get; set; }
        public DrawActions.PostDrawAction PostDraw { get; set; }
        public int Width { get; }
        public int Height { get; }
        private Texture2D texture;
        BitTex shader = new BitTex();

        public BitmapTexture(int width, int height)
        {
            Width = width;
            Height = height;
            texture = new Texture2D(GlobalState.GrphDevMngr.GraphicsDevice, width, height);
            var color = new Color[width * height];
            texture.SetData(color);
            shader.Apply();
        }

        public void Clear()
        {
            texture.SetData(new Color[texture.Width * texture.Height]);
        }

        public void SetPixel(int x, int y, Color color, int size = 1)
        {
            var position = x + (y * texture.Width);
            if (position < 0 || position >= texture.Width * texture.Height)
                return;
            var sizeY = size;
            var sizeX = size;
            try
            {
                var delta = texture.Height - y;
                if(delta < sizeY)
                {
                    sizeY = delta;
                }
                var colorArray = new Color[sizeX*sizeY];
                Array.Fill(colorArray, color);
                texture.SetData(0, new Rectangle(x, y, sizeX, sizeY), colorArray, 0, sizeX*sizeY);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while setting color at position {position} (X: {x}, Y: {y})");
            }
        }

        public void SetPixelRelative(int x, int y, Color color, int size = 1)
        {
            SetPixel((int)(x - Transform.Position.X), (int)(y - Transform.Position.Y), color, size);
        }

        public void ClearPixel(int x, int y, int size = 1)
        {
            var position = x + (y * texture.Width);
            if (position < 0 || position >= texture.Width * texture.Height)
                return;

            var sizeY = size;
            var sizeX = size;
            try
            {
                var delta = texture.Height - y;
                if (delta < sizeY)
                {
                    sizeY = delta;
                }
                var colorArray = new Color[sizeX * sizeY];
                texture.SetData(0, new Rectangle(x, y, sizeX, sizeY), colorArray, 0, sizeX * sizeY);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while clearing at position {position} (X: {x}, Y: {y})");
            }
            //try
            //{
            //    texture.SetData(0, new Rectangle(x, y, size, size), new Color[size * size], 0, size * size);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Error while clearing at position {position} (X: {x}, Y: {y})");
            //}
        }


        public void ClearPixelRelative(int x, int y, int size = 0)
        {
            ClearPixel((int)(x - Transform.Position.X), (int)(y - Transform.Position.Y), size);
        }
        public void Draw(SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime)
        {
            spriteBatch.End();
            spriteBatch.Begin(effect: shader.Effect, blendState: BlendState.NonPremultiplied);
            shader.Apply();
            spriteBatch.DrawGenericTexture(texture, this, Color.White);
        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }
    }
}
