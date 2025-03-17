using Microsoft.AspNetCore.Mvc;
using Curriculos.DAO;
using Curriculos.Models;
using System.Collections.Generic;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Curriculos.Controllers
{
    public class CurriculoController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                CurriculoDAO curriculoDAO = new CurriculoDAO();
                List<CurriculoViewModel> curriculos = curriculoDAO.Listar();
                return View(curriculos);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Create()
        {
            try
            {
                CurriculoViewModel curriculo = new CurriculoViewModel();
                curriculo.DataNascimento = DateTime.Now;
                return View("Formulario", curriculo);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }

        }

        public IActionResult Salvar(CurriculoViewModel curriculo)
        {
            try
            {
                CurriculoDAO curriculoDAO = new CurriculoDAO();
                curriculoDAO.Inserir(curriculo);
                return RedirectToAction("index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }

        }
        public IActionResult Exibicao(string cpf)
        {
            try
            {
                CurriculoDAO curriculoDAO = new CurriculoDAO();
                CurriculoViewModel curriculo= curriculoDAO.Consultar(cpf);
                return View("Exibicao", curriculo);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
    }
}
