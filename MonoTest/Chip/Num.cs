using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Chip
{
    struct Num
    {
        private byte l;
        private byte r;
        public static Num operator ++(Num n)
        {
            n.l++;
            return n;
        }
    }
}
