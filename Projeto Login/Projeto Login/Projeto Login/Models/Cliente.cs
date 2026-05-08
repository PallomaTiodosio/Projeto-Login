using System.ComponentModel.DataAnnotations;

namespace Projeto_Login.Models
{
    public class Cliente
    {
        [Display (Name = "Código", Description= "Código.")]
        public int ID { get; set; }

        [Display (Name = "Nome Completo", Description ="Nome e Sobrenome.")]
        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        public string Nome { get; set; }

        [Display(Name = "Nascimento" )]
        [Required(ErrorMessage = "A data é obrigatória")]
        public DateTime Nascimento { get; set; }

        [Display(Name = "Sexo")]
        [Required(ErrorMessage = "O Sexo é obrigatório.")]
        [StringLength(1,ErrorMessage = "Deve conter apenas 1 caracter")]
        public string Sexo { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public string CPF { get; set; }


        [Display(Name = "Celular")]
        [Required(ErrorMessage = "O Celular é obrigatório.")]
        public string Telefone { get; set; }


        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "O email não é válido")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido")]
        public string Email { get; set; }


        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "A senha é obrigatório.")]
        [StringLength(10,MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 10 aracteres")]
        public string Senha { get; set; }


        [Display(Name = "Situação")]
        [Required(ErrorMessage = "A situação é obrigatória")]
        public string Situação {  get; set; }







    }
}
