using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_3
{
    class Tool
    {
        int color;
        public Tool(int color)
        {
            this.color = color;
        }
        public bool setColor(int color)
        {
            this.color = color;
            return true;
        }
        public int getColor()
        {
            return color;
        }
        int available;// 0 = free ,1 =Save space
        public void setAvailable(int numTrue)
        {
            available = numTrue;

        }
        public int getAvilable()
        {

            return available;
        }
        int enPassant;//0 = free,1= en passnet for black ,2 en passnet for white
        public void SetEnPassant(int numPass)
        {
            enPassant = numPass;
        }
        public int getEnPassant()
        {
            return enPassant;
        }
        public bool castling { get; set; } // הצרחה 
        int mate; // 0 = free 1 save :mate test
        public bool setMate(int mateIn)
        {
            this.mate = mateIn;
            return true;
        }
        public int getMate()
        {
            return this.mate;
        }
        bool duringTesting1;// flase = רגיל ,true = בדיקת שחמט
        public void setDuringTesting(bool dT)
        {
            this.duringTesting1 = dT;
        }
        public bool getDuringTesting()
        {
            return duringTesting1;
        }
        bool danger; // בדיקת איום על ערוגה נתונה 
        public void setTestingDanger(bool agency)
        {
            this.danger =agency;
        }
        public bool getTestingDanger()
        {
            return danger;
        }

        //////////////////////////////////////////////////////////////////////////////////

        public virtual bool testingFrom(int n1, int n2, Tool[,] tool, int numTurn)
        {
            return true;
        }
        public bool downAndLandscape(int n1, int n2, Tool[,] tool, int numTurn)
        {
            int num1 = n1;
            int num2 = n2;
            bool testmove = false;

            num1++;
            for (; num1 < 8; num1++)
            {
                if (tool[num1, n2].getColor() == (numTurn == 1 ? 2 : 1))
                {
                    tool[num1, n2].setAvailable(1);
                    testmove = true;
                    if (duringTesting1 && tool[num1, n2] is King)
                    {
                        continue;
                    }
                    else
                        break;
                }

                if (tool[num1, n2].getColor() == 0) // לאורך 
                {
                    tool[num1, n2].setAvailable(1);
                    testmove = true;
                }
                else
                    break;

            }

            num1 = n1 - 1;

            for (; num1 >= 0; num1--)
            {
                if (tool[num1, n2].getColor() == (numTurn == 1 ? 2 : 1))
                {
                    tool[num1, n2].setAvailable(1);
                    testmove = true;
                    if (duringTesting1 && tool[num1, n2] is King)
                        continue;
                    else
                        break;
                }

                if (tool[num1, n2].getColor() == 0) // לאורך 
                {
                    tool[num1, n2].setAvailable(1);
                    testmove = true;
                }
                else
                    break;

            }

            num2++;
            for (; num2 < 8; num2++)
            {
                if (tool[n1, num2].getColor() == (numTurn == 1 ? 2 : 1))
                {
                    tool[n1, num2].setAvailable(1);
                    testmove = true;
                    if (duringTesting1 && tool[n1, num2] is King)
                        continue;
                    else
                        break;
                }

                if (tool[n1, num2].getColor() == 0) // לרוחב 
                {
                    tool[n1, num2].setAvailable(1);
                    testmove = true;
                }
                else
                    break;

            }


            num2 = n2 - 1;

            for (; num2 >= 0; num2--)
            {
                if (tool[n1, num2].getColor() == (numTurn == 1 ? 2 : 1))
                {
                    tool[n1, num2].setAvailable(1);
                    testmove = true;
                    if (duringTesting1 && tool[n1, num2] is King)
                        continue;
                    else
                        break;
                }

                if (tool[n1, num2].getColor() == 0) // לרוחב 
                {
                    tool[n1, num2].setAvailable(1);
                    testmove = true;
                }
                else
                    break;

            }

            return testmove;
        }
        public bool diagonal(int n1, int n2, Tool[,] tool, int numTurn)
        {
            int num1 = n1;
            int num2 = n2;
            bool testmove = false;

            num1 = n1 + 1;
            num2 = n2 + 1;
            for (; num1 < 8 && num2 < 8; num1++, num2++)
            {
                if (tool[num1, num2].getColor() == (numTurn == 1 ? 2 : 1))
                {
                    tool[num1, num2].setAvailable(1);
                    testmove = true;
                    if (duringTesting1 && tool[num1, num2] is King)
                        continue;
                    else
                        break;
                }
               
                if (tool[num1, num2].getColor() == 0)
                {
                    tool[num1, num2].setAvailable(1);
                    testmove = true;
                }
                else
                    break;

            }

            num1 = n1 + 1;
            num2 = n2 - 1;
            for (; num1 < 8 && num2 >= 0; num1++, num2--)
            {
                if (tool[num1, num2].getColor() == (numTurn == 1 ? 2 : 1))
                {
                    tool[num1, num2].setAvailable(1);
                    testmove = true;
                    if (duringTesting1 && tool[num1, num2] is King)
                        continue;
                    else
                        break;
                }
            
                if (tool[num1, num2].getColor() == 0)
                {
                    tool[num1, num2].setAvailable(1);
                    testmove = true;
                }
                else
                    break;

            }

            num1 = n1 - 1;
            num2 = n2 - 1;
            for (; num1 >= 0 && num2 >= 0; num1--, num2--)
            {
                if (tool[num1, num2].getColor() == (numTurn == 1 ? 2 : 1))
                {
                    tool[num1, num2].setAvailable(1);
                    testmove = true;
                    if (duringTesting1 == true && tool[num1, num2] is King)
                        continue;
                    else
                        break;
                }
               
                if (tool[num1, num2].getColor() == 0)
                {
                    tool[num1, num2].setAvailable(1);
                    testmove = true;
                }
                else
                    break;

            }

            num1 = n1 - 1;
            num2 = n2 + 1;
            for (; num1 >= 0 && num2 < 8; num1--, num2++)
            {
                if (tool[num1, num2].getColor() == (numTurn == 1 ? 2 : 1))
                {
                    tool[num1, num2].setAvailable(1);
                    testmove = true;
                    if (duringTesting1 && tool[num1, num2] is King)
                        continue;
                    else
                        break;
                }
        
                if (tool[num1, num2].getColor() == 0)
                {
                    tool[num1, num2].setAvailable(1);
                    testmove = true;
                }
                else
                    break;

            }


            return testmove;

        }
        public int testingDanger(int n1, int n2, Tool[,] tool,int numTurn)
        {
            int colorEnemy;
            int resultDanger=0;
            colorEnemy = numTurn==1?2:1;
            tool[n1, n2].setTestingDanger(true);
          
           
            for(int i =0;i<8;i++)
            {
                for(int z =0;z<8;z++)
                {
                    if(tool[i,z].getColor()==colorEnemy  && !(tool[i,z] is King))
                    {
                        tool[i, z].testingFrom(i, z, tool, colorEnemy);

                        foreach (Tool v in tool)
                            if (v.getAvilable() == 1 && v.getTestingDanger())
                            {
                               
                                    v.setAvailable(0);
                                    v.setTestingDanger(false);                                   
                                    resultDanger++;                                                                       
                            }
                            else
                                v.setAvailable(0);       
                    }
                    
                }
            }
            tool[n1, n2].setTestingDanger(false);
            return resultDanger;   
        }

    }
}