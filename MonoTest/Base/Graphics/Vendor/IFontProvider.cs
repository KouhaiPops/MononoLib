using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Graphics.Vendor
{
    public interface IFontProvider
    {
        public string Font { get; set; }
        public int FontSize { get; set; }

        public bool UpdateFont(string font, int fontsize);
        public bool UpdateFontSize(int fontsize);
    }
}
