using Microsoft.Extensions.Configuration;
using SistemaClienteTeste.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using System.Linq;

namespace SistemaClienteTeste.Repositorio
{
    public class MensalidadeRepositorio : IMensalidadeRepositorio
    {
        private readonly IConfiguration _configuration;

        public MensalidadeRepositorio(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DataBase"));
        }     

        public async Task<List<MensalidadeModel>> ListarMensalidades()
        {
            using (var connection = CreateConnection())
            {
                return (await connection.QueryAsync<MensalidadeModel>("sp_listarMensalidades")).ToList();
            }
        }

        public async Task<MensalidadeModel> GerarMensalidade(MensalidadeModel mensalidade)
        {
            var param = new {
                Mes = mensalidade.Mes,
                Ano = mensalidade.Ano,
                Valor = mensalidade.Valor,
                Situacao = mensalidade.Situacao,
                SolicitanteId = mensalidade.SolicitanteId,

            };

            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync("sp_gerarMensalidade", param, commandType: CommandType.StoredProcedure);

                return mensalidade;
            }
        }
    }
}
