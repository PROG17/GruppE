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

            string lätt1 = "100020003080090070500306008205000709003201500807000104700908001020060040600010007";
            string lätt2 = "010008932000010050038524100100000379000701000743000008001849720090060000867300090";
            string lätt3 = "003020600900305001001806400008102900700000008006708200002609500800203009005010300"; // Skoluppgift 2.1
            string medel1 = "000370000002004000000006005400000930000082640001700000570000200260105000000090003";
            string medel2 = "037060000205000800006908000000600024001503600650009000000302700009000402000050360"; // Skoluppgift 2.2
            string svår = "030010809000800040007006500005004000006080000200007000010000090003008007800600401";

            var Sudoku = new Sudoku(svår);
            // Sudoku.TypeBoard();
            Sudoku.SolveBoard();


            Console.ReadLine();
        }
    }
}
