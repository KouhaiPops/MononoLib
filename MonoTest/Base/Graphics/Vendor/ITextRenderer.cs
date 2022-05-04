using MonoTest.Base.Component;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Graphics.Vendor
{
    public interface ITextRenderer<T> : IDrawable where T : IFontProvider
    {
        public string Text { get; }
        public Transform Transform { get; set; }
        public void SetText(string text);
        public void UpdateFont();
    }
}
