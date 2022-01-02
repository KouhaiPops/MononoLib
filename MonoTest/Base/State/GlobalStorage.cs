using FontStashSharp;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.State
{
    public class GlobalStorage
    {
        private static readonly Dictionary<string, FontStashSharp.FontSystem> cachedFontManagers = new();

        public static void AddFontSystem(string fontName, FontSystem system)
        {
            cachedFontManagers.TryAdd(fontName.ToLower(), system);
        }

        public static bool TryGetFontSystem(string fontName,  [DisallowNull]out FontSystem system)
        {
            return cachedFontManagers.TryGetValue(fontName.ToLower(), out system);
        }
    }
}
