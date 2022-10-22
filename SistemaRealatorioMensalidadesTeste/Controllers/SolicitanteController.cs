using Microsoft.AspNetCore.Mvc;
using SistemaClienteTeste.Models;
using SistemaClienteTeste.Repositorio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaClienteTeste.Controllers
{
    public class SolicitanteController : Controller
    {
        private readonly ISolicitanteRepositorio _solicitanteRepositorio;

        public SolicitanteController(ISolicitanteRepositorio solicitanteRepositorio)
        {
            _solicitanteRepositorio = solicitanteRepositorio;
        }

        public async Task<IActionResult> Index()
        {
            List<SolicitanteModel> solicitantes = await _solicitanteRepositorio.BuscarTodos();

            return View(solicitantes);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public async Task<IActionResult> Editar(int id)
        {
            SolicitanteModel solicitante = await _solicitanteRepositorio.BuscarPorID(id);
            return View(solicitante);
        }

        public async Task<IActionResult> ApagarConfirmacao(int id)
        {
            SolicitanteModel solicitante = await _solicitanteRepositorio.BuscarPorID(id);
            return View(solicitante);
        }

        public async Task<IActionResult> Apagar(int id)
        {
            try
            {
                bool apagado = await _solicitanteRepositorio.Apagar(id);

                if(apagado) TempData["MensagemSucesso"] = "Solicitante apagado com sucesso!"; else TempData["MensagemErro"] = "Ops, não conseguimos cadastrar seu Solicitante, tente novamante!";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar seu Solicitante, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Criar(SolicitanteModel solicitanteModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    solicitanteModel = await _solicitanteRepositorio.Adicionar(solicitanteModel);

                    TempData["MensagemSucesso"] = "Solicitante cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(solicitanteModel);
            }
            catch (Exception erro)
            { 
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu Solicitante, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(SolicitanteModel solicitanteModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    solicitanteModel = await _solicitanteRepositorio.Atualizar(solicitanteModel);
                    TempData["MensagemSucesso"] = "Solicitante alterado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(solicitanteModel);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos atualizar seu solicitante, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
