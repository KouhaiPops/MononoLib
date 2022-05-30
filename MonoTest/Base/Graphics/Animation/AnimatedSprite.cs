using Microsoft.Xna.Framework;

using MonoTest.Base.State;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Graphics.Animation
{
    public class AnimatedSprite
    {
        public Sprite[] Sprites { get; private set; }
        private double timeSinceLastFrame;
        private int index;

        public float FrameDelay { get; set; } = 30;
        public bool Loop { get; set; } = false;
        public bool ForceLoop { get; set; }
        public AnimatedSprite(params Sprite[] sprites)
        {
            Sprites = sprites;
        }

        public void Reset(bool resetIndex = false)
        {
            index = resetIndex ? 0 : index;
            timeSinceLastFrame = 0;
        }

        public static AnimatedSprite FromFolder(string path)
        {
            return new (Directory.GetFiles(path).Select(f => Sprite.FromFile(f)).ToArray());
        }
        public Sprite GetNextSprite(GameTime gameTime)
        {
            if (timeSinceLastFrame >= (FrameDelay / GlobalState.AnimationScale))
            {
                if ((index + 1) == Sprites.Length)
                {
                    if (Loop || ForceLoop)
                    {
                        index = 0;
                    }
                }
                else
                {
                    index++;
                }
                timeSinceLastFrame = 0;
            }
            else
            {
                timeSinceLastFrame += gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            return Sprites[index];

        }
    }
}
