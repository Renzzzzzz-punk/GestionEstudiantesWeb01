using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEstudiantesDtos
{
    public class UsuarioDtos
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public string PasswordHash { get; set; }
        public string Rol { get; set; }
        public string Estado { get; set; }
    }
}
