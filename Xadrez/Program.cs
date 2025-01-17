﻿using System;

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
                    Tela.ImprimirPartida(partida);
                    Console.WriteLine();
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
                    partida.RealizarJogada(origem, destino);
                }
                catch (TabuleiroException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
            Console.Clear();
            Tela.ImprimirPartida(partida);
            Console.ReadLine();
        }
    }
}
