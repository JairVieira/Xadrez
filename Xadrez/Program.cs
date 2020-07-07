using System;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {

            var partida = new PartidaDeXadrez();
            while (!partida.PartidaTerminou)
            {
                try
                {
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.Tab);

                    Console.WriteLine();
                    Console.WriteLine($"Turno: {partida.Turno}");
                    Console.WriteLine($"Aguardando jogada: {partida.JogadorAtual}");
                    Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                    partida.ValidarPosicaoOrigem(origem);
                    bool[,] possicoesPossiveis = partida.Tab.ObterPeca(origem).MovimentosPossiveis();

                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.Tab, possicoesPossiveis);
                    Console.WriteLine();
                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
                    partida.ValidarPosicaoDestino(origem, destino);
                    partida.RealizaJogada(origem, destino);
                }
                catch (TabuleiroException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }

            }
            Console.ReadLine();
        }
    }
}
