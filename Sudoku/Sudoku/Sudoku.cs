using System;
using System.Collections.Generic;

namespace Sudoku
{
    class Sudoku
    {
        // ====
        // Fält
        // ====
        private string sudokoNummer;

        // String med nummer som ska finnas med
        private string[] nummerEttTillNio = new string[]{ "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        // Lista som sparar alla nummer i sudokun som inte är 0
        List<char> nummerSomFinns = new List<char>();

        // Skapar alla 9o boxar som ska inehålla "nummerEttTillNio"
        private char[] topLeftBox = new char[9];
        private char[] topMideBox = new char[9];
        private char[] topRightBox = new char[9];
        private char[] middleLeftBox = new char[9];
        private char[] middleMidBox = new char[9];
        private char[] middleRightBox = new char[9];
        private char[] bottomLeftBox = new char[9];
        private char[] bottomMidBox = new char[9];
        private char[] bottomRightBox = new char[9];

        // individuell counter för varje box
        // Går säkert att göra på ett snyggare sätt
        private int box1 = 0;
        private int box2 = 0;
        private int box3 = 0;
        private int box4 = 0;
        private int box5 = 0;
        private int box6 = 0;
        private int box7 = 0;
        private int box8 = 0;
        private int box9 = 0;


        // ===========
        //Konstruktors
        //============

        // Konstruktor som sätter värdet på sudokoNummer
        public Sudoku(string SudokuNummer)
        {
            this.sudokoNummer = SudokuNummer;
        }
        

        // =======
        // METODER
        // =======
        public void SkrivUtSudokoBräde()
        {
            // Räknare för att få ut korrekt grafik i consolen
            int countRow = 1;
            int countCol = 1;
            int indexOfSudokuArrayValu = 0;


            // Två loopar som går igenom alla nummer i sudokon
            Console.WriteLine("---------------------");
            for (int col = 0; col < 9; col++)
            {

                // Inre loopen skiver ut varje nummer på en plats
                for (int row = 0; row < 9; row++)
                {
                    char tecken = sudokoNummer[indexOfSudokuArrayValu];
                    Console.Write(tecken + " ");
                    if (countRow == 3 || countRow == 6) // if-satsen för visuel effekt
                    {
                        Console.Write("| ");
                    }

                    // Fyller listan med alla nummer som inte är 0/Tomma platser
                    if (tecken != '0')
                    {
                        nummerSomFinns.Add(tecken);
                        FyllBoxarna(col, row, tecken);
                    }

                    // Fyller boxarna med värden
                    // FyllBoxarna(col, row, tecken);
                    countRow++;
                    indexOfSudokuArrayValu++; // Går till nästa värde som ska skrivas ut

                }

                Console.WriteLine();
                if (countCol == 3) // if-sats för visuel effekt
                {
                    Console.WriteLine("---------------------");
                    countCol = 0;
                }

                countCol++;
                countRow = 1;
                
            }

            
        }

        // Fyller Boxarna med värden
        public void FyllBoxarna(int col, int row, char tecken)
        {
            // Topp Till vänster
            if (col < 3 && row < 3)
            {
                topLeftBox[box1] = tecken;
                box1++;
            }
            // Toppen i mitten
            else if (col < 3 && row < 6)
            {
                topMideBox[box2] = tecken;
                box2++;
            }
            // Toppen till höger
            else if (col < 3 && row > 5)
            {
                topRightBox[box3] = tecken;
                box3++;
            }
            // Mitten till vänster
            else if (col < 6 && row < 3)
            {
                middleLeftBox[box4] = tecken;
                box4++;
            }
            // Mitten i Mitten
            else if (col < 6 && row < 6)
            {
                middleMidBox[box5] = tecken;
                box5++;
            }
            // Mitten till Höger
            else if (col < 6 && row > 5)
            {
                middleRightBox[box6] = tecken;
                box6++;
            }
            //Botten till vänster
            else if (col > 5 && row < 3)
            {
                bottomLeftBox[box7] = tecken;
                box7++;
            }
            // Botten i Mitten
            else if (col > 5 && row < 6)
            {
                bottomMidBox[box8] = tecken;
                box8++;
            }
            // Botten till Höger
            else
            {
                bottomRightBox[box9] = tecken;
                box9++;
            }

        }

        // Onödig metod som ska tas bord innan allt är klar 
        // Skriver endast ut alla boxar och dess nummer
        public void SkrivUtAllaBoxar()
        {
            Console.WriteLine("Top Left Box");
            foreach (var item in topLeftBox)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Top Mid Box");
            foreach (var item in topMideBox)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Top Rigt box");
            foreach (var item in topRightBox)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Mid left box");
            foreach (var item in middleLeftBox)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Mid mid box");
            foreach (var item in middleMidBox)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Mid Right Box");
            foreach (var item in middleRightBox)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Bottom left");
            foreach (var item in bottomLeftBox)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Bottom middle box");
            foreach (var item in bottomMidBox)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Bottom riight box");
            foreach (var item in bottomRightBox)
            {
                Console.Write(item + " ");
            }
        }

        public void LösSudokun()
        {
            // 
            int idexOfSudokuArrayValu = 0;

            // Två loopar som går igenom alla nummer i sudokon
            Console.WriteLine();
            for (int col = 0; col < 9; col++)
            {

                // Inre loopen skiver ut varje nummer på en plats
                for (int row = 0; row < 9; row++)
                {
                    char nummerICell = sudokoNummer[idexOfSudokuArrayValu];

                    if (nummerSomFinns.Contains(nummerICell))
                    {
                        Console.WriteLine("Nummret finns ");
                    }

                    else if (nummerSomFinns.Contains(nummerICell) == false)
                    {
                        Console.WriteLine("nummret fins inte");
                    }

                    idexOfSudokuArrayValu++; // Går till nästa värde som ska skrivas ut

                }

                Console.ReadLine();
            }
        }

    }
}