﻿ namespace Xadrez
{
    class Bispo : Peca
    {
        public Bispo(Tabuleiro tab, Cor cor): base(tab, cor)
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

            //NO
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.ObterPeca(pos) != null && Tab.ObterPeca(pos).Cor != Cor)
                    break;
                pos.DefinirValores(pos.Linha - 1, pos.Coluna - 1);
            }

            //NE
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.ObterPeca(pos) != null && Tab.ObterPeca(pos).Cor != Cor)
                    break;
                pos.DefinirValores(pos.Linha - 1, pos.Coluna + 1);
            }

            //SE
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.ObterPeca(pos) != null && Tab.ObterPeca(pos).Cor != Cor)
                    break;
                pos.DefinirValores(pos.Linha + 1, pos.Coluna + 1);
            }

            //SO
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.ObterPeca(pos) != null && Tab.ObterPeca(pos).Cor != Cor)
                    break;
                pos.DefinirValores(pos.Linha + 1, pos.Coluna - 1);
            }            

            return mat;
        }

        public override string ToString()
        {
            return "B";
        }
    }
}
