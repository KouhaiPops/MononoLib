using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoTest.Base.State;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Component
{
    public class Renderer2D : IComponent
    {
        private Texture2D backingTexture;
        private Color[] backingData;
        private IElement parent;
        private bool initialized;
        private int currentPercentage;
        private int lastPosition;

        public IElement Parent { get => parent; set { if (parent == null) parent = value; } }

        public Renderer2D()
        {

        }
        public Renderer2D(IElement parent)
        {
            this.parent = parent;
        }

        public void Begin()
        {
            initialized = false;
            backingTexture = new Texture2D(GlobalState.GrphDevMngr.GraphicsDevice, (int)parent.Transform.Size.X, (int)parent.Transform.Size.Y);
            backingData = new Color[backingTexture.Width * backingTexture.Height];
        }

        public void SetPixel(int x, int y, Color color)
        {
            backingData[x + (y * backingTexture.Width)] = color;
        }

        public void SetPixel(float x, float y, Color color)
        {
            backingData[(int)x + ((int)y * backingTexture.Width)] = color;
        }


        public void DrawVerticalLine(float x, float y1, float y2, Color color, bool onlyIfDefault = false)
        {
            var tempY1 = y1;
            y1 = y1 < y2 ? y1 : y2;
            if(tempY1 != y1)
            {
                y2 = tempY1;
            }
            while(y1 < y2)
            {
                if (backingData[(int)x + ((int)y1 * backingTexture.Width)] != default && onlyIfDefault)
                {
                    y1++;
                    continue;

                    //var current = backingData[(int)x + ((int)y1 * backingTexture.Height)];
                    //if(current == Color.Red || color == Color.Red)
                    //{
                    //    backingData[(int)x + ((int)y1 * backingTexture.Height)] = Color.DarkBlue;
                    //}
                    //else
                    //{
                    //    backingData[(int)x + ((int)y1 * backingTexture.Height)] = Color.BlueViolet;
                    //}
                }
                backingData[(int)x + ((int)y1 * backingTexture.Width)] = color;
                y1++;
            }
        }

        public void DrawHorizontalLine(float x1, float x2, float y, Color color, bool onlyIfDefault = false)
        {
            var tempX1 = x1;
            x1 = x1 < x2 ? x1 : x2;
            if(tempX1 != x1)
            {
                x2 = tempX1;
            }
            while(x1 < x2)
            {
                if(backingData[(int)x1 + ((int)y * backingTexture.Width)] != default && onlyIfDefault)
                {
                    x1++;
                    continue;
                }
                backingData[(int)x1 + ((int)y * backingTexture.Width)] = color;
                x1++;
            }
        }

        internal void Fill(Color color)
        {
            for(int i = 0; i < backingData.Length; i++)
            {
                backingData[i] = color;
            }
        }

        internal void FillPartial(Color color, int percentage)
        {
            currentPercentage += percentage;

            if (currentPercentage > 100)
                return;

            var end = (int)(backingData.Length * (currentPercentage / 100f));
            for (int i = lastPosition; i < end; i++)
            {
                backingData[i] = color;
            }
            lastPosition = (int)end;
        }

        public bool End()
        {
            try
            {
                backingTexture.SetData(backingData);
                initialized = true;
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public void Render(SpriteBatch spriteBatch)
        {
            if(!initialized)
            {
                if(!End())
                {
                    throw new AccessViolationException("Tried to set the data to the texture but wasn't allowed");
                }
            }
            spriteBatch.Draw(backingTexture, parent.Transform.Position, null, Color.White, 0, parent.Transform.Origin, parent.Transform.Scale, SpriteEffects.None, 0);

        }
    }
}
