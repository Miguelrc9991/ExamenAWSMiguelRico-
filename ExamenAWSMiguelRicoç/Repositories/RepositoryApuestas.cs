using ExamenAWSMiguelRicoç.Data;
using ExamenAWSMiguelRicoç.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenAWSMiguelRicoç.Repositories
{
   
    public class RepositoryApuestas
    {
        private ApuestasContext context;
        public RepositoryApuestas(ApuestasContext context)
        {
            this.context = context;
        }
        public List<Equipo> GetEquipos()
        {
            return this.context.Equipos.ToList();
        }
        public List<Apuesta> GetApuestas()
        {
            return this.context.Apuestas.ToList();
        }
        public List<Jugador> GetJugadores(int idequipo)
        {
            var consulta = from datos in this.context.Jugadores
                           where datos.IdEquipo == idequipo
                           select datos;
            return consulta.ToList();
        }
        public int GetLastId()
        {
            if (this.context.Jugadores.Count() == 0)
            {
                return 1;
            }
            else{
                int id =
           this.context.Jugadores.Max(x => x.IdJugador) + 1;
                return id;
            }
           
        }
        public int GetLastIdApuesta()
        {
            if (this.context.Apuestas.Count() == 0)
            {
                return 1;
            }
            else
            {
                int id =
           this.context.Apuestas.Max(x => x.IdApuesta) + 1;
                return id;
            }

        }
        public void InsertJugador(int idjugador,string nombre,string posicion,string imagen,int idequipo)
        {
            int idjugadorr = this.GetLastId();
            Jugador j = new Jugador
            {
                IdJugador = idjugadorr,
                Nombre = nombre,
                Posicion=posicion,
                Imagen=imagen,
                IdEquipo=idequipo
                
            };
            this.context.Jugadores.Add(j);
            this.context.SaveChanges();
        }

        public void nuevaapuesta(int idapuesta, string usuario,  int idequipolocal, int idequipovisitante, int golesequipolocal,int golesequipovisitante)
        {
            int idapuestaf = this.GetLastIdApuesta();
            Apuesta a = new Apuesta
            {
               IdApuesta=idapuesta,
               Usuario = usuario,
               IdEquipoLocal=idequipolocal,
               IdEquipoVisitante=idequipovisitante,
               GolesEquipoLocal=golesequipolocal,
               GolesEquipoVisitante=golesequipovisitante

            };
            this.context.Apuestas.Add(a);
            this.context.SaveChanges();
        }


    }
}
