using MonoTest.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Chip.Engine
{
    internal class ChipScene : Base.Scene.BaseScene
    {
        Emulator emu;
        private int rectWidth;
        private int rectHeight;
        const int WIDTH = 64;
        const int HEIGHT = 32;
        private IDrawable[] pixels = new IDrawable[WIDTH * HEIGHT];
        public ChipScene()
        {
            emu = new()
            {
                Scene = this
            };
            emu.Load(@"C:\Users\MURICA\Downloads\test_opcode.ch8");
            
            AddBehaviour(emu);
            rectWidth = Base.State.GlobalState.WindowBounds.Width / WIDTH;
            rectHeight = Base.State.GlobalState.WindowBounds.Height / HEIGHT;
            OnMouseClick += (_) =>
            {
                emu.DEBUGGER_ATTACHED = !emu.DEBUGGER_ATTACHED;
            };
        }
        private Base.Primitives.RectanglePrimv Factory()
        {
            return new Base.Primitives.RectanglePrimv(rectHeight, rectWidth, Microsoft.Xna.Framework.Color.Black, 1, Microsoft.Xna.Framework.Color.Black);
        }
        public bool Draw(int x, int y)
        {
            var prev = pixels[x + (y * WIDTH)];
            if(prev != null)
            {
                RemoveDrawable(prev);
                return true;
            }
            var rect = Factory();
            rect.Transform.Position.X = x * rectWidth;
            rect.Transform.Position.Y = y * rectHeight;
            pixels[x + (y * WIDTH)] = rect;
            AddDrawable(rect);
            return false;
        }

        internal void Clear()
        {
            foreach(var drawable in Drawables)
            {
                RemoveDrawable(drawable);
            }
        }
    }
}
