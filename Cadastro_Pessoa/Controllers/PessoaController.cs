using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cadastro_Pessoa.Models;
using Npgsql;

namespace Cadastro_Pessoa.Controllers
{
    public class PessoaController : Controller
    {
        // GET: Pessoa
        [Route("Pessoa/Inserir")]
        public ActionResult Inserir()
        {
            return View();
        }

        public static void InserirPessoa(Pessoa pessoa)
        {
            using (NpgsqlConnection con = ConnectionFactory.CreateConnection())
            {
                try
                {
                    string sql = "INSERT INTO CadastroPessoa(nome, cpfcnpj, tipopessoa, datacadastro," +
                                 " dataalteracao, ativo) value(@nome, @cpfCnpj, @tipoPessoa, @dataCadastro" +
                                 " @dataAlteracao, @ativo)";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, con);

                    cmd.Parameters.Add(new NpgsqlParameter("@nome", pessoa.Nome));
                    cmd.Parameters.Add(new NpgsqlParameter("@cpfCnpj", pessoa.CpfCnpj));
                    cmd.Parameters.Add(new NpgsqlParameter("@tipoPessoa", pessoa.TipoPessoa));
                    cmd.Parameters.Add(new NpgsqlParameter("@dataCadastro", pessoa.DataCadastro));
                    cmd.Parameters.Add(new NpgsqlParameter("@dataAlteracao", pessoa.DataAlterecao));
                    cmd.Parameters.Add(new NpgsqlParameter("@ativo", pessoa.Ativo));

                    cmd.ExecuteNonQuery();
                }

                catch(NpgsqlException ex)
                {
                    ex.Message.ToString();
                }

                finally
                {
                    con.Close();
                }
            }
        }

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
                    cmd.Parameters.Add(new NpgsqlParameter("@dataAlteracao", pessoa.DataAlterecao));
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
    }
}