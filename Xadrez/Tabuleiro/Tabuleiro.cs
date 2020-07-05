namespace Xadrez
{
    class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] Pecas { get; set; }

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas, colunas];
        }

        public Peca ObterPeca(int linha, int coluna)
        {
            return Pecas[linha, coluna];
        }

        public Peca ObterPeca(Posicao pos)
        {
            return Pecas[pos.Linha, pos.Coluna];
        }

        public bool ExistePecaNaPosicao(Posicao pos)
        {
            ValidarPosicao(pos);
            return ObterPeca(pos) != null;
        }

        public void InserirPeca(Peca p, Posicao pos)
        {
            if (ExistePecaNaPosicao(pos))
                throw new TabuleiroException("Já existe uma peça nessa posição");
            Pecas[pos.Linha, pos.Coluna] = p;
            p.Posicao = pos;
        }

        public bool PosicaoValida(Posicao pos)
        {
            if (pos.Linha < 0 || pos.Linha >= Linhas || pos.Coluna < 0 || pos.Coluna >= Colunas)
                return false;
            return true;
        }

        public void ValidarPosicao(Posicao pos)
        {
            if (!PosicaoValida(pos))
                throw new TabuleiroException("Posição inválida!");
        }
    }
}
