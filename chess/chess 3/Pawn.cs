using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_3
{
    class Pawn:Tool 
    {
        
        public Pawn(int color) : base(color) { }

        public override bool testingFrom(int n1, int n2, Tool[,] tool,int numTurn)
        {     
       
            if(numTurn==2)
              return fromPawnWhite(n1, n2, tool) ;
            else
               return fromPawnBlack(n1, n2, tool);
        }
        public bool fromPawnBlack(int n1, int n2,Tool [,]tool)
        {
            bool testMove = false;

            if (8 > n1 + 1)
            {
                if (tool[n1 + 1, n2].getColor() == 0) // בדיקת תזוזה, צעד אחד קדימה 
                {
                    tool[n1 + 1, n2].setAvailable(1);
                    testMove = true;
                    if (n1 == 1 && tool[n1 + 2, n2].getColor() == 0)
                    {
                        tool[n1 + 2, n2].setAvailable(1); // מהלך ראשון של חייל = זכאי לשני צעדים קדימה 
                        testMove = true;
                    }
                }
            }
            if (n1 + 1 < 8) // קרב  
            {
                if (8 > n2 + 1)
                {
                    if (tool[n1 + 1, n2 + 1].getColor() == 2 || tool[n1 + 1, n2 + 1].getEnPassant() == 2)
                    {
                        tool[n1 + 1, n2 + 1].setAvailable(1);
                        testMove = true;
                    }
                }
                if (0 <= n2 - 1)
                {
                    if (tool[n1 + 1, n2 - 1].getColor() == 2 || tool[n1 + 1, n2 - 1].getEnPassant() == 2)
                    {
                        tool[n1 + 1, n2 - 1].setAvailable(1);
                        testMove = true;
                    }
                }
            }

            return testMove;
        }
        public bool fromPawnWhite(int n1, int n2,Tool [,]tool)
        {
            bool testMove = false;
           
            if (n1 - 1 >= 0)
            {
                
                if (tool[n1 - 1, n2].getColor() == 0) // בדיקת תזוזה, צעד אחד קדימה 
                {
                    tool[n1 - 1, n2].setAvailable(1);
                    testMove = true;
                    if (n1 == 6 && tool[n1 - 2, n2].getColor() == 0)
                    {
                        tool[n1 - 2, n2].setAvailable(1); // מהלך ראשון של חייל = זכאי לשני צעדים קדימה 
                        testMove = true;
                    }
                }
            }
            if (n1 - 1 >= 0) // קרב  
            {
                if (n2 + 1 < 8)
                {
                    if (tool[n1 - 1, n2 + 1].getColor() == 1 || tool[n1 - 1, n2 + 1].getEnPassant() == 1)
                    {
                        tool[n1 - 1, n2 + 1].setAvailable(1);
                        testMove = true;
                    }
                }
                if (n2 - 1 >= 0)
                {
                    if (tool[n1 - 1, n2 - 1].getColor() == 1 || tool[n1 - 1, n2 - 1].getEnPassant() == 2)
                    {
                        tool[n1 - 1, n2 - 1].setAvailable(1);
                        testMove = true;
                    }
                }
            }
            return testMove;
        }
        public bool coronation(int n3, int n4, Tool[,] tool,int numTurn)
        {
            bool loop =false;
            do
            {
                string input = "";
                Console.WriteLine("Hooray! You were crowned! Choose what role the soldier will be crowned? \n 1 for rook,2 for bishop, 3 queen 4 for knight");
                input = Console.ReadLine();
                input = input.ToLower();
                input = input.Trim();

                switch (input)
                {
                    case "1":
                        tool[n3, n4] = new Rook(numTurn);
                        return true;

                    case "2":
                        tool[n3, n4] = new Bishop(numTurn);
                        return true;

                    case "3":
                        tool[n3, n4] = new Qween(numTurn);
                        return true;

                    case "4":
                        tool[n3, n4] = new Knight(numTurn);
                        return true;
                }

            } while (loop); // לולאה אינסופית 
            return false;
        }     
        public override string ToString()
        {
            return " pwan ";
        }

    }
}
