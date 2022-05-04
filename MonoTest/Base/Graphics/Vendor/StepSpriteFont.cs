using FontStashSharp;
using FontStashSharp.Interfaces;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoTest.Base.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Graphics.Vendor
{

    /// <summary>
    /// A glyph renderer that allows control over individual characters
    /// </summary>
    internal class StepTextRenderer : IFontStashRenderer
    {
        private class Step
        {
            public char Character { get; set; }
            public double TotalMiliseoncds { get; set; }
            public bool Center { get; private set; }

            private bool firstPass;

            public Vector2 Pos { get; set; }
            public Rectangle Src { get; set; }
            public Color Color { get; set; }
            public float Rotation { get; set; }
            public Vector2 Origin { get; set; }
            public Vector2 Scale { get; set; }
            public float Depth { get; set; }

            public Step(char character, Vector2 pos, Rectangle src, Color color, float rotation, Vector2 origin, Vector2 scale, float depth, ref double totalMilliSeconds, bool center = false)
            {
                Character = character;
                Pos = pos;
                Src = src;
                Color = color;
                Rotation = rotation;
                Origin = origin;
                Scale = scale;
                Depth = depth;
                TotalMiliseoncds = totalMilliSeconds;
                Center = center;
                firstPass = true;
            }

            public void Draw(SpriteBatch spriteBatch, Texture2D texture)
            {
                var middle = new Vector2(Src.Width / 2, Src.Height / 2);
                if (!Center)
                {
                    middle = Vector2.One * -50;
                }
                var t = Origin;
                if (firstPass)
                    Console.WriteLine(Origin);
                //Origin = Origin + displacment;
                spriteBatch.Draw(texture,
                (Pos - Origin),
                Src,
                Color,
                Rotation,
                Vector2.Zero,
                Scale,
                SpriteEffects.None,
                Depth);
                Origin = t;
                firstPass = false;
                //spriteBatch.Draw(rectTexture, position, Color.White);
            }

            public Step Rotate(float rotation)
            {
                Rotation = rotation;
                return this;
            }
        }
        public GraphicsDevice GraphicsDevice { get; }
        public bool SeperateCharacters { get; set; }
        public TimeSpan Intervals { get; private set; }
        public int FontSize { get; set; }
        public string Text
        {
            get => text; set
            {
                text = value;
                redraw = true;
            }
        }

        public Vector2 Position { get; private set; }
        public Color Color { get; private set; }

        private SpriteBatch _spriteBatch;

        private Texture2D texture;
        private List<Step> drawnSteps = new List<Step>();
        private List<Step> steps = new List<Step>();
        private int characterCount;
        private double totalElappsedMilliseconds = 0;
        private SpriteFontBase fontBase;
        private String text = "";
        private bool redraw = true;

        public StepTextRenderer(GraphicsDevice graphicsDevice)
        {
            GraphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public StepTextRenderer(GraphicsDevice graphicsDevice, TimeSpan intervals, bool seperateCharacters = false) : this(graphicsDevice)
        {
            Intervals = intervals;
            SeperateCharacters = seperateCharacters;
            SetFont(FontUtils.GetFont());
        }

        public StepTextRenderer(GraphicsDevice graphicsDevice, TimeSpan intervals, SpriteFontBase spriteFont, bool seperateCharacters = false) : this(graphicsDevice, intervals, seperateCharacters)
        {

        }
        //public TextRenderer(GraphicsDevice graphicsDevice, TimeSpan intervals, bool seperateCharacters = false)
        //{
        //    this.seperateCharacters = seperateCharacters;
        //    GraphicsDevice = graphicsDevice;
        //    _spriteBatch = new SpriteBatch(graphicsDevice);
        //    this.intervals = intervals;
        //}

        public void SetFont(SpriteFontBase font)
        {
            fontBase = font;
            FontSize = font.FontSize;
        }

        public void DrawText(string text, Vector2 position, Color color)
        {
            Text = text;
            Position = position;
            Color = color;
        }

        public void Reset()
        {
            texture = null;
            steps = new List<Step>();
            drawnSteps = new List<Step>();
            characterCount = 0;
        }

        public void Draw(Texture2D texture, Vector2 pos, Rectangle? src, Color color, float rotation, Vector2 origin, Vector2 scale, float depth)
        {
            this.texture = texture;
            var abs = Intervals.TotalMilliseconds * characterCount++;

            steps.Add(new Step(text[characterCount - 1], pos,
                src.Value,
                color,
                rotation,
                origin,
                scale,
                depth,
                ref abs,
                SeperateCharacters));
        }
        public void AppendReactive(string text)
        {

        }

        public void SetIntervals(TimeSpan intervals)
        {
            Intervals = intervals;
            totalElappsedMilliseconds = 0;
            //TODO updating existing ones might not be necessery if we don't store state in the step itself
            for (int i = 0; i < steps.Count; i++)
            {
                steps[0].TotalMiliseoncds = intervals.TotalMilliseconds * i;
            }

        }
        public void ActualDraw(GameTime gameTime)
        {
            if (redraw)
            {
                Reset();
                fontBase.DrawText(this, text, Position, Color);
                redraw = false;
            }

            totalElappsedMilliseconds += gameTime.ElapsedGameTime.TotalMilliseconds;

            List<Step> toRemove = new List<Step>();
            foreach (var step in steps)
            {
                if (step.TotalMiliseoncds <= totalElappsedMilliseconds)
                {
                    drawnSteps.Add(step);
                    toRemove.Add(step);
                }
                else
                {
                    break;
                }
            }

            // TODO Instead of using a temp array, slice by the last index, which will always work
            foreach (var t in toRemove)
            {
                steps.Remove(t);
            }

            _spriteBatch.Begin();
            foreach (var prev in drawnSteps)
            {
                prev.Draw(_spriteBatch, texture);
            }
            _spriteBatch.End();
        }
    }
}
