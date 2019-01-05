﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Npgsql;
using Cadastro_Pessoa.ViewModel;
using System.Collections;
using Newtonsoft.Json;
using Cadastro_Pessoa.Dominio;
using Cadastro_Pessoa.Aplicacao;

namespace Cadastro_Pessoa.Controllers
{
    public class PessoaController : Controller
    {
        [Route("Pessoa/Index")]
        public ActionResult Index()
        {
            PessoaViewModel viewModel = new PessoaViewModel();
            return View(viewModel);
        }

        // GET: Pessoa
        [Route("Pessoa/Inserir")]
        public ActionResult Inserir()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Inserir(Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                var appPessoa = new PessoaAplicacao();
                appPessoa.InserirPessoa(pessoa);
                return Index();
            }

            return Inserir(pessoa);
        }

        [HttpPost]
        public ActionResult AlterarPessoa(Pessoa pessoa)
        {
            if(ModelState.IsValid)
            {
                var appPessoa = new PessoaAplicacao();
                appPessoa.AlterarPessoa(pessoa);
                return Index();
            }

            return AlterarPessoa(pessoa);
        }

        /*
        public static string ListarTodas(string CampoPesquisa, string TextoPesquisa)
        {
            var json = "";
            using (Contexto = new Context())
            {
                try
                {
                    string sql = "SELECT id, Nome, Apelido, CpfCnpj, TipoPessoa, DataCadastro, DataAlteracao, Ativo FROM pessoa";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, con);

                    switch (CampoPesquisa)
                    {
                        case "Código":
                            sql += "WHERE codigo = @texto";
                            int codigo;
                            Int32.TryParse(TextoPesquisa, out codigo);
                            cmd.Parameters.Add(new NpgsqlParameter("@texto", codigo));
                            break;
                        case "Nome(Ativos)":
                            sql += "WHERE nome = @texto AND ATIVO = true";
                            cmd.Parameters.Add(new NpgsqlParameter("@texto", TextoPesquisa));
                            break;
                        case "Nome(Todos)":
                            sql += "WHERE nome = @texto";
                            cmd.Parameters.Add(new NpgsqlParameter("@texto", TextoPesquisa));
                            break;
                    }

                    cmd.Parameters.Add(new NpgsqlParameter("@texto", TextoPesquisa));

                    List<Pessoa> lista = new List<Pessoa>();

                    NpgsqlDataReader dtr = cmd.ExecuteReader();

                    while (dtr.HasRows)
                    {
                        Pessoa p = new Pessoa();
                        p.Id = dtr.GetInt32(0);
                        p.Nome = dtr.GetString(1);
                        p.Apelido = dtr.GetString(2);
                        p.CpfCnpj = dtr.GetString(3);
                        p.TipoPessoa = dtr.GetString(4);
                        p.DataCadastro = (DateTime)dtr.GetDate(5);
                        p.DataAlteracao = dtr.GetDateTime(6);
                        p.Ativo = dtr.GetBoolean(7);

                        lista.Add(p);
                    }

                    json = JsonConvert.SerializeObject(lista);
                }

                catch (NpgsqlException ex)
                {
                    ex.Message.ToString();
                }

                finally
                {
                    con.Close();
                }

                return json;
            }
        }*/
    }
}