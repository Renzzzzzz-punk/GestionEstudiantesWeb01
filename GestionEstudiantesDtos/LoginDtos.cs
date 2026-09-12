using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEstudiantesDtos
{
    public class LoginRequestDto
    {
        public string Usuario { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseDto
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
    }
}
