using ExamenAWSMiguelRicoç.Models;
using ExamenAWSMiguelRicoç.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenAWSMiguelRicoç.Controllers
{
    public class ApuestasController : Controller {
        private RepositoryApuestas repo;
        public ApuestasController(RepositoryApuestas repo) {
            this.repo = repo;
        }
        public IActionResult Index() {
            return View(this.repo.GetApuestas());
        }
        [HttpGet]
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Apuesta apuesta) {
            this.repo.CreateApuesta(apuesta);
            return RedirectToAction("Index");
        }
    }
}
