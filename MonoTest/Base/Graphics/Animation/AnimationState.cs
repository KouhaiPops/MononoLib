using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Graphics.Animation
{
    /// <summary>
    /// A struct representing an animation state
    /// </summary>
    [DebuggerDisplay("Name: {Name}, Value: {Value}")]
    public readonly struct AnimationState
    {
        public readonly string Name;
        public readonly int Value;

        public AnimationState(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static readonly AnimationState Idle = new AnimationState("Idle", 0);
        public static readonly AnimationState Walk = new AnimationState("Walk", 1);
        public static readonly AnimationState Run = new AnimationState("Run", 2);
        public static readonly AnimationState Dying = new AnimationState("Dying", 3);
    }
}
