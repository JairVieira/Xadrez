using System;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {            
            try
            {
                var partida = new PartidaDeXadrez();
                while (!partida.PartidaTerminou)
                {
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.Tab);

                    Console.Write("");
                    Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();

                    partida.ExecutarMovimento(origem, destino);
                }
                
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
