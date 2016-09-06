using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_3
{
    class Knight:Tool
    {
        public Knight(int color) : base(color) { }
        public override bool testingFrom(int n1, int n2,Tool [,]tool,int numTurn)
        {
            bool testmove = false;

            if (8 > n1 + 2 && n2 + 1 < 8)
               if (tool[n1 + 2, n2 + 1].getColor() == 0 || tool[n1 + 2, n2 + 1].getColor() == (numTurn==1 ? 2 : 1))
               {
                   tool[n1 + 2, n2 + 1].setAvailable(1);
                   testmove = true;
               }            
            if (8 > n1 + 2 && n2 - 1 >= 0)            
                if (tool[n1 + 2, n2 - 1].getColor() == 0 || tool[n1 + 2, n2 - 1].getColor() == (numTurn == 1 ? 2 : 1))
                {
                    tool[n1 + 2, n2 - 1].setAvailable(1);
                    testmove = true;
                }          
            if (0 <= n1 - 2 && n2 + 1 < 8)           
                if (tool[n1 - 2, n2 + 1].getColor() == 0 || tool[n1 - 2, n2 + 1].getColor() == (numTurn == 1 ? 2 : 1))
                {
                    tool[n1 - 2, n2 + 1].setAvailable(1);
                    testmove = true;
                }           
            if (0 <= n1 - 2 && n2 - 1 >= 0)
                if (tool[n1 - 2, n2 - 1].getColor() == 0 || tool[n1 - 2, n2 - 1].getColor() == (numTurn == 1 ? 2 : 1))
                {
                    tool[n1 - 2, n2 - 1].setAvailable(1);
                    testmove = true;
                }           
            if (8 > n1 + 1 && n2 + 2 < 8)           
                if (tool[n1 + 1, n2 + 2].getColor() == 0 || tool[n1 + 1, n2 + 2].getColor() == (numTurn == 1 ? 2 : 1))
                {
                    tool[n1 + 1, n2 + 2].setAvailable(1);
                    testmove = true;
                }                     
            if (0 <= n1 - 1 && n2 + 2 < 8)          
                if (tool[n1 - 1, n2 + 2].getColor() == 0 || tool[n1 - 1, n2 + 2].getColor() == (numTurn == 1 ? 2 : 1))
                {
                    tool[n1 - 1, n2 + 2].setAvailable(1);
                    testmove = true;
                }           
            if (8 > n1 + 1 && n2 - 2 >= 0)            
                if (tool[n1 + 1, n2 - 2].getColor() == 0 || tool[n1 + 1, n2 - 2].getColor() == (numTurn == 1 ? 2 : 1))
                {
                    tool[n1 + 1, n2 - 2].setAvailable(1);
                    testmove = true;
                }
            
            if (0 <= n1 - 1 && n2 - 2 >= 0)            
                if (tool[n1 - 1, n2 - 2].getColor() == 0 || tool[n1 - 1, n2 - 2].getColor() == (numTurn == 1 ? 2 : 1))
                {
                    tool[n1 - 1, n2 - 2].setAvailable(1);
                    testmove = true;
                }          
            return testmove;
        }
        public override string ToString()
        {
            return " knig ";
        }
    }
}
