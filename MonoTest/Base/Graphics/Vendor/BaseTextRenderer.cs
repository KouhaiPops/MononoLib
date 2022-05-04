﻿using FontStashSharp.Interfaces;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoTest.Base.Component;
using MonoTest.Base.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Graphics.Vendor
{
    public class BaseTextRenderer<T> : IFontStashRenderer, ITextRenderer<T> where T : FontStashProvider
    {
        private string text;
        public string Text => text;
        List<Glyph> glyphs = new List<Glyph>();
        public GraphicsDevice GraphicsDevice { get; }
        public DrawActions.PreDrawAction PreDraw { get; set; }
        public DrawActions.PostDrawAction PostDraw { get; set; }
        public Transform Transform { get; set; } = new Transform();
        private Texture2D tex;
        private T _fontProvider;
        private bool redraw = true;
        public BaseTextRenderer(GraphicsDevice device, T fontProvider)
        {
            GraphicsDevice = device;
            _fontProvider = fontProvider;
            fontProvider.OnFontChange += UpdateFont;
            KeyboardController.AddHandler(Microsoft.Xna.Framework.Input.Keys.Up, (_) => fontProvider.UpdateFontSize(fontProvider.FontSize + 1));
        }
        public void Draw(Texture2D texture, Vector2 pos, Rectangle? src, Color color, float rotation, Vector2 origin, Vector2 scale, float depth)
        {
            if(redraw)
            {
                tex = texture;
            }
            glyphs.Add(new Glyph(pos-origin, rotation, color, Vector2.Zero, scale, depth, src));
            Transform.Size = origin;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var glyph in glyphs)
            {
                spriteBatch.Draw(tex,
                    (glyph.Transform.Position) + Transform.Position,
                    glyph.src,
                    glyph.color,
                    glyph.Transform.Rotation + Transform.Rotation,
                    glyph.Transform.Origin,
                    glyph.Transform.Scale * Transform.Scale,
                    SpriteEffects.None,
                    glyph.depth);
            }
        }

        public void Update(GameTime gameTime)
        {

        }

        public void SetText(string text)
        {
            redraw = true;
            this.text = text;

            // TODO should provide DrawText with all data instead of relying on the render pass
            glyphs.Clear();
            _fontProvider.SpriteFont.DrawText(this, text, Vector2.Zero, Color.Black);

        }

        public void UpdateFont()
        {
            // Force redraw by settings text
            SetText(text);
        }
    }
}
