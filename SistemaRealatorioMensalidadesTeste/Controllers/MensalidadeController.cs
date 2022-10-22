using Microsoft.AspNetCore.Mvc;
using SistemaClienteTeste.Models;
using SistemaClienteTeste.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaClienteTeste.Controllers
{
    public class MensalidadeController : Controller
    {
        private readonly IMensalidadeRepositorio _mensalidadeRepositorio;
        private readonly ISolicitanteRepositorio _solicitanteRepositorio;

        public MensalidadeController(IMensalidadeRepositorio mensalidadeRepositorio, ISolicitanteRepositorio solicitanteRepositorio)
        {
            _mensalidadeRepositorio = mensalidadeRepositorio;
            _solicitanteRepositorio = solicitanteRepositorio;
        }

        public async Task<IActionResult> Index()
        {
            List<MensalidadeModel> mensalidades = await _mensalidadeRepositorio.ListarMensalidades();
            List<MensalidadeAgrupadoModel> mensalidadeAgrupadoModel = new List<MensalidadeAgrupadoModel>();

            if(mensalidades != null && mensalidades.Any())
            {
                var mensalidadeAgrupado = mensalidades
                        .Where(x => x.Situacao.ToLower() == "em aberto")
                        .GroupBy(x => new { x.SolicitanteId, x.SolicitanteNome });

                foreach (var item in mensalidadeAgrupado)
                {
                    MensalidadeAgrupadoModel agrupadoModel = new MensalidadeAgrupadoModel();
                    agrupadoModel.Nome = item.Key.SolicitanteNome;

                    int contador = 0;
                    foreach (var mensalidade in item)
                    {
                        contador++;
                        string mes = mensalidade.Mes > 9 ? $"{mensalidade.Mes}" : $"0{mensalidade.Mes}";
                        agrupadoModel.MensalidadeAberta += $" {mes}/{mensalidade.Ano} – R$ {mensalidade.Valor} ";

                        if(contador < item.Count())
                        {
                            agrupadoModel.MensalidadeAberta += "| ";
                        }
                    }

                    mensalidadeAgrupadoModel.Add(agrupadoModel);
                }
            }

            return View(mensalidadeAgrupadoModel);
        }

        public async Task<IActionResult> Criar()
        {
            MensalidadeModel mensalidade = new MensalidadeModel();
            mensalidade.Solicitantes = await _solicitanteRepositorio.BuscarTodos();

            return View(mensalidade);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(MensalidadeModel mensalidadeModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    mensalidadeModel = await _mensalidadeRepositorio.GerarMensalidade(mensalidadeModel);

                    TempData["MensagemSucesso"] = "Mensalidade gerada com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(mensalidadeModel);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos gerar sua mensalidade, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
