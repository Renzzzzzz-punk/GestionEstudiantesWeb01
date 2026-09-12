using GestionEstudiantesDtos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace GestionEstudiantesWeb.Controllers
{
    public class EstudiantesController : Controller
    {
        private string Conexion => ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;

        // GET: Estudiantes/Inicio
        public ActionResult Inicio()
        {
            List<EstudiantesDtos> lista = new List<EstudiantesDtos>();

            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();
                string sql = "SELECT * FROM TBEstudiantes";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new EstudiantesDtos
                        {
                            IdEstudiante = Convert.ToInt32(dr["IdEstudiante"]),
                            Nombre = dr["Nombre"].ToString(),
                            ApellidoPaterno = dr["ApellidoPaterno"].ToString(),
                            ApellidoMaterno = dr["ApellidoMaterno"].ToString(),
                            Sexo = dr["Sexo"].ToString(),
                            TipoDocumento = dr["TipoDocumento"].ToString(),
                            NumeroDocumento = dr["NumeroDocumento"].ToString(),
                            Carrera = dr["Carrera"].ToString(),
                            Ciclo = dr["Ciclo"].ToString(),
                            Turno = dr["Turno"].ToString()
                        });
                    }
                }
            }

            return View(lista);
        }

        // GET: Estudiantes/Crear
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Estudiantes/Crear
        [HttpPost]
        public ActionResult Crear(EstudiantesDtos estudiante)
        {
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();
                string sql = @"INSERT INTO TBEstudiantes
                                (Nombre, ApellidoPaterno, ApellidoMaterno, Sexo, TipoDocumento,
                                 NumeroDocumento, Carrera, Ciclo, Turno)
                               VALUES
                                (@Nombre, @ApellidoPaterno, @ApellidoMaterno, @Sexo, @TipoDocumento,
                                 @NumeroDocumento, @Carrera, @Ciclo, @Turno)";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", estudiante.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", estudiante.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", estudiante.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Sexo", estudiante.Sexo);
                    cmd.Parameters.AddWithValue("@TipoDocumento", estudiante.TipoDocumento);
                    cmd.Parameters.AddWithValue("@NumeroDocumento", estudiante.NumeroDocumento);
                    cmd.Parameters.AddWithValue("@Carrera", estudiante.Carrera);
                    cmd.Parameters.AddWithValue("@Ciclo", estudiante.Ciclo);
                    cmd.Parameters.AddWithValue("@Turno", estudiante.Turno);

                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Inicio");
        }

        // GET: Estudiantes/Editar/5
        public ActionResult Editar(int id)
        {
            EstudiantesDtos estudiante = null;

            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();
                string sql = "SELECT * FROM TBEstudiantes WHERE IdEstudiante = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            estudiante = new EstudiantesDtos
                            {
                                IdEstudiante = Convert.ToInt32(dr["IdEstudiante"]),
                                Nombre = dr["Nombre"].ToString(),
                                ApellidoPaterno = dr["ApellidoPaterno"].ToString(),
                                ApellidoMaterno = dr["ApellidoMaterno"].ToString(),
                                Sexo = dr["Sexo"].ToString(),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                Carrera = dr["Carrera"].ToString(),
                                Ciclo = dr["Ciclo"].ToString(),
                                Turno = dr["Turno"].ToString()
                            };
                        }
                    }
                }
            }

            if (estudiante == null)
                return HttpNotFound();

            return View(estudiante);
        }

        // POST: Estudiantes/Editar
        [HttpPost]
        public ActionResult Editar(EstudiantesDtos estudiante)
        {
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();
                string sql = @"UPDATE TBEstudiantes SET
                                Nombre = @Nombre,
                                ApellidoPaterno = @ApellidoPaterno,
                                ApellidoMaterno = @ApellidoMaterno,
                                Sexo = @Sexo,
                                TipoDocumento = @TipoDocumento,
                                NumeroDocumento = @NumeroDocumento,
                                Carrera = @Carrera,
                                Ciclo = @Ciclo,
                                Turno = @Turno
                               WHERE IdEstudiante = @IdEstudiante";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", estudiante.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", estudiante.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", estudiante.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Sexo", estudiante.Sexo);
                    cmd.Parameters.AddWithValue("@TipoDocumento", estudiante.TipoDocumento);
                    cmd.Parameters.AddWithValue("@NumeroDocumento", estudiante.NumeroDocumento);
                    cmd.Parameters.AddWithValue("@Carrera", estudiante.Carrera);
                    cmd.Parameters.AddWithValue("@Ciclo", estudiante.Ciclo);
                    cmd.Parameters.AddWithValue("@Turno", estudiante.Turno);
                    cmd.Parameters.AddWithValue("@IdEstudiante", estudiante.IdEstudiante);

                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Inicio");
        }

        // GET: Estudiantes/Eliminar/5
        public ActionResult Eliminar(int id)
        {
            EstudiantesDtos estudiante = null;

            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();
                string sql = "SELECT * FROM TBEstudiantes WHERE IdEstudiante = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            estudiante = new EstudiantesDtos
                            {
                                IdEstudiante = Convert.ToInt32(dr["IdEstudiante"]),
                                Nombre = dr["Nombre"].ToString(),
                                ApellidoPaterno = dr["ApellidoPaterno"].ToString(),
                                ApellidoMaterno = dr["ApellidoMaterno"].ToString(),
                                Sexo = dr["Sexo"].ToString(),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                Carrera = dr["Carrera"].ToString(),
                                Ciclo = dr["Ciclo"].ToString(),
                                Turno = dr["Turno"].ToString()
                            };
                        }
                    }
                }
            }

            if (estudiante == null)
                return HttpNotFound();

            return View(estudiante);
        }

        // POST: Estudiantes/Eliminar
        // GET: Estudiantes/Eliminar/5
        public ActionResult Eliminar(int? id)
        {
            if (id == null)
                return RedirectToAction("Inicio");

            EstudiantesDtos estudiante = null;

            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();
                string sql = "SELECT * FROM TBEstudiantes WHERE IdEstudiante = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Id", id.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            estudiante = new EstudiantesDtos
                            {
                                IdEstudiante = Convert.ToInt32(dr["IdEstudiante"]),
                                Nombre = dr["Nombre"].ToString(),
                                ApellidoPaterno = dr["ApellidoPaterno"].ToString(),
                                ApellidoMaterno = dr["ApellidoMaterno"].ToString(),
                                Sexo = dr["Sexo"].ToString(),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                Carrera = dr["Carrera"].ToString(),
                                Ciclo = dr["Ciclo"].ToString(),
                                Turno = dr["Turno"].ToString()
                            };
                        }
                    }
                }
            }

            if (estudiante == null)
                return HttpNotFound();

            return View(estudiante);
        }
    }
}