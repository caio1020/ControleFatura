using Microsoft.AspNetCore.Mvc;
using SistemaClienteTeste.Models;
using SistemaClienteTeste.Repositorio;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaClienteTeste.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly IMensalidadeRepositorio _mensalidadeRepositorio;

        public RelatorioController(IMensalidadeRepositorio mensalidadeRepositorio)
        {
            _mensalidadeRepositorio = mensalidadeRepositorio;
        }

        public async Task<IActionResult> Index()
        {
            List<MensalidadeModel> mensalidades = await _mensalidadeRepositorio.ListarMensalidades();
            
            return View(mensalidades);
        }
    }
}
