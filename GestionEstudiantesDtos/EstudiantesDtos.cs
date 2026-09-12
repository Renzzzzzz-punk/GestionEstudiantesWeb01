using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEstudiantesDtos
{
    public class EstudiantesDtos
    {
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
