using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Projeto_Login.Libraries.Login;
using Projeto_Login.Models;

namespace Projeto_Login.Libraries.Filtro
{
	public class ClienteAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
		LoginCliente _loginCliente;
		public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginCliente = (LoginCliente)context.HttpContext.RequestServices.GetService(typeof(LoginCliente));
            Cliente cliente = _loginCliente.GetCliente();
            if (cliente == null)
            {
                context.Result = new ContentResult() { Content = "Acesso Negado." };
            }
        }
    }
}
