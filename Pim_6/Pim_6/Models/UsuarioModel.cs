using Pim_6.BancodeDados;

namespace Pim_6.Models
{
    public class UsuarioModel
    {
        #region Propriedades

        public int codigo               { get; set; } 
        public string nome              { get; set; }   
        public DateTime datacadastro    { get; set; }
        public string login             { get; set; }
        public string senha             { get; set; }
        public DateTime dataatualizacao { get; set; }
        public int tipoacesso           { get; set; }

        #endregion

        #region Funções Públicas

        public void Inserir()
        {
            string sql = $"CALL Inserirusuarios('{nome}', '{login}', '{senha}')";
            var dataSet = ConectaBanco.ExecutarComando(sql);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                var dados = dataSet.CreateDataReader();
                while (dados.Read())
                {
                    codigo = Convert.ToInt32(dados["codigo"]);
                }
            }
        }

        public void  validalogin()
        {
            string sql = $"CALL Validalogin('{login}', '{senha}')";
            var dataSet = ConectaBanco.ExecutarComando(sql);
            if (dataSet.Tables[0].Rows.Count > 0)

            {
                var dados = dataSet.CreateDataReader();
                while (dados.Read())
                {
                    codigo          = Convert.ToInt32(dados["codigo"]);
                    nome            = dados["nome"].ToString();
                    datacadastro    = DateTime.Parse(dados["data_cadastro"].ToString());
                    login           = dados["login"].ToString();
                    senha           = dados["senha"].ToString();
                    tipoacesso      = Convert.ToInt32(dados["tipo_acesso"]);

                    break;
                }
            }
        }

        #endregion
    }
}
