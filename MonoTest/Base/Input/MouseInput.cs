using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Input
{
    public static class MouseInput
    {
        public static Vector2 Position => Mouse.GetState().Position.ToVector2();
        public static bool LeftBtn => Mouse.GetState().LeftButton == ButtonState.Pressed;
    }
}
