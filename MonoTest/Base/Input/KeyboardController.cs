using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Input
{
    public static class KeyboardController
    {
        public delegate void KeyboardHandler(GameTime gameTime);
        private static Dictionary<Keys, HashSet<KeyboardHandler>> Handlers = new Dictionary<Keys, HashSet<KeyboardHandler>>();

        public static void Initialize()
        {
            foreach (var key in Enum.GetValues<Keys>())
            {
                Handlers.Add(key, new HashSet<KeyboardHandler>());
            }
        }
        public static void AddHandler(Keys key, KeyboardHandler handler)
        {
            Handlers[key].Add(handler);
        }

        public static void CheckInput(GameTime gameTime)
        {
            var kbState = Keyboard.GetState();
            foreach(var key in kbState.GetPressedKeys())
            {
                foreach(var handler in Handlers[key])
                {
                    handler(gameTime);
                }
            }
        }
    }
}
