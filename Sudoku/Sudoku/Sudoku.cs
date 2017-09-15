using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TestSudoku
{
    class Sudoku
    {

        // Fält
        private int[,] board = new int[9, 9];              //2-dimensionell array för brädet
        private string[] row = new string[9];              //string array för raderna (lodrätt)
        private string[] col = new string[9];              // string array för kolumnerna (vågrätt)
        private string[] box = new string[9];              //string array för box
        private string numbers;                            //string för Inputsiffrorna från MAIN.
        //  private int solvedNumbers = 81;


        // Konstruktor
        public Sudoku(string Numbers)                        //konstruktor för att sätta in siffror i main.
        {
            this.numbers = Numbers;
        }

        // Metod
        public void TypeBoardToConsole()                    //Metod för att fylla alla arrayer med siffror på rätt plats
        {

            int nextnumber = 0;                             //nextnumber är en counter för index
            double rowlength = Math.Sqrt(board.Length);     //Gör roten ur board för att få radlängd    (Kan egentligen bara vara = 9)


            Console.WriteLine(" ---------------------");    //Grafisk erotik för Sudoku
            for (int i = 0; i < rowlength; i++)             //Här gör vi loop för att få sifforna i rader, kolumner samt i boxar
            {                                               //Denna forloopen går lodrätt.
                for (int j = 0; j < rowlength; j++)         //Denna forloop går vågrätt. när den har gått 9 ggr byter den till nästa rad. (nedåt)
                {
                    board[i, j] = numbers[nextnumber] - '0'; //Subtrahera med 48 i ascii-tabell (för att undgå string till int fel) 
                    row[i] += numbers[nextnumber];           //Sparar i en rad
                    col[j] += numbers[nextnumber];           //Sparar i en kolumn
                    
                    //if (board[i,j] == 0)
                    //{
                    //    solvedNumbers--;
                    //}

                    if (i < 3 && j < 3)//Box 1 - Topleft                    // Tilldelar siffran rätt box
                    {
                        box[0] = box[0] + numbers[nextnumber];
                    }
                    else if (i < 3 && j < 6 && j > 2)//Box 2 - Topmid
                    {
                        box[1] = box[1] + numbers[nextnumber];
                    }
                    else if (i < 3 && j < 9 && j > 5)//Box 3 - Topright
                    {
                        box[2] = box[2] + numbers[nextnumber];
                    }
                    else if (i < 6 && i > 2 && j < 3)//Box 4 - Midleft
                    {
                        box[3] = box[3] + numbers[nextnumber];
                    }
                    else if (i < 6 && i > 2 && j < 6 && j > 2)//Box 5 - Midmid
                    {
                        box[4] = box[4] + numbers[nextnumber];
                    }
                    else if (i < 6 && i > 2 && j < 9 && j > 5)//Box 6 - Midright
                    {
                        box[5] = box[5] + numbers[nextnumber];
                    }
                    else if (i < 9 && i > 5 && j < 3)//Box 7 - Botleft
                    {
                        box[6] = box[6] + numbers[nextnumber];
                    }
                    else if (i < 9 && i > 5 && j < 6 && j > 2)//Box 8 - Botmid
                    {
                        box[7] = box[7] + numbers[nextnumber];
                    }
                    else //Box 9 - Botright
                    {
                        box[8] = box[8] + numbers[nextnumber];
                    }

                    Console.Write(" " + board[i, j]);

                    if ((j + 1) % 3 == 0 && j != rowlength - 1) //Grafisk erotik för Sudoku - sker efter varje 3:e siffra förutom 9
                    {
                        Console.Write(" |");
                    }
                    nextnumber++;

                }

                Console.WriteLine();
                if ((i + 1) % 3 == 0)//Grafisk erotik för Sudoku
                {
                    Console.WriteLine(" ---------------------");
                }
            }
        }

        public void SolveSudoku()
        {

            Console.ReadLine();
            int colPlace = 0;
            int rowPlace = 0;
            string lol = "";
            bool sudokuIsFull = false;

            while (sudokuIsFull == false)
            {

                for (int colNumber = 0; colNumber < 9; colNumber++)
                {

                    for (int rowNumber = 0; rowNumber < 9; rowNumber++)
                    {

                        List<char> NumbersOneToNine = new List<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

                        if (rowPlace > 8)
                        {
                            rowPlace = 0;
                        }

                        else if (colPlace > 8)
                        {
                            rowPlace++;

                            colPlace = 0;
                        }
                        else if (board[rowPlace, colPlace] == 0)
                        {
                            // Plockar bort alla nummer från listan som förekommer i nuvarande raden
                            foreach (var item in row[rowPlace])
                            {
                                if (NumbersOneToNine.Contains(item))
                                {
                                    NumbersOneToNine.Remove(item);
                                }
                            }

                            // Plockar bort alla nummer från listan som förekommer i nuvarande Kolumnen

                            foreach (var item in col[colPlace])
                            {
                                if (NumbersOneToNine.Contains(item))
                                {
                                    NumbersOneToNine.Remove(item);

                                }
                            }

                            // Plockar bort alla nummer från listan som förekommer i nuvarande boxen
                            int boxNummer = BoxCheckMethod(rowPlace, colPlace);
                            foreach (var item in box[boxNummer])
                            {

                                if (NumbersOneToNine.Contains(item))
                                {
                                    NumbersOneToNine.Remove(item);
                                }
                            }

                            // Om det endast finns ett nummer i listan ska det placeras på rätt plats i sudokun
                            if (NumbersOneToNine.Count == 1)
                            {
                                board[rowPlace, colPlace] = NumbersOneToNine[0] - '0';
                                string hi = NumbersOneToNine[0].ToString();

                             // lol = lol + board[rowPlace, colPlace];

                                row[rowPlace] = row[rowPlace] + hi;
                                col[colPlace] = col[colPlace] + hi;
                                box[boxNummer] = box[boxNummer] + hi;

                                // solvedNumbers++;

                                int totlaSum = 0;
                                foreach (var item in board)
                                {
                                    totlaSum = totlaSum + item;
                                }
                                if (totlaSum == 405) { sudokuIsFull = true; }
 

                                //När alla fält har blivit lösta, skriv ut brädet
                                //if (solvedNumbers == 81)
                                //{
                                //    Console.Clear();
                                //    WriteComliteSudoku();
                                //    sudokuIsFull = true;
                                //}
                                
                            }

                            colPlace++;
                        }
                        else
                        {
                            colPlace++;
                        }
                    }
                }
            }
            WriteCompleteSudoku();
        }

        public void WriteCompleteSudoku()
        {

            Console.Clear();
            
            
            // Skriver ut det nya sudokubrädet
            Console.WriteLine(" ---------------------");
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {

                    Console.Write(" " + board[i, j]);

                    if ((j + 1) % 3 == 0 && j != 9 - 1)//Grafisk erotik för Sudoku - sker efter varje 3:e siffra förutom 9
                    {
                        Console.Write(" |");
                    }
                }
                Console.WriteLine();
                if ((i + 1) % 3 == 0)//Grafisk erotik för Sudoku
                {
                    Console.WriteLine(" ---------------------");
                }
            }
            Console.WriteLine();
            
        }

        public int BoxCheckMethod(int i, int j)
        {
            int boxNr;
            if (i < 3 && j < 3)
            {
                boxNr = 0;              //Box 1 - Topleft
                return boxNr;
            }
            else if (i < 3 && j < 6)
            {
                boxNr = 1;              //Box 2 - topmid
                return boxNr;
            }
            else if (i < 3 && j < 9)//Box 3 - Topright
            {
                boxNr = 2;
                return boxNr;
            }
            else if (i < 6 && j < 3)//Box 4 - Midleft
            {
                boxNr = 3;
                return boxNr;

            }
            else if (i < 6 && j < 6)//Box 5 - Midmid
            {
                boxNr = 4;
                return boxNr;

            }
            else if (i < 6 && j > 5)//Box 6 - Midright
            {
                boxNr = 5;
                return boxNr;
            }
            else if (i < 9 && j < 3)//Box 7 - Botleft
            {
                boxNr = 6;
                return boxNr;
            }
            else if (i < 9 && j < 6)//Box 8 - Botmid
            {
                boxNr = 7;
                return boxNr;
            }
            else //Box 9 - Botright
            {
                boxNr = 8;
                return boxNr;
            }

        }
    }
}