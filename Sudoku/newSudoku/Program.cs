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

            var Sudoku = new Sudoku("030010809000800040007006500005004000006080000200007000010000090003008007800600401");
            Sudoku.TypeBoard();
            Sudoku.SolveBoard();


            Console.ReadLine();
        }
    }
}
