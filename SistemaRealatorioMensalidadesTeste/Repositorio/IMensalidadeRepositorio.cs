using SistemaClienteTeste.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaClienteTeste.Repositorio
{
    public interface IMensalidadeRepositorio
    {
         Task<List<MensalidadeModel>> ListarMensalidades();
         Task<MensalidadeModel> GerarMensalidade(MensalidadeModel mensalidade);
    }
}
