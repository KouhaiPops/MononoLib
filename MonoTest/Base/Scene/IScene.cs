using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Scene
{
    public interface IScene<TDrawable, TBehaviour> : IDisposable 
        where TDrawable : IDrawable
        where TBehaviour : IGameElement
    {
        public HashSet<TDrawable> Drawables { get; set; }
        public HashSet<TBehaviour> StaticBehaviours { get; set; }

        public IScene<TDrawable, TBehaviour> AddDrawable(TDrawable drawable);
        public IScene<TDrawable, TBehaviour> AddBehaviour(TBehaviour behaviour);
    }

    public interface IScene<TDrawable, TBehaviour, TMethodBehaviour> : IDisposable
        where TDrawable : IDrawable
        where TBehaviour : IGameElement
    {
        public HashSet<TDrawable> Drawables { get; set; }
        public HashSet<TBehaviour> StaticBehaviours { get; set; }
        public HashSet<TMethodBehaviour> MethodBehaviours { get; set; }

        public IScene<TDrawable, TBehaviour, TMethodBehaviour> AddDrawable(TDrawable drawable);
        public IScene<TDrawable, TBehaviour, TMethodBehaviour> AddBehaviour(TBehaviour behaviour);
        public IScene<TDrawable, TBehaviour, TMethodBehaviour> AddMethodBehaviour(TMethodBehaviour methodBehaviour);
    }
}
