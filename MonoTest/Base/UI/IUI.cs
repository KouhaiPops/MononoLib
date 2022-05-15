using MonoTest.Core.Collision;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.UI
{
    public interface IUI : IDrawable, IElement
    {
        public BoundingBox<IUI> BoundingBox { get; set; }
        public event Func<bool> OnHover;
        public event Func<bool> OnClick;
        public event Func<bool> OnRelease;
        public event Func<bool> OnExit;
        public event Func<bool> OnEnter;

        public bool Click();
        public bool Hover();
        public bool Exit();
        public bool Release();
        public bool Enter();

    }
}
