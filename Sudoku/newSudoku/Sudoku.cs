using System;
using System.CodeDom;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;

namespace newSudoku
{
    class Sudoku
    {
        // Fält
        private string sudokuNumbers;           // Nummer i sudokun
        char[,] bord = new char[9, 9];           // Skapar sudoku brädet
        private string[] row = new string[9];   // Rad arry
        private string[] col = new string[9];   // Kolumn array
        private string[] box = new string[9];   // Box arry

        private int addNumber;  // Int som används i FillBoxRowCol metoden för att få rätt rad och col
        private int boxNumber;  // Anvds i metod för att få ut rät box nr
        private bool guessNr;

        // Construktor - Tar in spelbrädet från main metod
        public Sudoku(string numberString)
        {
            this.sudokuNumbers = numberString;
        }

        // Metoder - Skriver ut brädet i consolen
        public void TypeBoard()
        {
            FillBoxRowCol();    // Fyller alla rader, kolumner och boxar

            // For-looparna skriver ut bärdet och lite skiljetecken på avsedda plattser 
            for (int colIndex = 0; colIndex < 9; colIndex++)
            {
                for (int rowIndex = 0; rowIndex < 9; rowIndex++)
                {
                    Console.Write(bord[colIndex, rowIndex] + " ");
                    if (rowIndex == 2 || rowIndex == 5)
                    {
                        Console.Write("| ");
                    }
                }
                Console.WriteLine();
                if (colIndex == 2 || colIndex == 5)
                {
                    Console.WriteLine("---------------------");
                }
            }
        }

        // Metod - Fyller boxar, rader och kloumner
        public void FillBoxRowCol()
        {
            // Nollställer alla arrys för att undivika att de kopieras istället för att bara lägga till det nya värdet
            Array.Clear(row, 0, row.Length);
            Array.Clear(col, 0, col.Length);
            Array.Clear(box, 0, box.Length);

            addNumber = 0;  // Blir indexnummer i sudokuNummer 
            boxNumber = 0;  // Blir index för att få rätt boxar

            // For-looparna går igenom sudokuNummer och fyller boxar, rader och kolumner
            for (int rowIndex = 0; rowIndex < 9; rowIndex++)
            {
                for (int colIndex = 0; colIndex < 9; colIndex++)
                {
                    boxNumber = SetBoxNumber(rowIndex, colIndex);

                    bord[rowIndex, colIndex] = sudokuNumbers[addNumber];
                    row[rowIndex] += sudokuNumbers[addNumber];
                    col[colIndex] += sudokuNumbers[addNumber];
                    box[boxNumber] += sudokuNumbers[addNumber];

                    addNumber++;
                }
            }
        }

        // Metod - Tar in två parametrar som tar reda på vart i sudokun vi är. Returnerar sedan korrekt boxnummer
        public int SetBoxNumber(int rowIndex, int colIndex)
        {
            if (rowIndex < 3 && colIndex < 3) return 0;
            if (rowIndex < 3 && colIndex < 6) return 1;
            if (rowIndex < 3 && colIndex > 5) return 2;
            if (rowIndex < 6 && colIndex < 3) return 3;
            if (rowIndex < 6 && colIndex < 6) return 4;
            if (rowIndex < 6 && colIndex > 5) return 5;
            if (rowIndex > 5 && colIndex < 3) return 6;
            if (rowIndex > 5 && colIndex < 6) return 7;
            return 8;
        }

        // Metod - Löser sudoku brädet
        public void SolveBoard()
        {
            bool sudkouIsFull = false;


            while (sudkouIsFull == false)        // Körs tills sudokun är full
            {
                int laps = 0;   // Räknar varv som används för att få ut rätt plats i sudokuNummer strängen
                bool sudokuToHard = true; // Kollar om sudokun går att lösa med uppgift 1s lösning



                // Looparna letar efter tomma platser i sudokun - Platser som innehåller '0'
                for (int rowIndex = 0; rowIndex < 9; rowIndex++)
                {
                    for (int colIndex = 0; colIndex < 9; colIndex++)
                    {
                        
                        if (bord[rowIndex, colIndex] == '0')    // Kollar om platsen är tom
                        {
                            boxNumber = SetBoxNumber(rowIndex, colIndex);   // Hämtar rätt boxnumer från metod
                            List<char> possible = GetPossibleNumber(rowIndex, colIndex);

                            //+++++++++++++++++++++++++++++++++

                            // Skickar in alla möjliga nummer och positionen i sudokun
                            // Om den char som returneras är ett nummer kommer numberOneToNine rensas och 
                            // endast lägga till det värdet
                            List<string> god = PosibleNumberToFillSudoku(possible, rowIndex, colIndex);

                            //Console.ReadLine();
                            if (god.Count == 1)
                            {
                                char nr = Convert.ToChar(god[0]);
                                possible.Clear();
                                possible.Add(nr);
                            }

                            if (possible.Count == 1) // Kollar om det bara finns ett nummer kvar     
                            {
                                sudokuToHard = false;

                                string nr = Convert.ToString(possible[0]);               // Gör om sista siffran till string
                                sudokuNumbers = sudokuNumbers.Remove(laps, 1).Insert(laps, nr); // Plockar bort ett nummer som är fel och lägger till rätt nummer

                                FillBoxRowCol();    // Fyller Box, rad och kloumn med nya värden
                                if (!sudokuNumbers.Contains("0"))   // Är sudokun full finns inga tomma platser kvar, Då löses sudokun
                                {
                                    Console.Write("\nPress ENTER to solve sudoku: ");
                                    Console.ReadLine();
                                    Console.Clear();
                                    TypeBoard();    // Skriver ut det färdiga bärdet
                                    sudkouIsFull = true;
                                }
                                
                            }
                            
                        }
                        laps++; // Ökar antalet varav
                        
                    }
                    
                }
                if (sudokuToHard == true)   // Om det inte läggs till något nytt nummer på ett varv skrivs brädet ut 
                {
                    Console.WriteLine();
                    TypeBoard();
                    Console.ReadLine();
                    // GuessNumber();
                }
            }
        }
        

        // Metod som gissar tal tills sudokun är full
        private void GuessNumber()
        {
            string guesssNumbers = sudokuNumbers;
            var guess = new Sudoku(guesssNumbers);
            for (int rowIndex = 0; rowIndex < 9; rowIndex++)
            {
                for (int colIndex = 0; colIndex < 9; colIndex++)
                {
                    if (bord[rowIndex, colIndex] == '0')
                    {
                        List<char> nr = GetPossibleNumber(rowIndex, colIndex);
                    }
                }
            }
           // guess.SolveBoard();

        }

        public List<char> GetPossibleNumber(int rowIndex, int colIndex)
        {
            List<char> NumberOneToNine = new List<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9' };  // Används för att sätta rätt nummer på plats
            foreach (var item in row[rowIndex]) // Plockar bort alla nummer som finns i rätt rad
            {
                NumberOneToNine.Remove(item);
            }

            foreach (var item in col[colIndex]) // Plockar bort alla nummer som finns i rätt kolumn
            {
                NumberOneToNine.Remove(item);
            }

            foreach (var item in box[boxNumber])// Plockar bort alla nummer som finns i rätt box
            {
                NumberOneToNine.Remove(item);
            }
            return NumberOneToNine;
        }

        public List<string> PosibleNumberToFillSudoku(List<char> numbersList, int positionRow, int positionCol)
        {
            // Temporära index för Col och Row
            int tempRowIndex = 0;
            int tempColIndex = 0;
            

            // Sätter Col och Row till rätt startvärden utifrån vilken box de tillhör
            if (boxNumber == 0)
            {
                tempRowIndex = 0;
                tempColIndex = 0;
            }
            if (boxNumber == 1)
            {
                tempRowIndex = 0;
                tempColIndex = 3;
            }
            if (boxNumber == 2)
            {
                tempRowIndex = 0;
                tempColIndex = 6;
            }
            if (boxNumber == 3)
            {
                tempRowIndex = 3;
                tempColIndex = 0;
            }
            if (boxNumber == 4)
            {
                tempRowIndex = 3;
                tempColIndex = 3;
            }
            if (boxNumber == 5)
            {
                tempRowIndex = 3;
                tempColIndex = 6;
            }
            if (boxNumber == 6)
            {
                tempRowIndex = 6;
                tempColIndex = 0;
            }
            if (boxNumber == 7)
            {
                tempRowIndex = 6;
                tempColIndex = 3;
            }
            if (boxNumber == 8)
            {
                tempRowIndex = 6;
                tempColIndex = 6;
            }


            // Skapar string listan med möjliga nummer
            List<string> possibleNumber = new List<string>();
            List<string> realNumber = new List<string>();

            // Gör om alla nummber i numbersList till string-värden och lägger till dem i nya listan
            foreach (var item in numbersList)
            {
                string posibleNr = Convert.ToString(item);
                possibleNumber.Add(posibleNr);
                realNumber.Add(posibleNr);
            }
            
            
            // Ökar radvärdet var tredje varv
            for (int xRow = tempRowIndex; xRow < tempRowIndex + 3; xRow++)
            {
                // Ökar Col värdet var tredje varv
                for (int yCol = tempColIndex; yCol < tempColIndex + 3; yCol++)
                {
                    // Kör bara om nuvarande cellen inte är cellen som ska fyllas med ett värde
                    if (xRow != positionRow || yCol != positionCol)
                    {
                        // Kollar om det är en tom cell
                        if (bord[xRow, yCol] == '0')
                        {
                            // If-satserna kollar om nummerna i possibleNumbers listan kan sitta på fler plater än
                            // Orginal cellen. Om det kan göra det tas de bort från möjliga tal
                            foreach (var item in possibleNumber)
                            {
                                if (!row[xRow].Contains(item) && !col[yCol].Contains(item))
                                {
                                    realNumber.Remove(item);

                                }
                            }
                            
                        }
                    }
                }

            }

            // Kollar om possible number endast har ett värde kvar
            // Om JA, returnerar det värdet som sedan läggs till i sudoku brädet
            // Om Nej, returneras en tom char
            
            return realNumber;
        }
    }
}

