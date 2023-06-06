using Pim_6.BancodeDados;

namespace Pim_6.Models
{
    internal class ClienteModel
    {
        #region Propriedades 

        public int codigo               { get; set; }
        public string rg                { get; set; }
        public string cpf               { get; set; }
        public string nome              { get; set; }
        public DateTime datacadastro    { get; set; } 
        public string endereco          { get; set; }
        public string telefone          { get; set; }
        public string email             { get; set; }

        #endregion

        #region Funcoes Publicas

        public void Inserir()
        { 
            string sql = $"CALL Inserirclientes('{rg}', '{cpf}', '{nome}', '{endereco}', '{telefone}', '{email}');";
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

        public List<ClienteModel> Listar()
        {
            List<ClienteModel> lstClientes = new();

            string sql = "CALL Listarclientes();";
            var dataSet = ConectaBanco.ExecutarComando(sql);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                var dados = dataSet.CreateDataReader();

                while(dados.Read())
                {
                    ClienteModel cliente = new()
                    {
                        codigo       = Convert.ToInt32(dados["codigo"]),
                        rg           = dados["rg"].ToString(),
                        cpf          = dados["cpf"].ToString(),
                        nome         = dados["nome"].ToString(),
                        datacadastro = DateTime.Parse(dados["data_cadastro"].ToString()),
                        email        = dados["email"].ToString()
                    };

                    lstClientes.Add(cliente);
                }
            }

            return lstClientes;
        }

        #endregion

    }
}
