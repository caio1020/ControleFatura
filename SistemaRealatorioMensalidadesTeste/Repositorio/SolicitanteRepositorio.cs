using Dapper;
using Microsoft.Extensions.Configuration;
using SistemaClienteTeste.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaClienteTeste.Repositorio
{
    public class SolicitanteRepositorio : ISolicitanteRepositorio
    {
        private readonly IConfiguration _configuration;

        public SolicitanteRepositorio(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DataBase"));
        }

        public async Task<SolicitanteModel> BuscarPorID(int id)
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<SolicitanteModel>("sp_listarSolicitantePorId", new { Codigo = id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<SolicitanteModel>> BuscarTodos()
        {
            using (var connection = CreateConnection())
            {
                return (await connection.QueryAsync<SolicitanteModel>("sp_listarTodosSolicitantes")).ToList();
            }
        }

        public async Task<SolicitanteModel> Adicionar(SolicitanteModel solicitante)
        {
            var param = new {
                Nome = solicitante.Nome,
                Telefone = solicitante.Telefone,
               
            };

            using (var connection = CreateConnection())
            {
               await connection.ExecuteAsync("sp_cadastrarSolicitante", param, commandType: CommandType.StoredProcedure);

                return solicitante;
            }
        }

        public async Task<SolicitanteModel> Atualizar(SolicitanteModel solicitante)
        {
            SolicitanteModel solicitanteDB = await BuscarPorID(solicitante.Codigo);

            if (solicitanteDB == null) throw new Exception("Houve um erro na atualização do cliente!");

            var param = new {
                Codigo = solicitante.Codigo,
                Nome = solicitante.Nome,
                Telefone = solicitante.Telefone
              
            };

            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync("sp_atualizarSolicitante", param, commandType: CommandType.StoredProcedure);

                return solicitante;
            }
        }

        public async Task<bool> Apagar(int id)
        {
            SolicitanteModel solicitanteDB = await BuscarPorID(id);

            if (solicitanteDB == null) throw new Exception("Houve um erro na deleção do solicitante!");

            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync("sp_deletarSolicitantePorId", new { Id = id}, commandType: CommandType.StoredProcedure);

                return true;
            }
        }
    }
}
