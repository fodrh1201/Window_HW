using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omok02
{
    class AI
    {
        Board b;

        public AI(Board b)
        {
            this.b = b;
        }

        public Stone Stone()
        {
            Stone s = new Stone(-1, 3, 3);
            return s;
        }
    }
}
