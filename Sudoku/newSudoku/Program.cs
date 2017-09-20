using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace newSudoku
{
    class Program
    {
        static void Main(string[] args)
        {

            var Sudoku = new Sudoku("037060000205000800006908000000600024001503600650009000000302700009000402000050360");
            Sudoku.TypeBoard();
            Sudoku.SolveBoard();


            Console.ReadLine();
        }
    }
}
