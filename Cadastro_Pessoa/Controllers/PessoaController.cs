using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cadastro_Pessoa.Models;
using Npgsql;
using Cadastro_Pessoa.ViewModel;
using System.Collections;
using Newtonsoft.Json;

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
        public ActionResult Index(Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                using (NpgsqlConnection con = ConnectionFactory.CreateConnection())
                {
                    try
                    {
                        pessoa.CpfCnpj = pessoa.CpfCnpj.Replace(".", "");
                        pessoa.CpfCnpj = pessoa.CpfCnpj.Replace("-", "");
                        pessoa.CpfCnpj = pessoa.CpfCnpj.Replace("/", "");

                        string sql = "INSERT INTO Pessoa(nome, apelido, cpfcnpj, tipopessoa, datacadastro," +
                                     " dataalteracao, ativo) values(@nome, @apelido, @cpfCnpj, @tipoPessoa, @dataCadastro," +
                                     " @dataAlteracao, @ativo)";

                        NpgsqlCommand cmd = new NpgsqlCommand(sql, con);

                        var dataCadastro = NpgsqlTypes.NpgsqlDate.ToNpgsqlDate(pessoa.DataCadastro);
                        var dataAlteracao = NpgsqlTypes.NpgsqlDateTime.ToNpgsqlDateTime(pessoa.DataAlteracao);

                        cmd.Parameters.Add(new NpgsqlParameter("@nome", pessoa.Nome));
                        cmd.Parameters.Add(new NpgsqlParameter("@apelido", pessoa.Apelido));
                        cmd.Parameters.Add(new NpgsqlParameter("@cpfCnpj", pessoa.CpfCnpj));
                        cmd.Parameters.Add(new NpgsqlParameter("@tipoPessoa", pessoa.TipoPessoa));
                        cmd.Parameters.Add(new NpgsqlParameter("@dataCadastro", dataCadastro));
                        cmd.Parameters.Add(new NpgsqlParameter("@dataAlteracao", dataAlteracao));
                        cmd.Parameters.Add(new NpgsqlParameter("@ativo", pessoa.Ativo));

                        cmd.ExecuteNonQuery();
                    }

                    catch (NpgsqlException ex)
                    {
                        ex.Message.ToString();
                    }

                    finally
                    {
                        con.Close();
                    }
                }
            }

            return Index();
        }

        [HttpPost]
        public void AlterarPessoa(Pessoa pessoa)
        {
            using (NpgsqlConnection con = ConnectionFactory.CreateConnection())
            {
                try
                {
                    string sql = "UPDATE Pessoa SET nome = @nome, cpfcnpj = @cpfCnpj, tipopessoa = @tipoPessoa," +
                                 "dataalteracao = @dataAlteracao, ativo = @ativo WHERE ID=@id";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, con);

                    cmd.Parameters.Add(new NpgsqlParameter("@id", pessoa.Id));
                    cmd.Parameters.Add(new NpgsqlParameter("@nome", pessoa.Nome));
                    cmd.Parameters.Add(new NpgsqlParameter("@cpfCnpj", pessoa.CpfCnpj));
                    cmd.Parameters.Add(new NpgsqlParameter("@tipoPessoa", pessoa.TipoPessoa));
                    cmd.Parameters.Add(new NpgsqlParameter("@dataAlteracao", pessoa.DataAlteracao));
                    cmd.Parameters.Add(new NpgsqlParameter("@ativo", pessoa.Ativo));

                    cmd.ExecuteNonQuery();
                }

                catch (NpgsqlException ex)
                {
                    ex.Message.ToString();
                }

                finally
                {
                    con.Close();
                }
            }
        }

        public static string ListarTodas(string CampoPesquisa, string TextoPesquisa)
        {
            var json = "";
            using (NpgsqlConnection con = ConnectionFactory.CreateConnection())
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

                    while(dtr.HasRows)
                    {
                        Pessoa p = new Pessoa();
                        p.Id = dtr.GetInt32(0);
                        p.Nome = dtr.GetString(1);
                        p.Apelido = dtr.GetString(2);
                        p.CpfCnpj = dtr.GetString(3);
                        p.TipoPessoa = dtr.GetString(4);
                        p.DataCadastro = (DateTime) dtr.GetDate(5);
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
        }
    }
}