#nullable enable
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoTest.Base.Graphics.Animation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Graphics
{
    public class SpriteRenderer : IDrawable
    {
        /// <summary>
        /// Repesents the default state for an aniamted sprite
        /// This is the spritestate played by deafult when Draw is called
        /// The behaviour should be changed and instead be manually trigied, the renderer shouldn't assume that is should start rendering right now?
        /// </summary>
        const int DEFUALT_STATE = 0;
        public DrawActions.PreDrawAction PreDraw { get; set; }
        public DrawActions.PostDrawAction PostDraw { get; set; }
        private bool _loopAll;
        public bool LoopAll { get => _loopAll; set { _loopAll = value; foreach (var animatedSprite in spriteStates.Values) { animatedSprite.ForceLoop = value; } } }
        public ISprite Sprite { get; set; }
        Animation.AnimatedSprite animated;
        private readonly Dictionary<int, AnimatedSprite> spriteStates = new() { {default, null } };
        public AnimationState State { get; private set; } = default;

        public void SetState(AnimationState state)
        {
            if(state.Value != State.Value && spriteStates.TryGetValue(state.Value, out var sprite))
            {
                State = state;
                animated?.Reset();
                animated = sprite;
            }
        }

        public AnimatedSprite? GetSprite(AnimationState state)
        {
            return spriteStates.TryGetValue(state.Value, out var val ) ? val : null;
        }

        public void AddAnimatedSprite(AnimatedSprite animatedSprite, AnimationState state = default)
        {
            if(LoopAll)
            {
                animatedSprite.Loop = true;
            }
            spriteStates[state.Value] = animatedSprite;
        }
        public BaseElement Parent { get; set; }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Sprite.Texture, Parent.Transform.Position, null, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            if(animated == null)
            {
                animated = spriteStates[State.Value];
            }
            Sprite = animated.GetNextSprite(gameTime);
        }
    }
}
