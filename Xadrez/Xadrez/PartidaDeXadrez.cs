using System;

namespace Xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        private int _turno;
        private Cor _jogadorAtual;
        public bool PartidaTerminou { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            _turno = 1;
            _jogadorAtual = Cor.Branca;
            ColocarPecas();
        }

        private void ColocarPecas()
        {
            Tab.InserirPeca(new Torre(Tab, Cor.Branca), new PosicaoXadrez('c', 1).ToPosicao());
            Tab.InserirPeca(new Torre(Tab, Cor.Branca), new PosicaoXadrez('c', 2).ToPosicao());
            Tab.InserirPeca(new Torre(Tab, Cor.Branca), new PosicaoXadrez('d', 2).ToPosicao());
            Tab.InserirPeca(new Torre(Tab, Cor.Branca), new PosicaoXadrez('e', 2).ToPosicao());
            Tab.InserirPeca(new Torre(Tab, Cor.Branca), new PosicaoXadrez('e', 1).ToPosicao());
            Tab.InserirPeca(new Rei(Tab, Cor.Branca), new PosicaoXadrez('d', 1).ToPosicao());

            Tab.InserirPeca(new Torre(Tab, Cor.Preta), new PosicaoXadrez('c', 7).ToPosicao());
            Tab.InserirPeca(new Torre(Tab, Cor.Preta), new PosicaoXadrez('c', 8).ToPosicao());
            Tab.InserirPeca(new Torre(Tab, Cor.Preta), new PosicaoXadrez('d', 7).ToPosicao());
            Tab.InserirPeca(new Torre(Tab, Cor.Preta), new PosicaoXadrez('e', 7).ToPosicao());
            Tab.InserirPeca(new Torre(Tab, Cor.Preta), new PosicaoXadrez('e', 8).ToPosicao());
            Tab.InserirPeca(new Rei(Tab, Cor.Preta), new PosicaoXadrez('d', 8).ToPosicao());
        }

        public void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RemoverPeca(origem);
            p.IncrementarQtdMovimentos();
            var pecaCapturada = Tab.RemoverPeca(destino);
            Tab.InserirPeca(p, destino);
        }
    }
}
