using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TestSudoku
{
    class Sudoku
    {
        private int[,] board = new int[9, 9];
        private string[] row = new string[9];
        private string[] col = new string[9];
        private string[] box = new string[9];
        private string checknr;
        private string numbers;

        public Sudoku(string Numbers)
        {
            this.numbers = Numbers;
        }

        public void TypeBoardToConsole()
        {

            int nextnumber = 0;
            int counter = 0;
            double rowlength = Math.Sqrt(board.Length);//Gör roten ur board för att få radlängd



            Console.WriteLine(" -------------------");//Grafisk erotik för Sudoku
            for (int i = 0; i < rowlength; i++)//Här gör vi loop för att få sifforna i rader, kolumner samt i boxar
            {
                for (int j = 0; j < rowlength; j++)
                {
                    board[i, j] = numbers[nextnumber] - '0'; //Subtrahera med 48 i ascii-tabell (för att undgå string till int fel) 
                    checknr = numbers[nextnumber].ToString();
                    row[i] = row[i] + numbers[nextnumber];//Sparar i en rad
                    col[j] = col[j] + numbers[nextnumber];//Sparar i en kolumn

                    if (i < 3 && j < 3)//Box 1 - Topleft
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

                    if ((j + 1) % 3 == 0 && j != rowlength - 1)//Grafisk erotik för Sudoku - sker efter varje 3:e siffra förutom 9
                    {
                        Console.Write("|");
                    }
                    else { }
                    counter++;
                    nextnumber++;

                }

                Console.WriteLine();
                if ((i + 1) % 3 == 0)//Grafisk erotik för Sudoku
                {
                    Console.WriteLine(" -------------------");
                }
                else { }
            }

            //for (int k = 0; k < rowlength; k++) // Såhär gör man när man vill söka igenom en rad
            //{
            //    Console.Write(board[k, 2]); //Detta är hela kolumn 3
            //}

        }
    }
}