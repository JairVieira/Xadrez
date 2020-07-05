using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            var tab = new Tabuleiro(8, 8);            

            Tela.ImprimirTabuleiro(tab);

            Console.ReadLine();
        }
    }
}
