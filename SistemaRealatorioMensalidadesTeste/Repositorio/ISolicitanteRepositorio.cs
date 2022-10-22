using SistemaClienteTeste.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaClienteTeste.Repositorio
{
    public interface ISolicitanteRepositorio
    {
        Task<List<SolicitanteModel>> BuscarTodos();
        Task<SolicitanteModel> BuscarPorID(int id);
        Task<SolicitanteModel> Adicionar(SolicitanteModel contato);
        Task<SolicitanteModel> Atualizar(SolicitanteModel contato);
        Task<bool> Apagar (int id);
    }
}
