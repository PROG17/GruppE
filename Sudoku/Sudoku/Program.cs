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
            var Sudoku = new Sudoku("384652090000800000792430000" +
                                    "060304007173000264400106030" +
                                    "000045128000003000050968473");

            // Skriver ut brädet i consolen 
           // Sudoko.LösSudokun();
           Sudoku.TypeBoardToConsole();
            Sudoku.SolveSudoku();

            Console.ReadLine();
        }
    }
}
