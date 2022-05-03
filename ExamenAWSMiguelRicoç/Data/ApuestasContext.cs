using ExamenAWSMiguelRicoç.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenAWSMiguelRicoç.Data
{
    public class ApuestasContext :DbContext
    {
        public ApuestasContext(DbContextOptions<ApuestasContext> options)
: base(options) { }
        public DbSet<Jugador> Jugadores { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Apuesta> Apuestas { get; set; }


    }
}
