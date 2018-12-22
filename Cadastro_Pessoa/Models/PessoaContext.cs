using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Cadastro_Pessoa.Models;

namespace Cadastro_Pessoa.Context
{
    public class PessoaContext:DbContext
    {
        public PessoaContext():base("PgCon")
        {

        }

        public DbSet<Pessoa> Pessoas { get; set; }
    }
}