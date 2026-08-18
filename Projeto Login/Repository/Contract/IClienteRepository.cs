using Projeto_Login.Models;
using X.PagedList;

namespace Projeto_Login.Repository.Contract
{
    public interface IClienteRepository
    {
        Cliente Login(string Email, string Senha);

        void Cadastrar (Cliente cliente);
        void Atualizar (Cliente cliente);
        void Excluir(int Id);
        Cliente ObterCliente(int Id);
        IEnumerable<Cliente> ObterTodosClientes();
        IPagedList<Cliente> ObterTodosClientes(int? pagina, string pesquisa);
    }
}
