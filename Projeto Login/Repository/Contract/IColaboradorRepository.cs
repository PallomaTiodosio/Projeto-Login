using Projeto_Login.Models;

namespace Projeto_Login.Repository.Contract
{
    public interface IColaboradorRepository
    {
        Colaborador Login(string Email, string Senha);

        void Cadastrar(Colaborador colaborador);
        void Atualizar (Colaborador colaborador);
        void AtualizarSenha (Colaborador colaborador);
        void Excluir (Colaborador colaborador);

        Colaborador ObterColaborador (int id);
        List<Colaborador> ObterColaboradorPorEmail(string email);
        IEnumerable<Colaborador> ObterTodosColaboradores();

        //IPagedList <Colaborador> ObterTodosColaboradores(int? pagina);



    }
}
