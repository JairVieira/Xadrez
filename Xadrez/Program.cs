using System;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            var pos = new PosicaoXadrez('c', 7);

            Console.WriteLine(pos);
            Console.WriteLine(pos.ToPosicao());
            //try
            //{
            //    var tab = new Tabuleiro(8, 8);
            //    tab.InserirPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
            //    tab.InserirPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
            //    tab.InserirPeca(new Rei(tab, Cor.Preta), new Posicao(2, 4));
            //    Tela.ImprimirTabuleiro(tab);                
            //}
            //catch (TabuleiroException e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            Console.ReadLine();
        }
    }
}
