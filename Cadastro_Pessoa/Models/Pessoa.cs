﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cadastro_Pessoa.Models
{
    public class Pessoa
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]

        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [StringLength(50)]
        public string Nome { get; set; }

        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Data de Alteração")]
        public DateTime DataAlterecao { get; set; }

        [Display(Name = "CPF ou CNPJ")]
        public string CpfCnpj { get; set; }

        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }
    }
}