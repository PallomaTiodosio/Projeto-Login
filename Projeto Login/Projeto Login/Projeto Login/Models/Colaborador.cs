using System.ComponentModel.DataAnnotations;

namespace Projeto_Login.Models
{
    public class Colaborador
    {

        [Display(Name = "Código", Description = "Código.")]
        public int ID { get; set; }

        [Display(Name = "Nome Completo", Description = "Nome e Sobrenome.")]
        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        public string Nome { get; set; }


        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "O email não é válido")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "A senha é obrigatório.")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 10 aracteres")]
        public string Senha { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "O Tipo é obrigatório")]
        public string Tipo { get; set; }
    }
}
