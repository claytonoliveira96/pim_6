using Pim_6.Models;

namespace Pim_6
{
    public class Sistema
    {
        #region "Propriedades"

        private Dictionary<int, string> _dicionarioMenuGeral;
        private UsuarioModel _usuarioLogado;

        #endregion

        #region "Construtor"

        public Sistema()
        {
            _dicionarioMenuGeral = new()
            {
                {1, "Cadastrar Produto"},
                {2, "Cadastrar Cliente"},
                {3, "Listar Produtos"},
                {4, "Listar Clientes"}
            };

            _usuarioLogado = new();
        }

        #endregion

        #region "Funções públicas"

        public bool Autenticar()
        {
            try
            {
                while (true)
                {
                    Console.Write("Login: ");
                    _usuarioLogado.login = Console.ReadLine();
                    Console.WriteLine();
                    Console.Write("Senha: ");
                    _usuarioLogado.senha = Console.ReadLine();

                    _usuarioLogado.validalogin();

                    if(_usuarioLogado.codigo > 0)
                    {                        
                        Console.Clear();
                        Console.WriteLine($"Bem vindo {_usuarioLogado.nome}");                        
                        return false;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return true;
        }

        public bool InserirUsuario()
        {
            try
            {
                Console.Write("Informe o nome: ");
                _usuarioLogado.nome = Console.ReadLine() ?? "";

                while (true)
                {
                    Console.Write("Informe o login: ");
                    _usuarioLogado.login = Console.ReadLine() ?? "";

                    if(_usuarioLogado.login.Contains("@"))
                    {
                        break;
                    }

                    Console.WriteLine("Login com formato inválido! Tente novamente");
                }

                Console.Write("Informe o senha: ");
                _usuarioLogado.senha = Console.ReadLine() ?? "";

                _usuarioLogado.Inserir();

                if(_usuarioLogado.codigo > 0)
                {
                    Console.Clear();
                    Console.WriteLine($"Bem vindo {_usuarioLogado.nome}");
                    return false;
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return true;
        }

        public void Iniciar()
        {
            int opcao = 0;
            bool prossegue = true;

            while (prossegue)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Precione 1 para se cadastrar ou 2 para efetuar o login. ");
                    opcao = Convert.ToInt32(Console.ReadLine());

                    switch (opcao)
                    {
                        case 1:
                            {
                                prossegue = InserirUsuario();
                            }
                            break;
                        case 2:
                            {
                                prossegue = Autenticar();
                            }
                            break;
                        default:
                            Console.WriteLine("Opção inválida, tente novamente!");
                            break;
                    }
                }
                catch
                {
                    continue;
                }
            }

            while(true)
            {
                try
                {
                    Console.WriteLine();
                    Console.WriteLine("Menu!");

                    foreach (var itemMenu in _dicionarioMenuGeral.ToList())
                    {
                        Console.WriteLine($"{itemMenu.Key} = {itemMenu.Value}");
                    }

                    Console.WriteLine();
                    int opcaoMenu = 0;

                    while (true)
                    {
                        Console.WriteLine("Informe a ação que deseja executar!");

                        try
                        {
                            opcaoMenu = Convert.ToInt32(Console.ReadLine());

                            if(_dicionarioMenuGeral.Where(p => p.Key.Equals(opcaoMenu)).ToList().Count > 0)
                            {
                                break;
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    bool acaoMenuLogado = true;

                    while (acaoMenuLogado)
                    {
                        switch (opcaoMenu)
                        {
                            case 1:
                                {
                                    acaoMenuLogado = InserirProduto();
                                }
                                break;
                            case 2:
                                {
                                    acaoMenuLogado = InserirCliente();
                                }
                                break;
                            case 3:
                                {
                                    acaoMenuLogado = ListarProdutos();
                                }
                                break;
                            case 4:
                                {
                                    acaoMenuLogado = ListarClientes();
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    int continua = 0;
                    Console.WriteLine();

                    while(true)
                    {
                        Console.WriteLine("Deseja continuar ? 1 -> Sim 2 -> Não: ");
                        continua = Convert.ToInt32(Console.ReadLine());

                        if(continua != 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Encerrando o sistema!");
                            Thread.Sleep(5000);
                            prossegue = false;
                            Environment.Exit(1);

                            break;
                        }
                        else 
                        {
                            prossegue = true;
                            break;
                        }
                    }                 

                }
                catch
                {
                    continue;
                }
            }
        }

        public bool ListarProdutos()
        {
            try
            {
                ProdutoModel produto = new();
                List<ProdutoModel> lstProdutos = new();

                lstProdutos = produto.Listar();

                if(lstProdutos.Count > 0)
                {
                    Console.WriteLine("Produtos cadastrados.");
                    Console.WriteLine();

                    foreach (ProdutoModel prod in lstProdutos)
                    {
                        Console.WriteLine($"CODIGO: {prod.codigo}\nCODIGO DE BARRAS: {prod.codigodebarras}\nNOME: {prod.nome}\nCATEGORIA: {prod.categoria}\nFABRICANTE: {prod.fabricante}\nQUANTIDADE EM ESTOQUE: {prod.quantidade}\nVALOR: R$ {prod.valorproduto}\nPLATAFORMA: {prod.plataforma}\nPRAZO DE GARANTIDA: {prod.prazogarantia} DIAS\nDATA DE CADASTRO: {prod.datacadastro.ToString("dd/MM/yyyy")}\nDATA DE ATUALIZAÇÃO: {prod.dataatualizacao.ToString("dd/MM/yyyy")}");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Nenhum produto cadastrado!");
                }

                return false;

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return true;
        }

        public bool ListarClientes()
        {
            try
            {
                ClienteModel cliente = new();
                List<ClienteModel> lstClientes = new();

                lstClientes = cliente.Listar();

                if (lstClientes.Count > 0)
                {
                    Console.WriteLine("Clientes cadastrados.");
                    Console.WriteLine();

                    foreach (ClienteModel cli in lstClientes)
                    {
                        Console.WriteLine($"CODIGO: {cli.codigo}\nNOME: {cli.nome}\nRG: {cli.rg}\nCPF: {cli.cpf}\nDATA DE CADASTRO: {cli.datacadastro.ToString("dd/MM/yyyy")}\nENDEREÇO: {cli.endereco}\nTELEFONE: {cli.telefone}\nE-MAIL: {cli.email}");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Nenhum cliente cadastrado!");
                }

                return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return true;
        }

        public bool InserirProduto()
        {
            try
            {
                ProdutoModel produto = new();

                Console.Write("Informe o nome do produto: ");
                produto.nome = Console.ReadLine();

                Console.Write("Informe a categoria do produto: ");
                produto.categoria = Console.ReadLine();

                Console.Write("Informe o fabricante do produto: ");
                produto.fabricante = Console.ReadLine();

                while (true)
                {
                    try
                    {
                        Console.Write("Informe a quantidade do produto em estoque: ");
                        produto.quantidade = Convert.ToInt32(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Quantidade inválida, favor informar um número inteiro!");
                    }
                }

                while (true)
                {
                    try
                    {
                        Console.Write("Informe o valor do produto: ");
                        produto.valorproduto = Convert.ToDecimal(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Valor inválido, favor informar um valor correto!");
                    }
                }

                Console.Write("Informe a plataforma em que o produto será disponibilizado: ");
                produto.plataforma = Console.ReadLine();

                while (true)
                {
                    try
                    {
                        Console.Write("Informe a quantidade de dias para a garantia do produto: ");
                        produto.prazogarantia = Convert.ToInt32(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Dias de garantia inválido, favor informar novamente!");
                    }
                }

                Console.Write("Informe ocódigo de barras do produto: ");
                produto.codigodebarras = Console.ReadLine();

                produto.Inserir();

                if(produto.codigo > 0)
                {
                    Console.WriteLine("Produto cadastrado com sucesso!");
                    return false;
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return true;
        }

        public bool InserirCliente()
        {
            try
            {
                ClienteModel cliente = new();

                Console.WriteLine("Informe o nome do Cliente: ");
                cliente.nome = Console.ReadLine();

                Console.WriteLine("Informe o RG do Cliente sem pontos ou traços: ");
                cliente.rg = Console.ReadLine();

                Console.WriteLine("Informe o CPF do Cliente sem pontos ou traços: ");
                cliente.cpf = Console.ReadLine();

                Console.WriteLine("Informe o endereço do Cliente: ");
                cliente.endereco = Console.ReadLine();

                Console.WriteLine("Informe o telefone do Cliente sem pontos ou traços: ");
                cliente.telefone = Console.ReadLine();

                while (true)
                {
                    Console.WriteLine("Informe o email do Cliente: ");
                    cliente.email = Console.ReadLine();

                    if(cliente.email.Contains("@"))
                    {
                        break;
                    }

                    Console.Write("E-mail inválido, favor preencher novamente!");
                }

                cliente.Inserir();

                if(cliente.codigo > 0)
                {
                    Console.WriteLine("Cliente cadastrado com sucesso!");
                    return false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return true;
        }

        #endregion
    }
}
