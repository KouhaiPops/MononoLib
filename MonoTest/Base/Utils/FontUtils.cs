using FontStashSharp;

using MonoTest.Base.State;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Utils
{
    public static class FontUtils
    {
        private static readonly string fontPath = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
        
        public static FontStashSharp.SpriteFontBase GetGlobalFont(string fontName = "Arial.ttf", int fontSize = 24, FontSystemSettings settings = null)
        {
            GlobalState.FontManager.AddFont(FontReader(fontName));
            return GlobalState.FontManager.GetFont(fontSize);
        }

        public static FontStashSharp.SpriteFontBase GetFont(string fontName = "Arial.ttf", int fontSize = 24, FontSystemSettings settings = null)
        {
            if (!GlobalStorage.TryGetFontSystem(fontName, out FontSystem system))
            {
                system = (settings != null) ? new FontSystem(settings) : new FontSystem();
                system.AddFont(FontReader(fontName));
                GlobalStorage.AddFontSystem(fontName, system);
            }

            return system.GetFont(fontSize);
        }


        private static byte[] FontReader(string fontName)
        {
            return File.ReadAllBytes(Path.Combine(fontPath, fontName));
        }
    }
}
