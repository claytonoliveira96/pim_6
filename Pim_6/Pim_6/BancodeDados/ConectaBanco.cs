using System.Data;

using MySql.Data.MySqlClient;

namespace Pim_6.BancodeDados
{
    public static class ConectaBanco
    {
        private static MySqlConnection _conexao;

        public static void Conectar()
        {
            try
            {
                _conexao = new MySqlConnection();
                _conexao.ConnectionString = "server=localhost;user=root;database=pim_6;password=;";
                _conexao.Open();
            }
            catch
            {
                throw;
            }
        }

        public static DataSet ExecutarComando(string sql)
        {
            try
            {
                Conectar();

                DataSet dataSet = new();
                MySqlDataAdapter dataAdapter = new()
                {
                    SelectCommand = new(sql, _conexao)
                };
                dataAdapter.SelectCommand.CommandTimeout = 0;
                dataAdapter.Fill(dataSet);

                return dataSet;
            }
            catch
            {
                throw;
            }
            finally
            {
                Desconectar();
            }
        }

        public static void Desconectar()
        {
            try
            {
                if (_conexao.State == ConnectionState.Connecting) 
                {
                    _conexao.Close();
                }
            }
            catch
            {
                throw;
            }
        }


    }
}
