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
        // TODO shouldn't assume the default font is arial, isntead try to query
        public const string DefaultFont = "Arial.ttf";
        public const int DefaultFontSize = 24;
        private static readonly string fontPath = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
       
        /// <summary>
        /// Gets font immeditaly from FontSystem
        /// Should be avoided as much as possible
        /// </summary>
        /// <param name="fontName"></param>
        /// <param name="fontSize"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static FontStashSharp.SpriteFontBase GetGlobalFont(string fontName = DefaultFont, int fontSize = DefaultFontSize, FontSystemSettings settings = null)
        {
            GlobalState.FontManager.AddFont(FontReader(fontName));
            return GlobalState.FontManager.GetFont(fontSize);
        }

        /// <summary>
        /// Get new or cached font
        /// </summary>
        /// <param name="fontName"></param>
        /// <param name="fontSize"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static FontStashSharp.SpriteFontBase GetFont(string fontName = DefaultFont, int fontSize = DefaultFontSize, FontSystemSettings settings = null)
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
