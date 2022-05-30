using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoTest.Base.Graphics.Animation;
using MonoTest.Base.State;

using System.Collections.Generic;

namespace MonoTest.Debug.Graphics
{
    internal class AnimatedSpriteDebug : IDebug
    {
        public static float GlobalSpriteScale = 1;
        private HashSet<AnimatedSprite> AnimatedSprites { get; } = new();
        private float scale = 0.2f;
        public float Scale { get => scale * GlobalSpriteScale; set => scale = value; }
        
        public AnimatedSpriteDebug(AnimatedSprite animatedSprite)
        {
            AnimatedSprites.Add(animatedSprite);
            DebugManager.AddDebuggable(this);
        }

        public AnimatedSpriteDebug AddNewSprite(AnimatedSprite sprite)
        {
            AnimatedSprites.Add(sprite);
            return this;
        }

        public static AnimatedSpriteDebug Initialize(AnimatedSprite animatedSprite)
        {
            return new AnimatedSpriteDebug(animatedSprite);

        }
        public void DebugDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            float positionY = 0;
            foreach(var animatedSprite in AnimatedSprites)
            {
                float positionX = 0;
                float tallest = 0;
                foreach (var sprite in animatedSprite.Sprites)
                {
                    spriteBatch.Draw(sprite.Texture, new Vector2(positionX, positionY), null, Color.White, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
                    positionX += sprite.Texture.Width * Scale;
                    if(sprite.Texture.Height > tallest)
                    {
                        tallest = sprite.Texture.Height;
                    }
                    if((positionX + (sprite.Texture.Width * Scale)) > GlobalState.WindowBounds.Width)
                    {
                        positionY += tallest * Scale;
                        positionX = 0;
                        tallest = 0;
                    }
                }
                positionY += tallest * Scale;
            }
        }

        public void UpdateDebug(GameTime gameTime)
        {

        }
    }
}
