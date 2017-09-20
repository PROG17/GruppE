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

            var Sudoku = new Sudoku("010008932000010050038524100100000379000701000743000008001849720090060000867300090");
            Sudoku.TypeBoard();
            Sudoku.SolveBoard();


            Console.ReadLine();
        }
    }
}
