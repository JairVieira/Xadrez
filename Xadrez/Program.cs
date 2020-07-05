using System;
using Xadrez;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            var tab = new Tabuleiro(8, 8);
            tab.InserirPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
            tab.InserirPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
            tab.InserirPeca(new Rei(tab,Cor.Preta), new Posicao(2, 4));
            Tela.ImprimirTabuleiro(tab);

            Console.ReadLine();
        }
    }
}
