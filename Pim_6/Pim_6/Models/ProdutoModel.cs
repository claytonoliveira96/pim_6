using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pim_6.BancodeDados;

namespace Pim_6.Models
{
    internal class ProdutoModel
    {
        #region
        public int codigo { get; set; }
        public string codigodebarras { get; set; }
        public string nome { get; set; }
        public string categoria { get; set; }
        public string fabricante { get; set; }
        public int quantidade  { get; set; }
        public decimal valorproduto  { get; set; }
        public string plataforma { get; set; }
        public int prazogarantia { get; set; }
        public DateTime datacadastro { get; set; }
        public DateTime dataatualizacao { get; set; }

        #endregion

        #region
        
        public void Inserir()

        {
            string sql = $"CALL Inserirproduto('{codigodebarras}', '{nome}', '{categoria}', '{fabricante}', {quantidade}, {valorproduto.ToString().Replace(',', '.')}, '{plataforma}', {prazogarantia});";
            var dataSet = ConectaBanco.ExecutarComando(sql);

            if (dataSet.Tables[0].Rows.Count> 0)
            {
                var dados = dataSet.CreateDataReader();

                while (dados.Read())
                {
                    codigo = Convert.ToInt32(dados["codigo"]);
                }
            }
        }

        public List<ProdutoModel> Listar()
        {
            List<ProdutoModel> lstProdutos = new();

            string sql = "CALL Listarprodutos();";
            var dataSet = ConectaBanco.ExecutarComando(sql);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                var dados = dataSet.CreateDataReader();

                while (dados.Read())
                {
                    ProdutoModel produto =
                    new()
                    {
                        codigo        = Convert.ToInt32(dados["codigo"]),
                        nome          = dados["nome"].ToString(),
                        categoria     = dados["categoria"].ToString(),
                        fabricante    = dados["fabricante"].ToString(),
                        quantidade    = Convert.ToInt32(dados["quantidade"]),
                        valorproduto  = Convert.ToDecimal(dados["valor_produto"]),
                        plataforma    = dados["plataforma"].ToString(),
                        prazogarantia = Convert.ToInt32(dados["prazo_garantia"]),
                        datacadastro  = DateTime.Parse(dados["data_cadastro"].ToString())
                            
                    };

                    if (!string.IsNullOrEmpty(dados["data_atualizacao"].ToString()))
                    {
                        produto.dataatualizacao = DateTime.Parse(dados["data_atualizacao"].ToString());
                    }

                    lstProdutos.Add(produto);
                }
            }

            return lstProdutos;
        }

    }
    #endregion
}
