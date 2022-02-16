using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.TestScenes.Utils
{
    public interface IUpdateable<T> where T : IUpdateable<T>
    {
        public Action<T> OnUpdate { get; set; }
    }
}
