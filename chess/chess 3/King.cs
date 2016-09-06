using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_3
{

    class King:Tool
    {
        public King(int color) : base(color) { }
        public override bool testingFrom(int n1, int n2, Tool[,] tool, int numTurn)
        {

            bool testMove = false;
            
                testMove = testCastling(n1, n2, tool, numTurn);
            
            int num1 = n1-1;
                      
            for(int i =0;i<3;i++,num1++)
                for(int z =0, num2 = n2-1; z <3 ;z++,num2++)                
                   if(num1>=0 && num1<8 && num2>=0 && num2<8)
                      if (tool[num1, num2].getColor() == 0 || tool[num1, num2].getColor() != numTurn )
                      {
                          tool[num1, num2].setAvailable(1);
                          testMove = true;
                      }
                
            return testMove;
        }
        public bool testCastling(int n1, int n2, Tool[,] tool, int numTurn)
        {
            
            int right = 0;
            int left = 0;
            

                if (tool[n1, n2].castling == true)
                {
                    for (int i = 4; i < 8 && right ==0 ; i++)
                        right += tool[n1, i].testingDanger(n1, i, tool,numTurn);
                    for (int i = 0; i < 5 && left==0 ; i++)
                        left += tool[n1, i].testingDanger(n1, i, tool,numTurn);

                    if (tool[n1, 5] is Space && tool[n1, 6] is Space && tool[n1, 7].castling == true && right==0 )
                        tool[n1, 6].setAvailable(1);

                    if (tool[n1, 3] is Space && tool[n1, 2] is Space && tool[n1, 1] is Space && tool[n1, 0].castling == true && left==0 )
                        tool[n1, 2].setAvailable(1);
                    return true;
                }
            return false;
        }
        public override string ToString()
        {
            return " King ";
        }

    }
}
