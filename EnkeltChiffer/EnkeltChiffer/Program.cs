using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnkeltChiffer
{
    class Program
    {
        static void Main(string[] args)
        {
            
            while (true)
            {
                Console.WriteLine("Skriv in en text i versaler: ");
                string text = Console.ReadLine();
                text = text.ToUpper();
                text = text.Replace(" ", "");
                text = text.Replace("!", "");
                text = text.Replace(".", "");
                text = text.Replace("?", "");

                Console.WriteLine("Skriv in ett tal som nyckel: ");
                int shift = int.Parse(Console.ReadLine());

                Console.WriteLine("Du skrev in " + text);
                Console.WriteLine("Resultat: " + Caesar(text, shift));
            }
                       
        }
        private static string Caesar(string text, int shift)
        {
            char[] chiffer = text.ToCharArray();

            for (int i = 0; i < chiffer.Length; i++)
            {
                
                char newchar = chiffer[i];
               
                newchar = (char)(newchar + (shift%26));
                if (newchar > 'Z')
                {
                    newchar = (char)(newchar - 26);
                }
                else if (newchar < 'A')
                {
                    newchar = (char)(newchar + 26);
                }
                chiffer[i] = newchar;
            }
            return new string(chiffer);
        }
    }
}
