using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Core
{
    public class Reference<T>
    {
        private T val;
        public Reference(T val)
        {
            this.val = val;
        }
        public void Set(T val)
        {
            this.val = val;
        }
        public ref T Value { get => ref val; }

    }
}
