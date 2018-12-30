using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using Npgsql;

namespace Cadastro_Pessoa.Controllers
{
    public class ConnectionFactory
    {
        public static NpgsqlConnection CreateConnection()
        {
            string connectionString = "Server=localhost; User Id=postgres; Database=CadsatroPessoa; Port=5432; Password=admin";

            NpgsqlConnection conn = new NpgsqlConnection(connectionString);

            conn.Open();

            return conn;
        }
    }
}