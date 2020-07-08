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

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.InserirPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            _pecas.Add(peca);
        }

        private void ColocarPecas()
        {         
           ColocarNovaPeca('c', 1, new Torre(Tab, Cor.Branca));
           ColocarNovaPeca('c', 2, new Torre(Tab, Cor.Branca));
           ColocarNovaPeca('d', 2, new Torre(Tab, Cor.Branca));
           ColocarNovaPeca('e', 2, new Torre(Tab, Cor.Branca));
           ColocarNovaPeca('e', 1, new Torre(Tab, Cor.Branca));
           ColocarNovaPeca('d', 1, new Rei(Tab, Cor.Branca));

            ColocarNovaPeca('c', 7, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('c', 8, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('d', 7, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('e', 7, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('e', 8, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('d', 8, new Rei(Tab, Cor.Preta));
        }

        public Peca ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RemoverPeca(origem);
            p.IncrementarQtdMovimentos();
            var pecaCapturada = Tab.RemoverPeca(destino);
            Tab.InserirPeca(p, destino);
            if (pecaCapturada != null)
                _pecasCapturadas.Add(pecaCapturada);

            return pecaCapturada;
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
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

            Turno++;
            MudaJogador();
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
        }

        public  void ValidarPosicaoOrigem(Posicao pos)
        {
            if (Tab.ObterPeca(pos) is null)
                throw new TabuleiroException("Não existe peça nessa posição de origem escolhida");
            if (JogadorAtual != Tab.ObterPeca(pos).Cor)
                throw new TabuleiroException("A peça de origem escolhida não é a sua");
            if(!Tab.ObterPeca(pos).ExisteMovimentosPossiveis())
                throw new TabuleiroException("Não há movimetos possíveis a peça de origem escolhida");
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.ObterPeca(origem).PodeMoverPara(destino))
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
