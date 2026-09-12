using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionEstudiantesWeb.Models
{
    public class Estudiantes
    {

        public Estudiantes()
        {
            this.Nombre = string.Empty;
            this.ApellidoPaterno = string.Empty;
            this.ApellidoMaterno = string.Empty;
            this.Sexo = string.Empty;
            this.TipoDocumento = string.Empty;
            this.NumeroDocumento = string.Empty;
            this.Carrera = string.Empty;
            this.Ciclo = string.Empty;
            this.Turno = string.Empty;
        }


        public int IdEstudiante { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Sexo { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Carrera { get; set; }
        public string Ciclo { get; set; }
        public string Turno { get; set; }
    }
}