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
            var Sudoku = new Sudoku("410020036060940072905370040740609800009280607180003290204060305001805029053402700");

            // Skriver ut brädet i consolen 
            // Sudoko.LösSudokun();
           Sudoku.TypeBoardToConsole();
            Sudoku.SolveSudoku();

            Console.ReadLine();
        }
    }
}
