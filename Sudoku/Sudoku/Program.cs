using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestSudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            // Skickar in en sträng med alla nummer som ska fylla Sudokon
            var Sudoku = new Sudoku("003020600900305001001806400008102900700000008006708200002609500800203009005010300");

            // Skriver ut brädet i consolen 
           // Sudoko.LösSudokun();
           Sudoku.TypeBoardToConsole();

            Console.ReadLine();
        }
    }
}
