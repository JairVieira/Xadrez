using System;
using System.Collections.Generic;

namespace Xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool PartidaTerminou { get; private set; }
        private HashSet<Peca> _pecas;
        private HashSet<Peca> _pecasCapturadas;
        public bool Xeque { get; private set; }
        public Peca VulneravelEnPassant { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            _pecas = new HashSet<Peca>();
            _pecasCapturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            var aux = new HashSet<Peca>();
            foreach (var peca in _pecasCapturadas)
            {
                if (peca.Cor == cor)
                    aux.Add(peca);
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            var aux = new HashSet<Peca>();
            foreach (var peca in _pecas)
            {
                if (peca.Cor == cor)
                    aux.Add(peca);
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
                return Cor.Preta;
            return Cor.Branca;
        }

        private Peca Rei(Cor cor)
        {
            foreach (var peca in PecasEmJogo(cor))
            {
                if (peca is Rei)
                    return peca;
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            var rei = Rei(cor);
            if (rei is null)
                throw new TabuleiroException($"Não tem rei da cor {cor} no tabuleiro");
            foreach (var peca in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = peca.MovimentosPossiveis();
                if (mat[rei.Posicao.Linha, rei.Posicao.Coluna])
                    return true;
            }
            return false;
        }

        public bool XequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
                return false;

            foreach (var peca in PecasEmJogo(cor))
            {
                bool[,] mat = peca.MovimentosPossiveis();
                for (int i = 0; i < Tab.Linhas; i++)
                {
                    for (int j = 0; j < Tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            var origem = peca.Posicao;
                            var destino = new Posicao(i, j);
                            var pecaCapturada = ExecutarMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazerMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                                return false;
                        }

                    }
                }
            }
            return true;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.InserirPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            _pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('a', 1, new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('b', 1, new Cavalo(Tab, Cor.Branca));
            ColocarNovaPeca('c', 1, new Bispo(Tab, Cor.Branca));
            ColocarNovaPeca('d', 1, new Dama(Tab, Cor.Branca));
            ColocarNovaPeca('e', 1, new Rei(Tab, Cor.Branca, this));
            ColocarNovaPeca('f', 1, new Bispo(Tab, Cor.Branca));
            ColocarNovaPeca('g', 1, new Cavalo(Tab, Cor.Branca));
            ColocarNovaPeca('h', 1, new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('a', 2, new Peao(Tab, Cor.Branca, this));
            ColocarNovaPeca('b', 2, new Peao(Tab, Cor.Branca, this));
            ColocarNovaPeca('c', 2, new Peao(Tab, Cor.Branca, this));
            ColocarNovaPeca('d', 2, new Peao(Tab, Cor.Branca, this));
            ColocarNovaPeca('e', 2, new Peao(Tab, Cor.Branca, this));
            ColocarNovaPeca('f', 2, new Peao(Tab, Cor.Branca, this));
            ColocarNovaPeca('g', 2, new Peao(Tab, Cor.Branca, this));
            ColocarNovaPeca('h', 2, new Peao(Tab, Cor.Branca, this));

            ColocarNovaPeca('a', 8, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('b', 8, new Cavalo(Tab, Cor.Preta));
            ColocarNovaPeca('c', 8, new Bispo(Tab, Cor.Preta));
            ColocarNovaPeca('d', 8, new Dama(Tab, Cor.Preta));
            ColocarNovaPeca('e', 8, new Rei(Tab, Cor.Preta, this));
            ColocarNovaPeca('f', 8, new Bispo(Tab, Cor.Preta));
            ColocarNovaPeca('g', 8, new Cavalo(Tab, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('a', 7, new Peao(Tab, Cor.Preta, this));
            ColocarNovaPeca('b', 7, new Peao(Tab, Cor.Preta, this));
            ColocarNovaPeca('c', 7, new Peao(Tab, Cor.Preta, this));
            ColocarNovaPeca('d', 7, new Peao(Tab, Cor.Preta, this));
            ColocarNovaPeca('e', 7, new Peao(Tab, Cor.Preta, this));
            ColocarNovaPeca('f', 7, new Peao(Tab, Cor.Preta, this));
            ColocarNovaPeca('g', 7, new Peao(Tab, Cor.Preta, this));
            ColocarNovaPeca('h', 7, new Peao(Tab, Cor.Preta, this));
        }

        public Peca ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RemoverPeca(origem);
            p.IncrementarQtdMovimentos();
            var pecaCapturada = Tab.RemoverPeca(destino);
            Tab.InserirPeca(p, destino);
            if (pecaCapturada != null)
                _pecasCapturadas.Add(pecaCapturada);

            //#jogada especial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                var origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                var destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                var torre = Tab.RemoverPeca(origemTorre);
                torre.IncrementarQtdMovimentos();
                Tab.InserirPeca(torre, destinoTorre);
            }

            //#jogada especial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                var origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                var destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                var torre = Tab.RemoverPeca(origemTorre);
                torre.IncrementarQtdMovimentos();
                Tab.InserirPeca(torre, destinoTorre);
            }

            //#jogada especial en passant
            if(p is Peao)
            {
                if(origem.Coluna != destino.Coluna && pecaCapturada is null)
                {
                    Posicao posicaoPeao;
                    if (p.Cor == Cor.Branca)
                        posicaoPeao = new Posicao(destino.Linha + 1, destino.Coluna);
                    else
                        posicaoPeao = new Posicao(destino.Linha - 1, destino.Coluna);
                    pecaCapturada = Tab.RemoverPeca(posicaoPeao);
                    _pecasCapturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        public void RealizarJogada(Posicao origem, Posicao destino)
        {
            var pecaCapturada = ExecutarMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazerMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não se pode colocar em xeque");
            }

            if (EstaEmXeque(Adversaria(JogadorAtual)))
                Xeque = true;
            else
                Xeque = false;

            if (XequeMate(Adversaria(JogadorAtual)))
            {
                PartidaTerminou = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }

            var peca = Tab.ObterPeca(destino);

            //#jogadaespecial en passant
            if (peca is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
                VulneravelEnPassant = peca;
            else
                VulneravelEnPassant = null;
        }

        private void DesfazerMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            var peca = Tab.RemoverPeca(destino);
            peca.DecrementarQtdMovimentos();
            if (pecaCapturada != null)
            {
                Tab.InserirPeca(pecaCapturada, destino);
                _pecasCapturadas.Remove(pecaCapturada);
            }
            Tab.InserirPeca(peca, origem);

            //#jogada especial roque pequeno
            if (peca is Rei && destino.Coluna == origem.Coluna + 2)
            {
                var origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                var destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                var torre = Tab.RemoverPeca(destinoTorre);
                torre.DecrementarQtdMovimentos();
                Tab.InserirPeca(torre, origemTorre);
            }

            //#jogada especial roque grande
            if (peca is Rei && destino.Coluna == origem.Coluna - 2)
            {
                var origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                var destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                var torre = Tab.RemoverPeca(destinoTorre);
                torre.IncrementarQtdMovimentos();
                Tab.InserirPeca(torre, origemTorre);
            }

            //#jogada especial en passant
            if (peca is Peao)
            {
                if(origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
                {
                    var peao = Tab.RemoverPeca(destino);
                    Posicao posicaoPeao;
                    if (peca.Cor == Cor.Branca)
                        posicaoPeao = new Posicao(3, destino.Coluna);
                    else
                        posicaoPeao = new Posicao(4, destino.Coluna);

                    Tab.InserirPeca(peao, posicaoPeao);
                }
            }
        }

        public void ValidarPosicaoOrigem(Posicao pos)
        {
            if (Tab.ObterPeca(pos) is null)
                throw new TabuleiroException("Não existe peça nessa posição de origem escolhida");
            if (JogadorAtual != Tab.ObterPeca(pos).Cor)
                throw new TabuleiroException("A peça de origem escolhida não é a sua");
            if (!Tab.ObterPeca(pos).ExisteMovimentosPossiveis())
                throw new TabuleiroException("Não há movimetos possíveis a peça de origem escolhida");
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.ObterPeca(origem).MovimentoPossivel(destino))
                throw new TabuleiroException("Posição de destino inválida");
        }

        private void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
                JogadorAtual = Cor.Preta;
            else
                JogadorAtual = Cor.Branca;
        }
    }
}
