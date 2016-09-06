﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_3
{
    class Bishop:Tool
    {
        public Bishop(int color) : base(color) { }
        public override bool testingFrom(int n1, int n2, Tool[,] tool, int numTurn)
        {
            return diagonal(n1, n2, tool, numTurn);
        }
        public override string ToString()
        {
            return " bish ";
        }
    }
}
