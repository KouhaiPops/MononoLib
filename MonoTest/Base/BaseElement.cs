using Microsoft.Xna.Framework;

using MonoTest.Base.Component;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base
{
    public abstract class BaseElement : IElement
    {
        /// <summary>
        /// Determine whether an element is active or not
        /// </summary>
        public bool Active { get; set; } = true;

        /// <summary>
        /// Backing field for <see cref="Parent"/>
        /// </summary>
        private IElement? parent;
        /// <summary>
        /// The elements current parent </br>
        /// Modifying this property sets <see cref="Transform.Parent"/> to Parent's transform
        /// </summary>
        public IElement? Parent
        {
            get => parent;
            set { parent = value; Transform.Parent = value?.Transform; }
        }
        public Transform Transform { get; } = new Transform();
        public HashSet<IElement> Children { get; set; } = new HashSet<IElement>();
        public Dictionary<Type, IComponent> Components { get; set; } = new Dictionary<Type, IComponent>();

        public void AddComponent(IComponent component) => Components.Add(component.GetType(), component);

        public void RemoveComponent(IComponent component) => RemoveComponent(component?.GetType());

        public void RemoveComponent<T>() => RemoveComponent(typeof(T));

        public void RemoveComponent(Type? type)
        {
            if(type != null)
            {
                Components.Remove(type);
            }
        }
        public IComponent? GetComponent<T>() => GetComponent(typeof(T));

        public IComponent? GetComponent(Type type) => Components.TryGetValue(type, out var component) ? component : null;

        public virtual void Initialize()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Removed()
        {

        }

        public virtual void AddChild(IElement child)
        {
            child.Parent?.RemoveChild(child);
            child.Parent = this;
            child.Transform.Parent = Transform;
            Children.Add(child);
        }

        public virtual void RemoveChild(IElement child)
        {
            Children.Remove(child);
        }
    }
}
