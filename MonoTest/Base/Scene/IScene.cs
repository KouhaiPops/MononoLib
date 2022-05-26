using MonoTest.Base.UI;

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
        public UITree UITree { get; set; }
        public HashSet<TDrawable> Drawables { get; set; }
        public HashSet<TBehaviour> StaticBehaviours { get; set; }
        public HashSet<TMethodBehaviour> MethodBehaviours { get; set; }

        /// <summary>
        /// Add a <see cref="TDrawable"/> to be drawn and upadted each call.
        /// </summary>
        /// <param name="drawable"></param>
        /// <returns></returns>
        public IScene<TDrawable, TBehaviour, TMethodBehaviour> AddDrawable(TDrawable drawable);

        /// <summary>
        /// Add a <see cref="TBehaviour"/> instance to be execute every update call.
        /// </summary>
        /// <param name="behaviour"></param>
        /// <returns></returns>
        public IScene<TDrawable, TBehaviour, TMethodBehaviour> AddBehaviour(TBehaviour behaviour);

        /// <summary>
        ///  Add a method or lambda function to be execute every udpate call.
        /// </summary>
        /// <param name="methodBehaviour"></param>
        /// <returns></returns>
        public IScene<TDrawable, TBehaviour, TMethodBehaviour> AddMethodBehaviour(TMethodBehaviour methodBehaviour);
    }
}
