using ExamenAWSMiguelRicoç.Models;
using ExamenAWSMiguelRicoç.Repositories;
using ExamenAWSMiguelRicoç.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenAWSMiguelRicoç.Controllers
{
    public class JugadoresController : Controller {
        private RepositoryApuestas repo;
        private ServiceAWSS3 service;
        public JugadoresController(RepositoryApuestas repo, ServiceAWSS3 service) {
            this.repo = repo;
            this.service = service;
        }
        public IActionResult VerJugadoresByEquipo(int idequipo) {
            Equipo equipo = this.repo.FindEquipo(idequipo);
            ViewData["EQUIPO"] = equipo;
            return View(this.repo.GetJugadoresByEquipo(idequipo));
        }
        public IActionResult Details(int id) { 
            return View(this.repo.FindJugador(id)); 
        }
        [HttpGet]
        public IActionResult Create(int idequipo) {
            Equipo equipo = this.repo.FindEquipo(idequipo);
            ViewData["EQUIPO"] = equipo;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Jugador jugador, IFormFile imagen) {
            string nombreFichero = jugador.Nombre + "." + imagen.FileName.Split(".")[1];
            using (Stream stream = imagen.OpenReadStream()) {
                await this.service.UploadFileAsync(stream, nombreFichero);
            }
            jugador.Imagen = nombreFichero;
            this.repo.CreateJugador(jugador);
            return RedirectToAction("VerJugadoresByEquipo", new { idequipo = jugador.IdEquipo});
        }
    }
}
