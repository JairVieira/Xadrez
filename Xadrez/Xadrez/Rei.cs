namespace Xadrez
{
    class Rei : Peca
    {
        private PartidaDeXadrez _partida;
        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
            _partida = partida;
        }

        private bool PodeMover(Posicao pos)
        {
            Peca p = Tab.ObterPeca(pos);
            return p is null || p.Cor != Cor;
        }

        private bool TesteTorreParaRoque(Posicao pos)
        {
            var peca = Tab.ObterPeca(pos);
            return peca != null && peca is Torre && peca.Cor == Cor && peca.QtdMovimentos == 0;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            var pos = new Posicao(0, 0);

            //acima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
                mat[pos.Linha, pos.Coluna] = true;

            // ne
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
                mat[pos.Linha, pos.Coluna] = true;

            // direita
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
                mat[pos.Linha, pos.Coluna] = true;

            // se
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
                mat[pos.Linha, pos.Coluna] = true;

            // abaixo
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
                mat[pos.Linha, pos.Coluna] = true;

            // so
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
                mat[pos.Linha, pos.Coluna] = true;

            // esquerda
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
                mat[pos.Linha, pos.Coluna] = true;

            //no
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
                mat[pos.Linha, pos.Coluna] = true;

            // #jogadaespecial roque
            if (QtdMovimentos == 0 && !_partida.Xeque)
            {
                // #jogadaespecial roque pequeno
                var posicaoTorre1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
                if (TesteTorreParaRoque(posicaoTorre1))
                {
                    var p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    var p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);
                    if (Tab.ObterPeca(p1) is null && Tab.ObterPeca(p2) is null)
                        mat[Posicao.Linha, Posicao.Coluna + 2] = true;
                }
                // #jogadaespecial roque grande
                var posicaoTorre2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
                if (TesteTorreParaRoque(posicaoTorre2))
                {
                    var p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    var p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    var p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);
                    if (Tab.ObterPeca(p1) is null && Tab.ObterPeca(p2) is null && Tab.ObterPeca(p3) is null)
                        mat[Posicao.Linha, Posicao.Coluna - 2] = true;
                }
            }

            return mat;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
