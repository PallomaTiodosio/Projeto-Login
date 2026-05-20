using System.ComponentModel.DataAnnotations;
using System.Data;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;
using Projeto_Login.Models;
using Projeto_Login.Models.Constant;
using Projeto_Login.Repository.Contract;
using X.PagedList;

namespace Projeto_Login.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string _conexaoMySQL;
        IConfiguration _conf;

        public ClienteRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
            _conf = conf;
        }

        public void Atualizar(Cliente cliente)
        {
            string Situacao = SituacaoConstant.Ativo;
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("update Cliente set Nome=@Nome, Nascimento=@Nascimento, Sexo=@Sexo, CPF=@CPF," +
                    "Telefone=@Telefone, Email=@Email, Senha=@Senha, Situacao=@Situacao WHERE Id=@Id", conexao);

                cmd.Parameters.Add("@ID", MySqlDbType.VarChar).Value = cliente.ID;
                cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = cliente.Nome;
                cmd.Parameters.Add("@Nascimento", MySqlDbType.DateTime).Value = cliente.Nascimento.ToString("yyyy/MM/dd");
                cmd.Parameters.Add("@Sexo", MySqlDbType.VarChar).Value = cliente.Sexo;
                cmd.Parameters.Add("@CPF", MySqlDbType.VarChar).Value = cliente.CPF;
                cmd.Parameters.Add("@Telefone", MySqlDbType.VarChar).Value = cliente.Telefone;
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = cliente.Email;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = cliente.Senha;
                cmd.Parameters.Add("@Situacao", MySqlDbType.VarChar).Value = Situacao;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
        

        public void Cadastrar(Cliente cliente)
        {
            string Situacao = SituacaoConstant.Ativo;

            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into Cliente(Nome,Nacimento, Sexo, CPF, Telefone, Email, Senha, Situacao)" +
                     " values (@Name, @Nascimento, @Sexo, @CPF, @Telefone, @Email, @Senha, @Situacao)", conexao); //@: PARAMETRO

                cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = cliente.Nome;
                cmd.Parameters.Add("@Nascimento", MySqlDbType.DateTime).Value = cliente.Nascimento.ToString("yyyy/MM/dd");
                cmd.Parameters.Add("@Sexo", MySqlDbType.VarChar).Value = cliente.Sexo;
                cmd.Parameters.Add("@CPF", MySqlDbType.VarChar).Value = cliente.CPF;
                cmd.Parameters.Add("@Telefone", MySqlDbType.VarChar).Value = cliente.Telefone;
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = cliente.Email;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = cliente.Senha;
                cmd.Parameters.Add("@Situacao", MySqlDbType.VarChar).Value = Situacao;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from Cliente WHERE ID=@ID", conexao);
                cmd.Parameters.AddWithValue("@ID", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();

            }
        }

        public Cliente Login(string Email, string Senha)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from cliente where Email = @Email and Senha = @Senha", conexao);
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = Email;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = Senha;

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Cliente cliente = new Cliente();
                dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    cliente.ID = Convert.ToInt32(dr["ID"]);
                    cliente.Nome = Convert.ToString(dr["Nome"]);
                    cliente.Nascimento = Convert.ToDateTime(dr["Nascimento"]);

                    cliente.Sexo = Convert.ToString(dr["Sexo"]);
                    cliente.CPF = Convert.ToString(dr["CPF"]);
                    cliente.Telefone = Convert.ToString(dr["Telefone"]);
                    cliente.Situacao = Convert.ToString(dr["Situacao"]);

                    cliente.Email = Convert.ToString(dr["Email"]);
                    cliente.Senha = Convert.ToString(dr["Senha"]);

                 }
                return cliente;
                    
            }
        }

        public Cliente ObterCliente(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL.MySql))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Cliente WHERE  id=@id", conexao);
                cmd.Paramaters.AddWithValue("@Id", Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Cliente cliente = new Cliente();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    cliente.ID = (Int32)(dr["Id"]);
                    cliente.Nome = (string)(dr["Nome"]);
                    cliente.Nascimento = (DateTime)(dr["Nascimento"]);
                    cliente.Sexo = (string)(dr["Sexo"]);
                    cliente.CPF = (string)(dr["CPF"]);
                    cliente.Telefone = (string)(dr["Telefone"]);
                    cliente.Email = (string)(dr["Email"]);
                    cliente.Senha = (string)(dr["Senha"]);
                    cliente.Situacao = (string)(dr["Situacao"]);




                }
                return cliente;

            }
        }

        public IEnumerable<Cliente> ObterTodosClientes()
        {
            List<Cliente> cList = new List<Cliente> ();
            using (var conexao = new MySqlConnection )
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from cliente where Email = @Email and Senha = @Senha", conexao);

                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = Email;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = Senha;

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Cliente cliente = new Cliente();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    cliente.Id = Convert.ToInt32(dr["id"]);
                    cliente.Nome = Convert.ToString(dr["Nome"]);
                    cliente.Nascimento = Convert.ToDateTime(dr["Nascimento"]);

                    cliente.Sexo = Convert.ToString(dr["Sexo"]);
                    cliente.CPF = Convert.ToString(dr["CPF"]);
                    cliente.Telefone = Convert.ToString(dr["Telefone"]);
                    cliente.Email = Convert.ToString(dr["Email"]);
                    cliente.Senha = Convert.ToString(dr["Senha"]);
                    cliente.Situacao = Convert.ToString(dr["Situacao"]);

                }




            }
        }

        public IPagedList<Cliente> ObterTodosClientes(int? pagina, string pesquisa)
        {
            List<Cliente> cliList = new List<Cliente>();
            using (var conexao = new MySqlCommand(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM CLIENTE", conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                conexao.Clone();

                foreach (DataRow dr in dt.Rows)
                {
                    cliList.Add
                     (
                        new Cliente
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            Nome = (string)(dr["Nome"]),
                            Nascimento = Convert.ToDateTime(dr["Nascimento"]),
                            Sexo = Convert.ToString(dr["Sexo"]),
                            CPF = Convert.ToString(dr["CPF"]),
                            Telefone = Convert.ToString(dr["Telefone"]),
                            Email = Convert.ToString(dr["Email"]),
                            Senha = Convert.ToString(dr["Senha"]),
                            Situacao = Convert.ToString(dr["Situacao"]),
                        }
                        
                     );

                }
                return cliList;
            }
        }
    }

}
