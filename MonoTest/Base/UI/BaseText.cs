
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoTest.Base.Component;
using MonoTest.Base.Graphics.Vendor;
using MonoTest.Base.State;
using MonoTest.Base.Utils;

using System;

namespace MonoTest.Base.UI
{
    public class BaseText : BaseElement, IDrawable
    {
        public DrawActions.PreDrawAction PreDraw { get; set; } = DrawBase.PreDraw;
        public DrawActions.PostDrawAction PostDraw { get; set; } = DrawBase.PostDraw;
        public ref Color Color { get => ref _Color; }
        private Color _Color = Color.Black;

        //TODO signal the renderer to redraw the new text
        public string Text { get => renderer.Text; set => renderer.SetText(value); }
        public string Font => fontProvider.Font;
        public int FontSize => fontProvider.FontSize;
        //private SpriteFontBase font;
        // TODO show allow generic to change the renderer type
        FontStashProvider fontProvider;
        private ITextRenderer<FontStashProvider> renderer;
        private TimeSpan intervalPerCharacter = TimeSpan.Zero;
        public int CharactersPerSecond { get => 0; set => intervalPerCharacter = TimeSpan.FromSeconds(1f / value); }
        
        public BaseText(string text = "Text", string fontname = FontUtils.DefaultFont, int fontSize = FontUtils.DefaultFontSize)
        {
            // Should move to initialize
            fontProvider = new FontStashProvider();
            renderer = new BaseTextRenderer<FontStashProvider>(GlobalState.GrphDevMngr.GraphicsDevice, fontProvider);
            renderer.Transform.Parent = Transform;
            SetFont(fontname, fontSize);
            Text = text;
            Transform.Size = renderer.Transform.Size;
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //if(Mouse.GetState().LeftButton == ButtonState.Pressed)
            //{
            //    renderer.Text = "Another message";
            //}
            renderer.Draw(spriteBatch, gameTime);
        }

        public override void Initialize()
        {

        }

        public override void Update(GameTime gameTime)
        {

        }
        public bool SetFont(string fontname, int fontSize = FontUtils.DefaultFontSize)
        {
            return fontProvider.UpdateFont(fontname, fontSize);
        }

        /// <summary>
        /// Set the current FontSystem's fontsize, this doesn't downscale, instead it relies on the renderer
        /// </summary>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        public bool SetFontSize(int fontSize)
        {
            return fontProvider.UpdateFontSize(fontSize);
        }

   
    
    }
}
