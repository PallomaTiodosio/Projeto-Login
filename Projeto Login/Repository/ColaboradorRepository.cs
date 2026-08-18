using System.Data;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Projeto_Login.Models;
using Projeto_Login.Models.Constant;
using Projeto_Login.Repository.Contract;
using X.PagedList.Extensions;
using X.PagedList;

namespace Projeto_Login.Repository
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        private readonly string _conexaoMySQL;
        private IConfiguration _conf;
        IConfiguration _config;
        public ColaboradorRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
            _config = conf;
            _conf = conf;
        }
        public void Atualizar(Colaborador colaborador)
        {
            string TIpo = ColaboradorTipoConstant.Comum;
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("update Colaborador set Nome=@Nome, CPF=@CPF," + " Telefone=@Telefone, Email=@Email, Senha=@Senha, Tipo=@Tipo where Id=@Id", conexao);

                cmd.Parameters.Add("@Id", MySqlDbType.VarChar).Value = colaborador.id;
                cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = colaborador.Nome;
               
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = colaborador.Email;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = colaborador.Senha;
                cmd.Parameters.Add("@Tipo", MySqlDbType.VarChar).Value = colaborador.Tipo;

                cmd.ExecuteNonQuery();
                conexao.Close();

            }
        }

        public void AtualizarSenha(Colaborador colaborador)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(Colaborador colaborador)
        {
            string Comum = ColaboradorTipoConstant.Comum;
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into Colaborador(Nome, Email, Senha, TIPO)" +
                                                        " values(@Nome, @CPF, @Telefone, @Email, @Senha, @Tipo)", conexao);

                cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = colaborador.Nome;
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = colaborador.Email;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = colaborador.Senha;
                cmd.Parameters.Add("@Tipo", MySqlDbType.VarChar).Value = colaborador.Tipo;


                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Excluir(Colaborador colaborador)
        {
            throw new NotImplementedException();
        }

        public Colaborador Login(string Email, string Senha)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Colaborador where Email = @Email and Senha = @Senha", conexao);

                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = Email;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = Senha;

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Colaborador colaborador = new Colaborador();
                dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    colaborador.id = (Int32)(dr["Id"]);
                    colaborador.Nome = (string)(dr["Nome"]);
                    colaborador.Email = (string)(dr["Email"]);
                    colaborador.Senha = (string)(dr["Senha"]);
                    colaborador.Tipo = (string)(dr["Tipo"]);
                }
                return colaborador;
            }
        }

        public Colaborador ObterColaborador(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand(" select * from Colaborador where Id=@Id", conexao);
                cmd.Parameters.AddWithValue("@Id", id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Colaborador colaborador = new Colaborador();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    colaborador.id = (Int32)(dr["Id"]);
                    colaborador.Nome = (string)dr["Nome"];
                    colaborador.Email = (string)(dr["Email"]);
                    colaborador.Senha = (string)(dr["Senha"]);
                  
                }
                return colaborador;
            }
        }

        public List<Colaborador> ObterColaboradorPorEmail(string email)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Colaborador> ObterTodosColaboradores()
        {
            List<Colaborador> colabList = new List<Colaborador>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand(" select * from Colaborador", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    colabList.Add(
                        new Colaborador
                        {
                            id = Convert.ToInt32(dr["Id"]),
                            Nome = (string)(dr["Nome"]),
                            Email = (string)(dr["Email"]),
                            Senha = (string)(dr["Senha"]),
                            Tipo = (string)(dr["Tipo"]),


                        }
                        );
                }
                return colabList;
            }
        }

        public IPagedList<Colaborador> ObterTodosColaboradores(int? pagina)
        {
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");

            int NumeroPagina = pagina ?? 1;
            List<Colaborador> ListCat = new List<Colaborador>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from colaborador;", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    ListCat.Add(
                        new Colaborador
                        {
                            id = Convert.ToInt32(dr["Id"]),
                            Nome = (string)(dr["Nome"]),
                            Senha = (string)(dr["Senha"]),
                            Email = (string)(dr["Email"]),
                            Tipo = (string)(dr["Senha"])

                        });
                }
                return ListCat.ToPagedList<Colaborador>(NumeroPagina, RegistroPorPagina);
            }
        }

    }
}
