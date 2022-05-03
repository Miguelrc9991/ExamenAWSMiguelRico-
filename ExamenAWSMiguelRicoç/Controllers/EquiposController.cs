using ExamenAWSMiguelRicoç.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenAWSMiguelRicoç.Controllers { 
    public class EquiposController : Controller {
        private RepositoryApuestas repo;
        public EquiposController(RepositoryApuestas repo) {
            this.repo = repo;
        }
        public IActionResult Index() {
            return View(this.repo.GetEquipos());
        }
    }
}
