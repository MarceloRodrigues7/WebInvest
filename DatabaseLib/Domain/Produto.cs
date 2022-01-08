using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Domain
{
    public class Produto
    {
        public long Id { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }
        public decimal ValorAtual { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public long TipoId { get; set; }

        public List<HistoricoPreco> HistoricoPrecos { get; set; }
        public TipoProduto Tipo { get; set; }
        public List<InvestimentoUsuario> InvestimentosUsuario { get; set; }

        public static decimal VariacaoValor(Produto produto)
        {
            var rand = new Random();
            var sorteio = rand.Next(produto.Tipo.VariacaoMenos, produto.Tipo.VariacaoMais);
            decimal variacao = GeraValor(rand);
            if (sorteio < 0)
            {
                return produto.ValorAtual - variacao;
            }
            else if (sorteio > 0)
            {
                return produto.ValorAtual + variacao;
            }
            return produto.ValorAtual;
        }

        public static Produto AtualizaObj(Produto produto, HistoricoPreco historico)
        {
            produto.ValorAtual = historico.Valor;
            produto.DataAtualizacao = historico.DataHora;
            return produto;
        }

        private static decimal GeraValor(Random rand) =>
            decimal.Parse($"{rand.Next(0, 1)},{rand.Next(0, 9)}{rand.Next(1, 9)}", new CultureInfo("pt-BR"));

    }
}
