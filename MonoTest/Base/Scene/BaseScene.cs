using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoTest.Base.Scene
{
    class BaseScene : IScene<IDrawable, IGameElement>, IDrawable
    {
        public enum ScrollEvent
        {
            Up = 0,
            Down = 1,
        }
        private int wheelVal = 0;

        private bool isHeld = false;
        public event Action<Vector2> OnMouseRelease;
        public event Action<Vector2> OnMouseClick;
        public event Action<Vector2> OnMouseHold;
        public event Action<ScrollEvent> OnScroll;

        public HashSet<IDrawable> Drawables { get; set; }
        public HashSet<IGameElement> StaticBehaviours { get; set; }
        public DrawActions.PreDrawAction PreDraw { get; set; }
        public DrawActions.PostDrawAction PostDraw { get; set; }

        public BaseScene()
        {
            Drawables = new HashSet<IDrawable>();
            StaticBehaviours = new HashSet<IGameElement>();
            
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var drawable in Drawables)
            {
                drawable.Draw(spriteBatch, gameTime);
            }
        }

        public void Handle(GameTime gameTime)
        {
            var mouse = Microsoft.Xna.Framework.Input.Mouse.GetState();
            var wheelDelta = mouse.ScrollWheelValue - wheelVal;
            wheelVal = mouse.ScrollWheelValue;
            if(wheelDelta < 0)
            {
                OnScroll?.Invoke(ScrollEvent.Down);
            }
            else if(wheelDelta > 0)
            {
                OnScroll?.Invoke(ScrollEvent.Up);
            }
            var mousePos = new Vector2(mouse.X, mouse.Y);
            if (isHeld)
            {
                OnMouseHold?.Invoke(mousePos);
            }
            if (!isHeld && mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                OnMouseClick?.Invoke(mousePos);
            }
            else if(isHeld && mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                OnMouseRelease?.Invoke(mousePos);
            }

            isHeld = mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
        }

        public void Update(GameTime gameTime)
        {
            //InputManager.Handle(gametime)
            Handle(gameTime);
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
