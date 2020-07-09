namespace Xadrez
{
    class Peao : Peca
    {
        private PartidaDeXadrez _partida;
        public Peao(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
            _partida = partida;

        }

        private bool ExisteInimigo(Posicao pos)
        {
            var peca = Tab.ObterPeca(pos);
            return peca != null && peca.Cor != Cor;
        }

        private bool Livre(Posicao pos)
        {
            return Tab.ObterPeca(pos) is null;
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

            if (Cor == Cor.Branca)
            {
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (Tab.PosicaoValida(pos) && Livre(pos))
                    mat[pos.Linha, pos.Coluna] = true;

                pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                if (Tab.PosicaoValida(pos) && Livre(pos) && QtdMovimentos == 0)
                    mat[pos.Linha, pos.Coluna] = true;

                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                    mat[pos.Linha, pos.Coluna] = true;

                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                    mat[pos.Linha, pos.Coluna] = true;

                // #jogadaespecial en passant
                if (Posicao.Linha == 3)
                {
                    var posicaoEsquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tab.PosicaoValida(posicaoEsquerda) && ExisteInimigo(posicaoEsquerda) && Tab.ObterPeca(posicaoEsquerda) == _partida.VulneravelEnPassant)
                        mat[posicaoEsquerda.Linha - 1, posicaoEsquerda.Coluna] = true;
                    var posicaoDireita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tab.PosicaoValida(posicaoDireita) && ExisteInimigo(posicaoDireita) && Tab.ObterPeca(posicaoDireita) == _partida.VulneravelEnPassant)
                        mat[posicaoDireita.Linha - 1, posicaoDireita.Coluna] = true;
                }
            }
            else
            {
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tab.PosicaoValida(pos) && Livre(pos))
                    mat[pos.Linha, pos.Coluna] = true;

                pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                if (Tab.PosicaoValida(pos) && Livre(pos) && QtdMovimentos == 0)
                    mat[pos.Linha, pos.Coluna] = true;

                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                    mat[pos.Linha, pos.Coluna] = true;

                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                    mat[pos.Linha, pos.Coluna] = true;

                // #jogadaespecial en passant
                if (Posicao.Linha == 4)
                {
                    var posicaoEsquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tab.PosicaoValida(posicaoEsquerda) && ExisteInimigo(posicaoEsquerda) && Tab.ObterPeca(posicaoEsquerda) == _partida.VulneravelEnPassant)
                        mat[posicaoEsquerda.Linha + 1, posicaoEsquerda.Coluna] = true;
                    var posicaoDireita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tab.PosicaoValida(posicaoDireita) && ExisteInimigo(posicaoDireita) && Tab.ObterPeca(posicaoDireita) == _partida.VulneravelEnPassant)
                        mat[posicaoDireita.Linha + 1, posicaoDireita.Coluna] = true;
                }
            }

            return mat;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
