using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_3
{
    class Space:Tool
    {
        public Space(int color) :base(0){}
        public override string ToString()
        {
            return "  ..  ";
        }
    }
}
