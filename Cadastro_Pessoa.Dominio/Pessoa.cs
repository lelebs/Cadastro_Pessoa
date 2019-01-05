using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro_Pessoa.Dominio
{
    public class Pessoa
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [StringLength(50, ErrorMessage = "O campo nome não pode ser maior que 50 carácteres")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Apelido")]
        public string Apelido { get; set; }

        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Data de Alteração")]
        public DateTime DataAlteracao { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "CPF ou CNPJ")]
        public string CpfCnpj { get; set; }

        [Display(Name = "Tipo Pessoa")]
        public string TipoPessoa { get; set; }

        public bool Ativo { get; set; }
    }
}
