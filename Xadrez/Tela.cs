using System;

namespace Xadrez
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (tab.ObterPeca(i, j) is null)
                        Console.Write("- ");
                    else
                        Console.Write(tab.ObterPeca(i, j) + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
