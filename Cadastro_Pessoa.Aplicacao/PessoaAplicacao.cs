using Cadastro_Pessoa.Dominio;
using Cadastro_Pessoa.Repositorio;
using Npgsql;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cadastro_Pessoa.Aplicacao
{
    public class PessoaAplicacao
    {
        private Contexto contexto;

        public void InserirPessoa(Pessoa pessoa)
        {
            using (contexto = new Contexto())
            {
                try
                {
                    pessoa.CpfCnpj = pessoa.CpfCnpj.Replace(".", "");
                    pessoa.CpfCnpj = pessoa.CpfCnpj.Replace("-", "");
                    pessoa.CpfCnpj = pessoa.CpfCnpj.Replace("/", "");

                    string sql = "INSERT INTO Pessoa(nome, apelido, cpfcnpj, tipopessoa, datacadastro," +
                                    " dataalteracao, ativo) values(@nome, @apelido, @cpfCnpj, @tipoPessoa, @dataCadastro," +
                                    " @dataAlteracao, @ativo)";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, contexto.Conexao());

                    var dataCadastroStr = DateTime.Now.ToString("dd/MM/yyyy");
                    var dataCadastro = DateTime.Parse(dataCadastroStr);

                    var dataAlteracaoStr = DateTime.Now.ToString("dd/MM/yyyyThh:mm");
                    var dataAlteracao = DateTime.Parse(dataAlteracaoStr);

                    var dataCadastroSql = NpgsqlTypes.NpgsqlDate.ToNpgsqlDate(dataCadastro);
                    var dataAlteracaoSql = NpgsqlTypes.NpgsqlDateTime.ToNpgsqlDateTime(pessoa.DataAlteracao);

                    cmd.Parameters.Add(new NpgsqlParameter("@nome", pessoa.Nome));
                    cmd.Parameters.Add(new NpgsqlParameter("@apelido", pessoa.Apelido));
                    cmd.Parameters.Add(new NpgsqlParameter("@cpfCnpj", pessoa.CpfCnpj));
                    cmd.Parameters.Add(new NpgsqlParameter("@tipoPessoa", pessoa.TipoPessoa));
                    cmd.Parameters.Add(new NpgsqlParameter("@dataCadastro", dataCadastroSql));
                    cmd.Parameters.Add(new NpgsqlParameter("@dataAlteracao", dataAlteracaoSql));
                    cmd.Parameters.Add(new NpgsqlParameter("@ativo", pessoa.Ativo));

                    cmd.ExecuteNonQuery();
                }

                catch (NpgsqlException ex)
                {
                    ex.Message.ToString();
                }
            }
        }

        public void AlterarPessoa(Pessoa pessoa)
        {
            using (contexto = new Contexto())
            {
                try
                {
                    string sql = "UPDATE Pessoa SET nome = @nome, cpfcnpj = @cpfCnpj, tipopessoa = @tipoPessoa," +
                                 "dataalteracao = @dataAlteracao, ativo = @ativo WHERE ID=@id";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, contexto.Conexao());

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
            }
        }

        public IEnumerable<Pessoa> ListarTodos(string campoPesquisa, string textoPesquisa)
        {
            List<Pessoa> lista = new List<Pessoa>();

            using (contexto = new Contexto())
            {
                try
                {
                    string sql = "SELECT id, Nome, CpfCnpj, Ativo FROM pessoa ";

                    NpgsqlCommand cmd = new NpgsqlCommand();

                    switch (campoPesquisa)
                    {
                        case "Código":
                            sql += "WHERE id = @texto";
                            int codigo;
                            Int32.TryParse(textoPesquisa, out codigo);
                            cmd = new NpgsqlCommand(sql, contexto.Conexao());
                            cmd.Parameters.Add(new NpgsqlParameter("@texto", codigo));
                            break;
                        case "Nome(Ativos)":
                            sql += "WHERE nome = @texto AND ATIVO = true";
                            cmd = new NpgsqlCommand(sql, contexto.Conexao());
                            cmd.Parameters.Add(new NpgsqlParameter("@texto", textoPesquisa));
                            break;
                        case "Nome(Todos)":
                            sql += "WHERE nome = @texto";
                            cmd = new NpgsqlCommand(sql, contexto.Conexao());
                            cmd.Parameters.Add(new NpgsqlParameter("@texto", textoPesquisa));
                            break;
                    }

                    NpgsqlDataReader dtr = cmd.ExecuteReader();

                    while (dtr.Read())
                    {
                        Pessoa p = new Pessoa();
                        p.Id = dtr.GetInt16(0);
                        p.Nome = dtr.GetString(1);
                        p.CpfCnpj = dtr.GetString(2);
                        p.Ativo = dtr.GetBoolean(3);

                        lista.Add(p);
                    }
                }

                catch (NpgsqlException ex)
                {
                    ex.Message.ToString();
                }

                IEnumerable<Pessoa> pessoas = lista;

                return pessoas;
            }
        }

        public string ListarUltimoInserido()
        {
            using (contexto = new Contexto())
            {
                try
                {
                    string sql = "SELECT id FROM pessoa order by id desc limit 1";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, contexto.Conexao());
                    NpgsqlDataReader dtr;

                    dtr = cmd.ExecuteReader();
                    return dtr.GetString(0);
                }

                catch(Exception ex)
                {
                    ex.Message.ToString();
                    return "";
                }
            }
        }
    }
}
