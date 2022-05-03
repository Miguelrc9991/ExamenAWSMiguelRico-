using ExamenAWSMiguelRicoç.Models;
using ExamenAWSMiguelRicoç.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenAWSMiguelRicoç.Controllers
{
    public class ApuestasController : Controller
    {
        private RepositoryApuestas repo;
        private RepositoryS3 repos3;

        public ApuestasController(RepositoryApuestas repo, RepositoryS3 repos3)
        {
            this.repo = repo;
            this.repos3 = repos3;
        }
        public IActionResult Index()
        {
            List<Equipo> equipos = this.repo.GetEquipos();
            return View(equipos);
        }
        public IActionResult GetJugadores(int idequipo)
        {
            ViewData["IDEQUIPO"] = idequipo;
            List<Jugador> jugadores = this.repo.GetJugadores(idequipo);
            return View(jugadores);
        }
        public  IActionResult NuevoJugador(int idequipo)
        {
            ViewData["IDEQUIPO"] = idequipo;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NuevoJugadorAsync(Jugador jugador,IFormFile file)
        {
            string extension = file.FileName.Split(".")[1];
            string fileName = jugador.IdJugador.ToString().Trim() + "." + extension;
            using (Stream stream = file.OpenReadStream())
            {
                await this.repos3.UploadFileAsync(stream, fileName);
            }
            jugador.Imagen = fileName;
             this.repo.InsertJugador(jugador.IdJugador,jugador.Nombre,jugador.Posicion,jugador.Imagen,jugador.IdEquipo);
            return RedirectToAction("Index");
        }
        public IActionResult NuevaApuesta()
        {
            return View();
        }
        [HttpPost]
        public IActionResult NuevaApuesta(Apuesta apuesta)
        {
            this.repo.nuevaapuesta(apuesta.IdApuesta,apuesta.Usuario,apuesta.IdEquipoLocal,apuesta.IdEquipoVisitante,apuesta.GolesEquipoLocal,apuesta.GolesEquipoVisitante);
            return RedirectToAction("GetApuestas");
        }
        public IActionResult GetApuestas()
        {
            List<Apuesta> apuestas = this.repo.GetApuestas();
            return View(apuestas);

        }
    }
}
