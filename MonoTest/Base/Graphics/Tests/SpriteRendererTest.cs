using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoTest.Base.Graphics.Animation;
using MonoTest.Base.Input;
using MonoTest.Core.Physics;
using MonoTest.Debug.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Graphics.Tests
{
    public class SpriteRendererTest : BaseElement, IDrawable
    {
        public DrawActions.PreDrawAction PreDraw { get; set; }
        public DrawActions.PostDrawAction PostDraw { get; set; }
        public SpriteRenderer SpriteRenderer { get; } = new SpriteRenderer();
        private float speed = 1;
        public override void Initialize()
        {
            
            SpriteRenderer.AddAnimatedSprite(AnimatedSprite.FromFolder(@"E:\GameDev\Assets\DemoAnimations\cutegirlfiles\png\Idle"), AnimationState.Idle);
            SpriteRenderer.AddAnimatedSprite(AnimatedSprite.FromFolder(@"E:\GameDev\Assets\DemoAnimations\cutegirlfiles\png\Run"), AnimationState.Run);
            //AnimatedSpriteDebug.Initialize(SpriteRenderer.GetSprite(AnimationState.Idle)).AddNewSprite(SpriteRenderer.GetSprite(AnimationState.Run));
            SimulationFactory.GetDefault.AddDynamic(this);
            SpriteRenderer.Parent = this;
            SpriteRenderer.LoopAll = true;

        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            bool walking = false;
            if(Keys.D.IsDown())
            {
                walking = true;
                Transform.Position.X += (float)(speed * gameTime.ElapsedGameTime.TotalMilliseconds);
                SpriteRenderer.HorizontalFlip = false;
            }
            else if(Keys.A.IsDown())
            {
                walking = true;
                Transform.Position.X -= (float)(speed * gameTime.ElapsedGameTime.TotalMilliseconds);
                SpriteRenderer.HorizontalFlip = true;
            }
            if (walking)
            {
                SpriteRenderer.SetState(AnimationState.Run);
            }
            else
            {
                SpriteRenderer.SetState(AnimationState.Idle);
            }
            SpriteRenderer.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime)
        {
            SpriteRenderer.Draw(spriteBatch, gameTime);
        }
    }
}
