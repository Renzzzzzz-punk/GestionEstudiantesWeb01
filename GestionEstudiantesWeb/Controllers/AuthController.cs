using GestionEstudiantesDtos;
using GestionEstudiantesDtos.Utilidades;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Mvc;

namespace GestionEstudiantesWeb.Controllers
{
    public class AuthController : Controller
    {
        private string Conexion => ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;

        [HttpPost]
        public JsonResult Login(LoginRequestDto request)
        {
            UsuarioDtos usuario = null;

            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();
                string sql = "SELECT * FROM TBUsuarios WHERE Usuario = @Usuario";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Usuario", request.Usuario);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            usuario = new UsuarioDtos
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Nombre = dr["Nombre"].ToString(),
                                Usuario = dr["Usuario"].ToString(),
                                PasswordHash = dr["PasswordHash"].ToString(),
                                Rol = dr["Rol"].ToString(),
                                Estado = dr["Estado"].ToString()
                            };
                        }
                    }
                }
            }

            if (usuario == null)
                return Json(new { Exito = false, Mensaje = "Usuario no encontrado" });

            if (usuario.Estado != "Activo")
                return Json(new { Exito = false, Mensaje = "Usuario inactivo" });

            string passwordDesencriptada;
            try
            {
                passwordDesencriptada = CifradoAES.Desencriptar(usuario.PasswordHash);
            }
            catch
            {
                return Json(new { Exito = false, Mensaje = "Error al validar credenciales" });
            }

            if (passwordDesencriptada != request.Password)
                return Json(new { Exito = false, Mensaje = "Contraseña incorrecta" });

            string token = GenerarToken(usuario);

            return Json(new
            {
                Exito = true,
                Mensaje = "Login correcto",
                Token = token,
                Nombre = usuario.Nombre,
                Rol = usuario.Rol
            });
        }

        private string GenerarToken(UsuarioDtos usuario)
        {
            string clave = ConfigurationManager.AppSettings["JwtSecretKey"];
            string issuer = ConfigurationManager.AppSettings["JwtIssuer"];
            int minutos = Convert.ToInt32(ConfigurationManager.AppSettings["JwtExpireMinutes"]);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.Usuario),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(clave));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(minutos),
                signingCredentials: credenciales
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        // GET: Auth/Login
        public ActionResult Login()
        {
            return View();
        }
    }
}