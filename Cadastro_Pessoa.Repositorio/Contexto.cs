﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Cadastro_Pessoa.Repositorio
{
    public class Contexto:IDisposable
    {
        private readonly NpgsqlConnection minhaConexao;

        public Contexto()
        {
            minhaConexao = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["PgCon"].ConnectionString);
            minhaConexao.Open();
        }

        public void Dispose()
        {
            if (minhaConexao.State == ConnectionState.Open)
                minhaConexao.Close();
        }

        public void ExecutarComando(string cmd)
        {
            var comando = new NpgsqlCommand
            {
                CommandText = cmd,
                CommandType = CommandType.Text,
                Connection = minhaConexao
            };

            comando.ExecuteNonQuery();
        }

        public NpgsqlDataReader LeituraComando(string cmd)
        {
            var comando = new NpgsqlCommand(cmd, minhaConexao);
            return comando.ExecuteReader();
        }
    }
}