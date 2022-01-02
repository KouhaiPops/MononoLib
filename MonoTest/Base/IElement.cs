using Microsoft.Xna.Framework;

using MonoTest.Base.Component;
using MonoTest.Base.Effects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base
{
    public interface IElement
    {
        public bool Active { get; set; }
        public IElement Parent { get; set; }
        public Transform Transform { get; }
        public HashSet<IElement> Children { get; set; }
        public Dictionary<Type, IComponent> Components { get; set; }
        public void Initialize();
        public void Update(GameTime gameTime);
        public void AddComponent(IComponent component);
        public void RemoveComponent(IComponent component);
        public void RemoveComponent<T>();
        public void RemoveComponent(Type type);
        public IComponent GetComponent<T>();
        public IComponent GetComponent(Type type);

    }
}
