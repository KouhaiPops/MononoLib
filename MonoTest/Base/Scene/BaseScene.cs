using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoTest.Base.Scene
{
    class BaseScene : IScene<IDrawable, IGameElement>, IDrawable
    {
        private bool isHeld = false;
        public event Action<Vector2> OnMouseClick;
        public HashSet<IDrawable> Drawables { get; set; }
        public HashSet<IGameElement> StaticBehaviours { get; set; }
        public DrawActions.PreDrawAction PreDraw { get; set; }
        public DrawActions.PostDrawAction PostDraw { get; set; }

        public BaseScene()
        {
            Drawables = new HashSet<IDrawable>();
            StaticBehaviours = new HashSet<IGameElement>();
            
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var drawable in Drawables)
            {
                drawable.Draw(spriteBatch, gameTime);
            }
        }

        public void Update(GameTime gameTime)
        {
            var mouse = Microsoft.Xna.Framework.Input.Mouse.GetState();
            if (isHeld && mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                OnMouseClick(new Vector2(mouse.X, mouse.Y));
            }
            isHeld = mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
            foreach (var drawable in Drawables)
            {
                drawable.Update(gameTime);
            }

            foreach (var staticBehaviour in StaticBehaviours)
            {
                staticBehaviour.Update(gameTime);
            }
        }

        public IScene<IDrawable, IGameElement> AddDrawable(IDrawable drawable)
        {
            Drawables.Add(drawable);
            return this;
        }

        public IScene<IDrawable, IGameElement> RemoveDrawable(IDrawable drawable)
        {
            Drawables.Remove(drawable);
            return this;
        }

        public IScene<IDrawable, IGameElement> AddBehaviour(IGameElement behaviour)
        {
            StaticBehaviours.Add(behaviour);
            return this;
        }

        public void Dispose()
        {
            // TODO free unmannged here
            throw new NotImplementedException();
        }
    }
}
