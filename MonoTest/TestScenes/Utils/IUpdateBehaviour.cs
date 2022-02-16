using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.TestScenes.Utils
{
    public interface IUpdateBehaviour<T> where T : IUpdateable<T>
    {
        public void OnUpdate(T updatable);
    }
}
