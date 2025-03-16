using Microsoft.AspNetCore.Mvc;
using Curriculos.DAO;
using Curriculos.Models;
using System.Collections.Generic;
using System;

namespace Curriculos.Controllers
{
    public class CurriculoController : Controller
    {
        public IActionResult Index()
        {
            CurriculoDAO curriculoDAO = new CurriculoDAO();
            List<CurriculoViewModel> curriculos = curriculoDAO.Listar();
            return View(curriculos);
        }

        public IActionResult Create()
        {
            CurriculoViewModel curriculo = new CurriculoViewModel();
            curriculo.DataNascimento = DateTime.Now;
            return View("Formulario", curriculo);
        }

        public IActionResult Salvar(CurriculoViewModel curriculo)
        {
            CurriculoDAO curriculoDAO = new CurriculoDAO();
            curriculoDAO.Inserir(curriculo);
            return RedirectToAction("index");
        }
    }
}
