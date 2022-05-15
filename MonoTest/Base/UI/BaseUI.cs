using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoTest.Core.Collision;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.UI
{
    public abstract class BaseUI : BaseElement, IUI
    {
        public BoundingBox<IUI> BoundingBox { get; set; }
        public DrawActions.PreDrawAction PreDraw { get; set; }
        public DrawActions.PostDrawAction PostDraw { get; set; }
        public IUI Container { get; private set; }

        public event Func<bool> OnHover;
        public event Func<bool> OnClick;
        public event Func<bool> OnRelease;
        public event Func<bool> OnExit;
        public event Func<bool> OnEnter;
        public sealed override void Initialize()
        {
            BoundingBox = new BoundingBox<IUI>(this);
        }
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

        public bool Click()
        {
            if (OnClick == null)
                return true;
            return OnClick();
        }
        public bool Hover()
        {
            if (OnHover == null)
                return true;
            return OnHover();
        }
        public bool Release()
        {
            if (OnRelease == null)
                return true;
            return OnRelease();
        }

        public bool Exit()
        {
            if (OnExit == null)
                return true;
            return OnExit();
        }

        public bool Enter()
        {
            if (OnEnter == null)
                return true;
            return OnEnter();
        }
    }
}
