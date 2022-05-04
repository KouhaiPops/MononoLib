using FontStashSharp;

using MonoTest.Base.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Graphics.Vendor
{
    public class FontStashProvider : IFontProvider
    {
        public event Action OnFontChange;
        public string Font { get; set; }
        public int FontSize { get; set; }
        public SpriteFontBase SpriteFont { get; private set; }

        public bool UpdateFont(string fontname, int fontSize)
        {
            var font = FontUtils.GetFont(fontname, fontSize);
            if (font == null)
            {
                return false;
            }
            SpriteFont = font;
            Font = fontname;
            FontSize = fontSize;
            OnFontChange?.Invoke();
            return true;
        }

        public bool UpdateFontSize(int fontSize)
        {
            if (fontSize == FontSize)
            {
                return true;
            }
            var font = FontUtils.GetFont(Font, fontSize);
            if (font == null)
            {
                return false;
            }
            SpriteFont = font;
            FontSize = fontSize;
            OnFontChange?.Invoke();
            return true;
        }
    }
}
