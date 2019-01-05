using Cadastro_Pessoa.Dominio;
using Cadastro_Pessoa.Repositorio;
using Npgsql;

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

                    NpgsqlCommand cmd = new NpgsqlCommand(sql);

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

                    NpgsqlCommand cmd = new NpgsqlCommand(sql);

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
    }
}
