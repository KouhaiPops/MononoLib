using FontStashSharp;
using FontStashSharp.Interfaces;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoTest.Base.Graphics.Vendor;
using MonoTest.Base.State;
using MonoTest.Base.Utils;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.UI
{
    public class Text : BaseElement, IDrawable
    {
        public DrawActions.PreDrawAction PreDraw { get; set; } = DrawBase.PreDraw;
        public DrawActions.PostDrawAction PostDraw { get; set; } = DrawBase.PostDraw;
        public ref Vector2 Position { get => ref _Position; }
        private Vector2 _Position = Vector2.Zero;
        public ref Color Color { get => ref _Color; }
        private Color _Color = Color.Black;

        //TODO signal the renderer to redraw the new text
        public string StringText { get => renderer.Text; set => renderer.SetText(value); }
        public string Font => fontProvider.Font;
        public int FontSize => fontProvider.FontSize;
        //private SpriteFontBase font;
        // TODO show allow generic to change the renderer type
        FontStashProvider fontProvider;
        private ITextRenderer<FontStashProvider> renderer;
        //private StepTextRenderer renderer;
        //private TextRenderer secondRenderer;
        private TimeSpan intervalPerCharacter = TimeSpan.Zero;
        public int CharactersPerSecond { get => 0; set => intervalPerCharacter = TimeSpan.FromSeconds(1f / value); }

        StepTextRenderer stepRenderer;
        public Text(string text = "Text", string fontname = FontUtils.DefaultFont, int fontSize = FontUtils.DefaultFontSize)
        {
            fontProvider = new FontStashProvider();
            renderer = new BaseTextRenderer<FontStashProvider>(GlobalState.GrphDevMngr.GraphicsDevice, fontProvider);
            SetFont(fontname, fontSize);
            StringText = text;

            stepRenderer = new StepTextRenderer(GlobalState.GrphDevMngr.GraphicsDevice, intervalPerCharacter);
            stepRenderer.SetFont(fontProvider.SpriteFont);
            stepRenderer.DrawText("Hello world", Vector2.One*20, Color.Black);
            // Should move to initialize


        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //PreDraw(spriteBatch, gameTime);
            //DEBUG LOGIC
            //if(Mouse.GetState().LeftButton == ButtonState.Pressed)
            //{
            //    renderer.Text = "Another message";
            //}


            //spriteBatch.DrawString(font, StringText, Position, Color);
            renderer.Draw(spriteBatch, gameTime);
            stepRenderer.ActualDraw(gameTime);
            //renderer.ActualDraw(gameTime);
            //secondRenderer.ActualDraw(gameTime);
            //PostDraw(spriteBatch, gameTime);
        }

        public override void Initialize()
        {
            //Position = new Vector2(10, 10);
            //CharactersPerSecond = 10;
            //var fontPath = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
            //GlobalState.FontManager.AddFont(File.ReadAllBytes(Path.Combine(fontPath, "Arial.ttf")));
            //font = GlobalState.FontManager.GetFont(48);
            //renderer = new TextRenderer(GlobalState.GrphDevMngr.GraphicsDevice, intervalPerCharacter);
            //secondRenderer = new TextRenderer(GlobalState.GrphDevMngr.GraphicsDevice, intervalPerCharacter, true);
            //renderer.SetFont(font);
            //secondRenderer.SetFont(font);

            //var msg = "hello world"; //world! this is a message!";
            //renderer.DrawText(msg, new Vector2(Position.X, Position.Y + 50), Color);
            //secondRenderer.DrawText(msg, Position, Color);
            ////font.DrawText(renderer, "Hello world", Position, Color);
            //renderer = new StepTextRenderer(GlobalState.GrphDevMngr.GraphicsDevice, intervalPerCharacter);
        }

        public override void Update(GameTime gameTime)
        {
            //var f = Math.Abs((float)Math.Sin(gameTime.TotalGameTime.TotalSeconds))*60;
            //font = GlobalState.FontManager.GetFont((int)f);
            //throw new NotImplementedException();
        }
        public bool SetFont(string fontname, int fontSize = FontUtils.DefaultFontSize)
        {
            return fontProvider.UpdateFont(fontname, fontSize);
            //var font = FontUtils.GetFont(fontname, fontSize);
            //if(font == null)
            //{
            //    return false;
            //}
            //this.font = font;
            //Font = fontname;
            //FontSize = fontSize;
            //return true;
        }

        /// <summary>
        /// Set the current FontSystem's fontsize, this doesn't downscale, instead it relies on the renderer
        /// </summary>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        public bool SetFontSize(int fontSize)
        {
            return fontProvider.UpdateFontSize(fontSize);
            //if(fontSize == FontSize)
            //{
            //    return true;
            //}
            //var font = FontUtils.GetFont(Font, fontSize);
            //if(font == null)
            //{
            //    return false;
            //}
            //this.font = font;
            //FontSize = fontSize;
            //return true;
        }

        /// <summary>
        /// A glyph renderer that allows control over individual characters
        /// </summary>
        private class StepTextRenderer : IFontStashRenderer
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
                        middle = Vector2.One*-50;
                    }
                    var t = Origin;
                    if(firstPass)
                        Console.WriteLine(Origin);
                    //Origin = Origin + displacment;
                    spriteBatch.Draw(texture,
                    (Pos-Origin),
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
            public string Text { get => text; set
            {
                text = value;
                redraw = true;
            } }

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

                steps.Add(new Step(text[characterCount-1],pos,
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
                for(int i = 0; i < steps.Count; i++)
                {
                    steps[0].TotalMiliseoncds = intervals.TotalMilliseconds * i;
                }

            }
            public void ActualDraw(GameTime gameTime)
            {
                if(redraw)
                {
                    Reset();
                    fontBase.DrawText(this, text, Position, Color);
                    redraw = false;
                }

                totalElappsedMilliseconds += gameTime.ElapsedGameTime.TotalMilliseconds;

                List<Step> toRemove = new List<Step>();
                foreach(var step in steps)
                {
                    if(step.TotalMiliseoncds <= totalElappsedMilliseconds)
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
                foreach(var t in toRemove)
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
}
