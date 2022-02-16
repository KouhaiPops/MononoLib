using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Chip
{
    public interface ILoader
    {
        public byte[] Load(out int count);
    }
}
