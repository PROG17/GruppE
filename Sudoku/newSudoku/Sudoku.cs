using System;
using System.CodeDom;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Policy;

namespace newSudoku
{
    class Sudoku
    {
        // Fält för brädet 
        private int[,] sudokuBoard;

        // Konstruktor tar in en string och sätter alla värden till brädet
        public Sudoku(string allNumbers)
        {
            // Skapar en 2d array för bärdet
            sudokuBoard = new int[9, 9];

            // Går igenom alla 81 nummer
            for (int i = 0; i < allNumbers.Length; i++)
            {
                int row = i / 9;        // Sätter raden genom att dela med 9
                int column = i % 9;     // Sätter kolumn med modilus
                if ((row < 9) && (column < 9))
                {

                    this.sudokuBoard[row, column] = int.Parse(allNumbers.Substring(i, 1));
                }
            }
            PrintBoard();
            Console.WriteLine();
            Console.Write("Press ENTER to solve...");
            Console.ReadLine();
        }

        // Konstruktor - Anropas när det är dax att gissa 
        public Sudoku(int[,] newBoard)
        {
            sudokuBoard = new int[9, 9];

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    this.sudokuBoard[row, col] = newBoard[row, col];
                }
            }

        }

        // METOD - Skriver ut Sudokut på konsolen
        public void PrintBoard()
        {
            for (int rowiIndex = 0; rowiIndex < 9; rowiIndex++)
            {
                for (int colIndex = 0; colIndex < 9; colIndex++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(sudokuBoard[rowiIndex, colIndex] + " ");
                    Console.ResetColor();

                    if (colIndex == 2 || colIndex == 5)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("| ");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
                if (rowiIndex == 2 || rowiIndex == 5)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("---------------------");
                    Console.ResetColor();
                }
            }
        }

        // METOD - Löser brädet och returnerar om löset eller ej
        public bool Solve()
        {

            // Kör tills det inte går längre. Är sudokun inte löst skickas man in i gissnings metod
            bool toHard = false;
            while (toHard == false)
            {
                toHard = true;
                // Går igenm brädet och letar efter en tom cell
                for (int rowIndex = 0; rowIndex < 9; rowIndex++)
                {
                    for (int colIndex = 0; colIndex < 9; colIndex++)
                    {
                        if (sudokuBoard[rowIndex, colIndex] == 0)
                        {
                            // Skpar en lista med möjliga tal som kan sitta i cellen
                            List<int> PossibleNumbers = GetPossibleNumbers(rowIndex, colIndex);

                            // Om listan innehåller ett nummer kommer det placeras i sudokun
                            if (PossibleNumbers.Count == 1)
                            {
                                sudokuBoard[rowIndex, colIndex] = PossibleNumbers[0];

                                // Tohard blir 
                                toHard = false;
                            }
                        }
                    }
                }
            }

            int totalSum = 0;
            foreach (var item in sudokuBoard)
            {
                totalSum = totalSum + item;
                if (totalSum == 405)
                {
                    return true;
                }
            }

            // Det gick inte att lösa sudokut med logik, så nu måste vi göra en gissning
            // Gissa en lösning och returnera en bool som anger om gissningen gick bra          
            bool resultGuessing = GuessSolution();
            return resultGuessing;
        }

        // METOD - Returnerar en lista på dom siffror som är inskrivna i en viss rad
        public List<int> GetPossibleNumbers(int row, int col)
        {
            List<int> numberOneToNine = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, };

            //  List<int> retunrNumbers = new List<int>();
            for (int rowIndex = 0; rowIndex < 9; rowIndex++)
            {
                int colNumber = sudokuBoard[rowIndex, col];
                numberOneToNine.Remove(colNumber);
                for (int colIndex = 0; colIndex < 9; colIndex++)
                {
                    int rowNumber = sudokuBoard[row, colIndex];
                    numberOneToNine.Remove(rowNumber);
                }
            }

            List<int> boxNumbers = GetNumbersInBox(row, col);

            foreach (var item in boxNumbers)
            {
                numberOneToNine.Remove(item);
            }

            return numberOneToNine;
        }

        // METOD - Returnerar en lista på dom siffror som är inskrivna i samma ruta
        private List<int> GetNumbersInBox(int row, int col)
        {
            List<int> numbersInBox = new List<int>();
            int startRow = row / 3;
            int startCol = col / 3;

            for (int boxRow = startRow * 3; boxRow < (startRow + 1) * 3; boxRow++)
            {
                for (int boxCol = startCol * 3; boxCol < (startCol + 1) * 3; boxCol++)
                {
                    int number = sudokuBoard[boxRow, boxCol];
                    if (number > 0)
                    {
                        numbersInBox.Add(number);
                    }
                }
            }
            return numbersInBox;
        }

        // METOD - Gissar tills löst eller ej går längre
        private bool GuessSolution()
        {
            int tempRow = 0;
            int tempCol = 0;

            // Hämtar den sista tomma cellen i brädet
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (sudokuBoard[row, col] == 0)
                    {
                        // Sätter de temporära variablerna till brädeds possition
                        tempRow = row;
                        tempCol = col;
                    }
                }
            }

            // Om ingen tom cell hittas gick det inte att lösa
            if (tempRow == 0 || tempCol == 0)
            {
                return false;
            }

            List<int> numbersToGuess = GetPossibleNumbers(tempRow, tempCol);
            while (numbersToGuess.Count > 0)      // Det finns fler siffror som man kan gissa på för denna cell 
            {
                // Gissa på nästa möjliga siffra för cellen
                int guess = numbersToGuess[0];

                // Lägger till en gissning i brädet
                sudokuBoard[tempRow, tempCol] = guess;

                // Skapar en ny instans av klassen med en gissning, Skickar till konstruktorn som tar emot Int
                Sudoku guessSudoku = new Sudoku(sudokuBoard);

                // Försöker lösa med den nya gissningen
                bool result = guessSudoku.Solve();

                if (result == true)    // Sudokut är löst, kopiera den rätta lösningen och returnera true  
                {
                    sudokuBoard = guessSudoku.sudokuBoard;
                    return true;
                }

                // Sudokut är inte löst och det testade nummret tas bort från möjliga gissningar
                numbersToGuess.Remove(guess);
            }

            // Om sudokun är olösbar
            return false;
        }
    }
}

