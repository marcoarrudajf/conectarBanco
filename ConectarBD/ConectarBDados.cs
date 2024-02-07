using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Security;

namespace ConectarBD
{
    class ConectarBDados
    {
        MySqlConnection conexao;
        string sqlQuery = "";
        public void conectarBDados()
        {
            string connectionString = "Server=localhost;Database=cadastropessoa;Uid=root;Pwd=senha;";

            conexao = new MySqlConnection(connectionString);

            try
            {
                conexao.Open();
                Console.WriteLine("Conexão bem-sucedida!");

                Console.WriteLine("O que deseja fazer?\nDigite 1: Consulta, 2: Inserir dados, 0: Encerrar");
                int num = int.Parse(Console.ReadLine());

                switch (num)
                {
                    case 0:
                        Console.WriteLine("Encerrado");
                        break;
                    case 1:
                        AcessoBD();
                        break;
                    case 2:
                        insertDados();
                        break;
                    default:
                        Console.WriteLine("Entre com um valor valido");
                        conexao.Close();
                        conectarBDados();
                        break;
                }
            }
            catch (MySqlException ex)
            {
                // Em caso de erro na conexão
                Console.WriteLine("Erro ao conectar ao banco de dados: " + ex.Message);
            }
            finally
            {
                // Fecha a conexão, independentemente do que aconteceu
                conexao.Close();
            }
        }
        public void AcessoBD()
        {
            // Executa a consulta e manipula os resultados, se necessário
            sqlQuery = "SELECT id_pessoa, nome_pessoa, sobrenome, cpf, funcao, idade, salario FROM pessoa";
            MySqlCommand comando = new MySqlCommand(sqlQuery, conexao);

            // Executa a consulta e obtém um leitor de dados
            MySqlDataReader leitor = comando.ExecuteReader();

            // Processa os resultados
            while (leitor.Read())
            {
                int id = leitor.GetInt32("id_pessoa");
                Console.WriteLine($"Cadastro: {id}");
                string nome = leitor.GetString("nome_pessoa");
                string sobreNome = leitor.GetString("sobrenome");
                string cpf = leitor.GetString("cpf");
                string desc = leitor.GetString("funcao");
                var idade = leitor.GetInt32("idade");
                var salario = leitor.GetDouble("salario");
                Console.WriteLine($"ID: {id}\nNome: {nome.ToUpper()} {sobreNome.ToUpper()}\nCPF: {cpf}\nFunção: {desc}\nIdade: {idade}\nSalário: {salario.ToString("F2")}\n");
            }

            // Fecha o leitor de dados
            leitor.Close();
        }

        public void insertDados()
        {
            try
            {
                sqlQuery = "INSERT INTO pessoa (nome_pessoa, sobrenome, cpf, funcao, idade, salario) VALUES (@nome, @sobrenome, @cpf, @funcao, @idade, @salario)";

                // Cria um novo comando para a instrução SQL e associa a conexão
                MySqlCommand comando = new MySqlCommand(sqlQuery, conexao);

                // Adiciona parâmetros para a consulta
                Console.WriteLine("Digite o nome para o cadastro:");
                comando.Parameters.AddWithValue("@nome", Console.ReadLine()); // "Exemplo"); ;
                Console.WriteLine("Digite o sobrenome para o cadastro:");
                comando.Parameters.AddWithValue("@sobrenome", Console.ReadLine()); //"exemplo@email.com");
                Console.WriteLine("Digite o CPF para o cadastro:");
                comando.Parameters.AddWithValue("@cpf", Console.ReadLine()); //"025.157.103-80");
                Console.WriteLine("Digite a função para o cadastro:");
                comando.Parameters.AddWithValue("@funcao", Console.ReadLine()); //"Feliz demais");
                Console.WriteLine("Digite a idade da pessoa  para o cadastro:");
                comando.Parameters.AddWithValue("@idade", Console.ReadLine()); //"36");
                Console.WriteLine("Digite o valor do salário para o cadastro:");
                comando.Parameters.AddWithValue("@salario", Console.ReadLine()); //"36");

                // Executa a instrução SQL de inserção
                int linhasAfetadas = comando.ExecuteNonQuery();

                // Verifica se a inserção foi bem-sucedida
                if (linhasAfetadas > 0)
                {
                    Console.WriteLine("Dados inseridos com sucesso!");
                }
                else
                {
                    Console.WriteLine("Falha ao inserir dados.");
                }

            }
            catch (MySqlException ex)
            {
                // Em caso de erro na conexão
                Console.WriteLine("Erro ao conectar ao banco de dados: " + ex.Message);
            }
            finally
            {
                // Fecha a conexão, independentemente do que aconteceu
                conexao.Close();
            }
        }
    }
}
