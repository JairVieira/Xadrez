﻿namespace Xadrez
{
    class Torre : Peca
    {
        public Torre(Tabuleiro tab, Cor cor): base(tab, cor)
        {

        }

        private bool PodeMover(Posicao pos)
        {
            Peca p = Tab.ObterPeca(pos);
            return p is null || p.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            var pos = new Posicao(0, 0);

            //acima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.ObterPeca(pos) != null && Tab.ObterPeca(pos).Cor != Cor)
                    break;
                pos.Linha -= 1;
            }

            //abaixo
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.ObterPeca(pos) != null && Tab.ObterPeca(pos).Cor != Cor)
                    break;
                pos.Linha += 1;
            }

            //direita
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.ObterPeca(pos) != null && Tab.ObterPeca(pos).Cor != Cor)
                    break;
                pos.Coluna += 1;
            }

            //esquerda
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.ObterPeca(pos) != null && Tab.ObterPeca(pos).Cor != Cor)
                    break;
                pos.Coluna -= 1;
            }

            return mat;
        }

        public override string ToString()
        {
            return "T";
        }
    }
}
