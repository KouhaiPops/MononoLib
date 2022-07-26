using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Core
{
    public unsafe struct ValueRef<T> where T : unmanaged
    {
        private T value;
        public ref T Ref => ref GetPtr();
        private ref T GetPtr()
        {
            fixed (T* ptr = &value)
                return ref (*ptr);
        }
        public ValueRef(T val)
        {
            value = val;
        }
        //public ref T Ref()
        //{
        //    return ref (*@ref);
        //}
    }

    public static class Extensions
    {
        public static void GetRef<T>(this ValueRef<T> valueRef) where T : unmanaged
        {

        }
    }
}
