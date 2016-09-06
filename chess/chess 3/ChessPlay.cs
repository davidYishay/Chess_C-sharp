using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_3
{
    class ChessPlay
    {   

        string from, to, player1, player2;
        bool turn = false; // flase = turn black ;     
        int counter;
        Tool[,] tool;
           
        public bool startPlay()
        {      
          tool = makeTools();
          duringPlay();
            return true;
        }
        public void duringPlay()
        {
            bool win = false;

            colorYellow("Player 1"); Console.Write(" Write your name ");
            player1 = Console.ReadLine();
            Console.WriteLine();
            colorRed("Player 2"); Console.Write(" Write your name ");
            player2 = Console.ReadLine();
            Console.WriteLine();
            makeCastling(); // אתחול הצרחה
            print();

            do
            {
                do
                {
                    Console.WriteLine();
                    if (turn == false)
                        colorYellow(player1);
                    else
                        colorRed(player2);
                    Console.Write(" queue, select Tools ");
                    from = Console.ReadLine();
                } while (!CheckValidity(from, "1"));
                print();
                do
                {
                    Console.Write(" to  ");
                    to = Console.ReadLine();
                } while (!CheckValidity(from, to));

                Console.Clear();
                win = checkMate();
                print();
                counter++;
                cleanMate();
                turn = !turn;
            } while (!win);
            endTheGame();    
        }
        public bool CheckValidity(string input, string durnig)
        {
            int n1 = 0;
            int n2 = 0;
            int n3 = 0;
            int n4 = 0;
            int circle = durnig == "1" ? 1 : 2;
            for (int i = 0; i < circle; i++)
            {
                bool correctly = false;

                string locat1 = "";
                string locat2 = "";
                string inputTest;
                string test = "012345678 ";
                string test2 = "abcdefgh";
                inputTest = i == 0 ? input.Trim() : durnig.Trim(); // טרנארי 

                if (inputTest.Length != 2)
                    return false;

                for (int z = 0; z < test.Length; z++)  // input[0]
                {
                    if (inputTest[0] == test[z])
                    {
                        locat1 += test[z - 1];
                        correctly = true;
                    }
                }
                if (correctly == false)
                    return false;

                correctly = false;
                for (int z = 0; z < test2.Length; z++) // input[1] 
                {
                    if (inputTest[1] == test2[z])
                    {
                        locat2 += test[z];
                        correctly = true;
                    }
                }
                if (correctly == false)
                    return false;

                if (i == 0)
                {
                    n1 = int.Parse(locat1);
                    n2 = int.Parse(locat2);
                }
                if (i == 1)
                {
                    n3 = int.Parse(locat1);
                    n4 = int.Parse(locat2);
                }
            }

            int turnNum = turn ? 2 : 1;
               
            if (circle == 1 && tool[n1,n2].getColor()== turnNum)
                return tool[n1,n2].testingFrom(n1, n2, tool,turnNum );
            else
                return toMove(n1, n2, n3, n4);
        }
        public bool toMove(int n1, int n2, int n3, int n4)
        {
            
            if (tool[n3, n4].getAvilable() == 1)
            {
                if (tool[n1, n2] is Pawn && tool[n3, n4].getEnPassant() == (turn == false ? 2 : 1)) // בדיקה האם אפשרי הכאה דך הילוכו 
                {
                    if (turn == false)
                        tool[n3 - 1, n4] = new Space(0);
                    if (turn == true)
                        tool[n3 + 1, n4] = new Space(0);
                }
                foreach (Tool i in tool) // זכות הכאה דרך הילכו לתור אחד בלבד
                    if (i.getEnPassant() == (turn == false ? 2 : 1))
                        i.SetEnPassant(0);

                if (tool[n1, n2] is King && (n2 + 2 == n4 || n2 - 2 == n4)) // ביצוע הצרחה 
                {
                    if (n2 + 2 == n4)
                    {
                        tool[n1, 5] = tool[n1, 7];
                        tool[n1, 7] = new Space(0);
                    }
                    if (n2 - 2 == n4)
                    {
                        tool[n1, 3] = tool[n1, 0];
                        tool[n1, 0] = new Space(0);
                    }

                }
                tool[n3, n4] = tool[n1, n2]; // החלפת מקום  או קרב - באופן רגיל 
                tool[n1, n2] = new Space(0);
                if (tool[n3, n4] is Rook || tool[n3, n4] is King)
                {
                    tool[n3, n4].castling = false;
                }
                if (tool[n3, n4] is Pawn && turn == false ? (n3 == 3 && n1 == 1) : (n3 == 4 && n1 == 6)) // מסמן הכאה דרך הילוכו 
                {
                    if (turn == false)
                    {
                        tool[n3 - 1, n4].SetEnPassant(1);
                    }
                    if (turn == true)
                    {
                        tool[n3 + 1, n4].SetEnPassant(2);
                    }
                }
                if (tool[n3, n4] is Pawn && n3 == (turn == false ? 7 : 0))     
                    ((Pawn)tool[n3, n4]).coronation(n3, n4, tool, (turn ? 2 : 1));  //coronation(n3, n4);// שולח לפונקצית הכתרה
                      
                cleanAvailable(); // מנקה סימונים 
                return true;
            }
            return false;
        }
        public bool cleanAvailable()
        {
            foreach (Tool z in tool)
                z.setAvailable(0);
            
            return true;
        }
        public bool makeCastling()
        {
            foreach (Tool z in tool)
            {
                if (z is King || z is Rook)
                    z.castling = true;
            }
            return true;
        }           
        public bool checkMate()
        {
            Tool[,] toolCheck;
            toolCheck = tool;

            bool turnTrue = turn;
            int PossibleMoves = 0;

            for (int i = 0; i < 8; i++) // בדיקת מקומות פנוים של המלך 
            {
                for (int z = 0; z < 8; z++)
                {
                    if (toolCheck[i, z] is King && toolCheck[i, z].getColor() == (turn == false ? 2 : 1))
                    {
                        turn = (turn == false ? true : false);
                        toolCheck[i, z].setMate(1);
                        PossibleMoves++;
                        if (true == toolCheck[i,z].testingFrom(i, z,toolCheck,(turn?2:1)))
                        {
                            foreach (Tool t in toolCheck)
                            {
                                if (t.getAvilable() == 1)
                                {
                                    t.setMate(1);
                                    PossibleMoves++;
                                }
                            }
                        }
                    }
                    turn = turnTrue;
                }
            }
            Console.WriteLine("level1 " + PossibleMoves);
            cleanAvailable();
            turn = turnTrue;
            /////////////////////////////////////////////////////////////////////////////// מדליק מצב בדיקה 
            foreach (Tool q in toolCheck) 
                q.setDuringTesting(true);
            //////////////////////////////////////////////////////////////////////////// האם יש מקום שמאוים פעמיים 
            int tr=0;
           for (int q=0; q < 8 ; q++)
                for (int s = 0;s<8 ;s++ )
                {
                    if( toolCheck[q,s].getMate()==1 && 1<(tr=toolCheck[q,s].testingDanger(q,s,toolCheck,(turn == false ? 2 : 1))))
                    {
                        Console.WriteLine(toolCheck[q, s] + " " + tr);
                        PossibleMoves--;
                        q = 8;
                        break;
                    }
                }
          ////////////////////////////////////////////////////////////////////////////////
                    for (int i = 0; i < 8; i++)  // בדיקה: האם אפשרי שחיילים אחרים יכנסו לערוגת המלך או סביבותיו 
                    {
                        for (int z = 0; z < 8; z++)
                        {
                            if (toolCheck[i, z].testingFrom(i, z, toolCheck, (turn ? 2 : 1)) && toolCheck[i, z].getColor() == (turn == false ? 1 : 2))
                            {

                               
                                foreach (Tool t in toolCheck)
                                {
                                    if (t.getAvilable() == 1 && t.getMate() == 1)
                                    {
                                        PossibleMoves--;
                                        Console.WriteLine(toolCheck[i, z] + " level2  " + PossibleMoves);
                                        t.setMate(0);
                                    }
                                }
                            }
                            cleanAvailable();
                        }
                    }         
            Console.WriteLine(PossibleMoves + ";level end ");

            foreach (Tool a in toolCheck)
                a.setTestingDanger(false);
            return (PossibleMoves > 0 ? false : true);
        } 
        public bool cleanMate()
        {
            foreach (Tool z in tool)
                z.setMate(0);
            return true;
        }      
        public void endTheGame()
        {
            Console.WriteLine((turn == false ? player2 : player1) + " is the winner of the game!\n Total moves " + (counter + 1 / 2));
        }
        public Tool[,] makeTools()
        {
            Tool[,] bord;
            bord = new Tool[8, 8];
            for (int z = 0; z < 8; z++)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (z < 2)
                        bord[z, i] = new Pawn(1);                 
                    if (z < 6 && z > 1)
                        bord[z, i] = new Space(0) ;
                    if (z > 5)
                        bord[z, i] = new Pawn(2);
                }
            }
            bord[0, 2] = new Bishop(1);
            bord[0, 5] = new Bishop(1);
            bord[7, 2] = new Bishop(2);
            bord[7, 5] = new Bishop(2);
            bord[0, 1] = new Knight(1);
            bord[0, 6] = new Knight(1);
            bord[7, 1] = new Knight(2);
            bord[7, 6] = new Knight(2);
            bord[0, 7] = new Rook(1);//
            bord[7, 7] = new Rook(2);
            bord[7, 0] = new Rook(2);
            bord[0, 3] = new Qween(1); ;
            bord[7, 3] = new Qween(2);
            bord[0, 4] = new King(1);
            bord[7, 4] = new King(2);

            return bord;
        }
        public void print()
        {
            Console.WriteLine();
            string show = "      a      b       c       d       e       f       g       h       ";
            //colorGreen(show);
            Console.WriteLine();
            show = "";// תוספת 

            for (int i = 0; i < 8; i++)
            {
                for (int z = 0; z < 8; z++)
                {

                    if (z == 0)
                    {
                        Console.WriteLine();
                        colorGreen("    " + (i + 1) + " |");
                    }
                    if (tool[i, z].getAvilable() == 1) // לא לשכוח לבטל סימון!
                    {
                        backgroundColorBlue();
                    }
                    if (tool[i, z].getColor() == 0)
                    {
                        Console.Write(tool[i, z] + "| ");
                        backgroundColorBlack();
                        continue;
                    }
                    
                    /*  if(board[i,z].getMate()==1) // checkMate ביצוע בדיקה ל 
                       {
                           backgroundColorMagenta();
                       }*/
                  
                    if (tool[i, z].getColor() == 1)
                    {
                        colorYellow(tool[i, z].ToString());
                        Console.Write("| ");
                        backgroundColorBlack();
                        continue;
                    }
                    if (tool[i, z].getColor() == 2)
                    {
                        colorRed(tool[i, z].ToString());
                        Console.Write("| ");
                        backgroundColorBlack();
                        continue;
                    }                   
                    
                }

                Console.WriteLine("\n   ------------------------------------------------------------------- ");

            }
            show = "         a       b       c       d       e       f       g       h       ";
            colorGreen(show);
            Console.WriteLine();
        }
        public void colorRed(string color)
        {

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(color);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public void colorGreen(string color)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(color);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public void colorYellow(string color)
        {

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(color);
            Console.ForegroundColor = ConsoleColor.Gray;


        }
        public void backgroundColorBlue()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
        }
        public void backgroundColorBlack()
        {
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public void backgroundColorMagenta()
        {
            Console.BackgroundColor = ConsoleColor.Green;
        }
        ////////////////////////////////////////////////////////////////////////
    
            

    }
}
