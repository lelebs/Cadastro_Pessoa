using Cadastro_Pessoa.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cadastro_Pessoa.ViewModel
{
    public class PessoaViewModel
    {
        public string CampoPesquisa { get; set; }
        public string TipoPesquisa { get; set; }
        public IEnumerable<Pessoa> Pessoas { get; set; }
    }
}