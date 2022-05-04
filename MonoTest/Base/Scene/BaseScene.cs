using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using MonoTest.Base.Graphics;
using MonoTest.Base.Input;

namespace MonoTest.Base.Scene
{
    internal delegate void MethodBehaviour(GameTime gameTime);
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
        public ICamera Camera { get; set; } = new BaseCamera();


        public HashSet<IDrawable> Drawables { get; set; }
        public HashSet<IGameElement> StaticBehaviours { get; set; }
        public HashSet<MethodBehaviour> MethodBehaviours { get; set; }
        public DrawActions.PreDrawAction PreDraw { get; set; }
        public DrawActions.PostDrawAction PostDraw { get; set; }

        public BaseScene()
        {
            Input.KeyboardController.Initialize();
            Drawables = new HashSet<IDrawable>();
            StaticBehaviours = new HashSet<IGameElement>();
            MethodBehaviours = new HashSet<MethodBehaviour>();
            
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin(transformMatrix: Camera.GetTransform());
            foreach (var drawable in Drawables)
            {
                drawable.Draw(spriteBatch, gameTime);
            }
            spriteBatch.End();
        }

        public void Handle(GameTime gameTime)
        {
            KeyboardController.CheckInput(gameTime);
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

            foreach(var methodBehaviour in MethodBehaviours)
            {
                methodBehaviour(gameTime);
            }
        }

        public IScene<IDrawable, IGameElement> AddDrawable(IDrawable drawable)
        {
            Drawables.Add(drawable);
            if (drawable is IElement element)
            {
                element.Initialize();
            }
            return this;
        }

        public IScene<IDrawable, IGameElement> RemoveDrawable(IDrawable drawable)
        {
            Drawables.Remove(drawable);
            if(drawable is IElement element)
            {
                element.Removed();
            }
            return this;
        }

        public IScene<IDrawable, IGameElement> AddBehaviour(IGameElement behaviour)
        {
            StaticBehaviours.Add(behaviour);
            return this;
        }

        public IScene<IDrawable, IGameElement> AddMethodBehaviour(MethodBehaviour behaviour)
        {
            MethodBehaviours.Add(behaviour);
            return this;
        }

        public void Dispose()
        {
            // TODO free unmannged here
            throw new NotImplementedException();
        }
    }
}
