using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Translanza.Models;

namespace Translanza.Controllers
{
    public class OperacionController : Controller
    {
        TranslanzaEntities db = new TranslanzaEntities();

        #region Empleado

        [CheckSessionOutAttribute]
        public ActionResult VistaEmpleado()
        {
            Session["Modulo"] = "Operación";
            Session["SubModulo"] = "Vista Empleado";

            ViewBag.Lista = db.Empleado.Where(f => f.Activo == true);

            return View();
        }

        [CheckSessionOutAttribute]
        public ActionResult Empleado(int? rowid)
        {
            Session["Modulo"] = "Operación";
            Session["SubModulo"] = "Empleado";

            Empleado obj = new Empleado();
            if (rowid != 0 && rowid != null)
                obj = db.Empleado.Where(f => f.RowID == rowid).FirstOrDefault();

            return View(obj);
        }

        [HttpPost]
        public JsonResult GuardarEmpleado()
        {
            Usuario usuarioLogeo = ((Usuario)Session["Usuario"]);

            int rowid = int.Parse(Request.Params["rowid"]);
            string nombres = Request.Params["nombres"];
            string apellidos = Request.Params["apellidos"];
            string identificacion = Request.Params["identificacion"];
            string telefono = Request.Params["telefono"];
            string celular = Request.Params["celular"];
            string correo = Request.Params["correo"];


            Empleado obj = new Empleado();

            if (rowid != 0)
            {
                obj = db.Empleado.Where(f => f.RowID == rowid).FirstOrDefault();
            }

            obj.Identificacion = int.Parse(identificacion);
            obj.Nombre = nombres;
            obj.Apellido = apellidos;
            obj.Telefono = telefono;
            obj.Celular = celular;
            obj.Correo = correo;

            if (obj.RowID == 0)
            {
                obj.Activo = true;
                obj.FechaCreacion = DateTime.Now;
                obj.UsuarioCreacion = usuarioLogeo.NombreUsuario;
                db.Empleado.Add(obj);
            }
            else
            {
                obj.FechaActualizacion = DateTime.Now;
                obj.UsuarioActualizacion = usuarioLogeo.NombreUsuario;
            }
            db.SaveChanges();

            return Json(obj.RowID.ToString());
        }



        #endregion

        #region Afiliado

        [CheckSessionOutAttribute]
        public ActionResult VistaAfiliado()
        {
            Session["Modulo"] = "Operación";
            Session["SubModulo"] = "Vista Afiliado";

            ViewBag.Lista = db.Afiliado;

            return View();
        }

        [CheckSessionOutAttribute]
        public ActionResult Afiliado(int? rowid)
        {
            Session["Modulo"] = "Operación";
            Session["SubModulo"] = "Afiliado";

            Afiliado obj = new Afiliado();
            if (rowid != 0 && rowid != null)
                obj = db.Afiliado.Where(f => f.RowID == rowid).FirstOrDefault();

            return View(obj);
        }

        [HttpPost]
        public JsonResult GuardarAfiliado()
        {
            Usuario usuarioLogeo = ((Usuario)Session["Usuario"]);

            int rowid = int.Parse(Request.Params["rowid"]);

            HttpPostedFileBase docidentificacion = Request.Files["input_Identificacion"];
            string rutaIdentificacion = Request.Params["archivoRutaIdentificacion"];

            Afiliado obj = new Afiliado();

            if (rowid != 0)
            {
                obj = db.Afiliado.Where(f => f.RowID == rowid).FirstOrDefault();
            }

            obj.Identificacion = int.Parse(Request.Params["identificacion"]);
            obj.Nombre = Request.Params["nombres"];
            obj.Apellido = Request.Params["apellidos"];
            obj.Telefono = Request.Params["telefono"];
            obj.Celular = Request.Params["celular"];
            obj.Correo = Request.Params["correo"];
            obj.Direccion = Request.Params["direccion"];

            string d = Request.Params["activo"];

            if (Request.Params["activo"] == "on")
            {
                obj.Activo = true;
            }
            else
            {
                obj.Activo = false;
            }

            if (obj.RowID == 0)
            {
                obj.Img_DocIdentidad = GuardarImagen(docidentificacion, "Identificacion");
                obj.Activo = true;
                obj.FechaCreacion = DateTime.Now;
                obj.UsuarioCreacion = usuarioLogeo.NombreUsuario;
                db.Afiliado.Add(obj);
            }
            else
            {
                if (!string.IsNullOrEmpty(rutaIdentificacion))
                {
                    EliminarImagen(obj.Img_DocIdentidad, "Identificacion");

                    obj.Img_DocIdentidad = GuardarImagen(docidentificacion, "Identificacion");
                }

                obj.FechaActualizacion = DateTime.Now;
                obj.UsuarioActualizacion = usuarioLogeo.NombreUsuario;
            }

            try
            {
                //db.Empleado.ToList();

                db.SaveChanges();
            }
            catch (Exception e)
            {

            }

            return Json(obj.RowID.ToString());
        }

        #endregion

        #region Conductor

        [CheckSessionOutAttribute]
        public ActionResult VistaConductor()
        {
            Session["Modulo"] = "Operación";
            Session["SubModulo"] = "Vista Conductor";

            ViewBag.Lista = db.Conductor;

            return View();
        }

        [CheckSessionOutAttribute]
        public ActionResult Conductor(int? rowid)
        {
            Session["Modulo"] = "Operación";
            Session["SubModulo"] = "Conductor";

            ViewBag.listaTipoEspecialidad = db.Tipo.Where(f => f.Activo == true && f.Agrupacion.Nombre == "TipoEspecialidad").OrderBy(o => o.Orden).ToList();

            Conductor obj = new Conductor();
            if (rowid != 0 && rowid != null)
            {
                obj = db.Conductor.Where(f => f.RowID == rowid).FirstOrDefault();
                ViewBag.listaEspecialidad = db.Especialidad.Where(f => f.ConductorID == obj.RowID).ToList();
                ViewBag.listaObservacion = db.ObservacionConductor.Where(f => f.ConductorID == obj.RowID).ToList();
            }

            return View(obj);
        }

        [HttpPost]
        public JsonResult GuardarConductor()
        {
            Usuario usuarioLogeo = ((Usuario)Session["Usuario"]);

            int rowid = int.Parse(Request.Params["rowid"]);
            string especialidad = Request.Params["especialidad"];
            string calificacion = Request.Params["example"];

            //Alisto el listado de especialidades
            string[] listaEspecialidadAgregar = especialidad.Split(',');

            HttpPostedFileBase docidentificacion = Request.Files["input_Identificacion"];
            HttpPostedFileBase licencia = Request.Files["input_Licencia"];
            string rutaLicencia = Request.Params["archivoRutaLicencia"];
            string rutaIdentificacion = Request.Params["archivoRutaIdentificacion"];

            Conductor obj = new Conductor();

            //Si es una actualizacion consulto el objeto para asignar nuevos valores
            if (rowid != 0)
            {
                obj = db.Conductor.Where(f => f.RowID == rowid).FirstOrDefault();
            }

            obj.Identificacion = int.Parse(Request.Params["identificacion"]);
            obj.Nombre = Request.Params["nombres"];
            obj.Apellido = Request.Params["apellidos"];
            obj.Telefono = Request.Params["telefono"];
            obj.Celular = Request.Params["celular"];
            obj.Direccion = Request.Params["direccion"];
            obj.Correo = Request.Params["correo"];

            if (Request.Params["activo"] == "on")
            {
                obj.Activo = true;
            }
            else
            {
                obj.Activo = false;
            }

            if (calificacion != null)
            {
                obj.Calificacion = int.Parse(calificacion);
            }
            else
            {
                obj.Calificacion = 0;
            }

            if (usuarioLogeo.Afiliado != null)
            {
                obj.Afiliado = usuarioLogeo.Afiliado;
            }

            if (obj.RowID == 0)
            {
                obj.Img_DocIdentidad = GuardarImagen(docidentificacion, "Identificacion");
                obj.Img_LicenciaConduccion = GuardarImagen(licencia, "Licencia");
                obj.FechaCreacion = DateTime.Now;
                obj.UsuarioCreacion = usuarioLogeo.NombreUsuario;
                db.Conductor.Add(obj);
            }
            else
            {
                if (!string.IsNullOrEmpty(rutaIdentificacion))
                {
                    EliminarImagen(obj.Img_DocIdentidad, "Identificacion");

                    obj.Img_DocIdentidad = GuardarImagen(docidentificacion, "Identificacion");
                }
                if (!string.IsNullOrEmpty(rutaLicencia))
                {
                    EliminarImagen(obj.Img_LicenciaConduccion, "Licencia");

                    obj.Img_LicenciaConduccion = GuardarImagen(licencia, "Licencia");
                }
                obj.FechaActualizacion = DateTime.Now;
                obj.UsuarioActualizacion = usuarioLogeo.NombreUsuario;

                //Eliminar especialidades asignadas al conductor para insertar las nuevas especialidades
                List<Especialidad> listaEspecialidadEliminar = db.Especialidad.Where(f => f.ConductorID == rowid).ToList();
                db.Especialidad.RemoveRange(listaEspecialidadEliminar);

            }
            db.SaveChanges();

            //Almacenamiento de especialidades
            foreach (string item in listaEspecialidadAgregar)
            {
                Especialidad obj_Especialidad = new Especialidad();

                obj_Especialidad.ConductorID = obj.RowID;
                obj_Especialidad.TipoID = int.Parse(item);
                db.Especialidad.Add(obj_Especialidad);
            }

            db.SaveChanges();

            return Json(obj.RowID.ToString());
        }

        [HttpPost]
        public JsonResult GuardarObservacionConductor()
        {
            Usuario usuarioLogeo = ((Usuario)Session["Usuario"]);

            int rowid = int.Parse(Request.Params["rowid"]);
            string descripcion = Request.Params["descripcion"];

            ObservacionConductor obj = new ObservacionConductor();

            //Si es una actualizacion consulto el objeto para asignar nuevos valores
            //if (rowid != 0)
            //{
            //    obj = db.Conductor.Where(f => f.RowID == rowid).FirstOrDefault();
            //}
            obj.ConductorID = rowid;
            obj.Descripcion = descripcion;

            //if (obj.RowID == 0)
            //{
            obj.Activo = true;
            obj.FechaCreacion = DateTime.Now;
            obj.UsuarioCreacion = usuarioLogeo.NombreUsuario;
            db.ObservacionConductor.Add(obj);
            //}
            //else
            //{
            //    obj.FechaActualizacion = DateTime.Now;
            //    obj.UsuarioActualizacion = usuarioLogeo.NombreUsuario;
            //}

            db.SaveChanges();

            return Json(rowid);
        }

        #endregion

        #region Vehiculo

        [CheckSessionOutAttribute]
        public ActionResult VistaVehiculo()
        {
            Session["Modulo"] = "Operación";
            Session["SubModulo"] = "Vista Vehiculo";

            if (((Usuario)Session["Usuario"]).Rol.Nombre == "Afiliado")
            {
                ViewBag.Lista = db.Vehiculo.Where(f => f.Afiliado == ((Usuario)Session["Usuario"]).Afiliado);
            }
            else
            {
                ViewBag.Lista = db.Vehiculo;
            }

            return View();
        }

        [CheckSessionOutAttribute]
        public ActionResult Vehiculo(int? rowid)
        {
            if (Session["Usuario"] != null)
            {
                Session["Modulo"] = "Operación";
                Session["SubModulo"] = "Vehiculo";

                Vehiculo obj = new Vehiculo();
                if (rowid != 0 && rowid != null)
                {
                    obj = db.Vehiculo.Where(f => f.RowID == rowid).FirstOrDefault();
                    ViewBag.listaObservacion = db.ObservacionVehiculo.Where(f => f.VehiculoID == obj.RowID).ToList();
                }

                ViewBag.listaPropietario = db.Afiliado.Where(f => f.Activo == true).OrderBy(o => o.Nombre).ToList();
                ViewBag.listaMarca = db.Tipo.Where(f => f.Activo == true && f.Agrupacion.Nombre == "Marca").OrderBy(o => o.Orden).ToList();
                ViewBag.listaTipoVehiculo = db.Tipo.Where(f => f.Activo == true && f.Agrupacion.Nombre == "TipoVehiculo").OrderBy(o => o.Orden).ToList();
                ViewBag.listaTipoCombustible = db.Tipo.Where(f => f.Activo == true && f.Agrupacion.Nombre == "TipoCombustible").OrderBy(o => o.Orden).ToList();

                return View(obj);
            }
            return View();
        }

        [HttpPost]
        public JsonResult GuardarVehiculo()
        {
            int rowid = int.Parse(Request.Params["rowid"]);

            HttpPostedFileBase TarjetaOperacion = Request.Files["input_TarjetaOperacion"];
            HttpPostedFileBase Tecnicomecanica = Request.Files["input_Tecnomecanica"];
            HttpPostedFileBase Soat = Request.Files["input_Soat"];
            HttpPostedFileBase SeguroCivil = Request.Files["input_SeguroCivil"];
            HttpPostedFileBase TarjetaPropiedad = Request.Files["input_TarjetaPropiedad"];
            HttpPostedFileBase Vehiculo = Request.Files["input_Vehiculo"];

            string rutaTarjetaOperacion = Request.Params["archivoRutaTarjetaOperacion"];
            string rutaTecnicomecanica = Request.Params["archivoRutaTecnomecanica"];
            string rutaSoat = Request.Params["archivoRutaSoat"];
            string rutaSeguroCivil = Request.Params["archivoRutaSeguroCivil"];
            string rutaTarjetaPropiedad = Request.Params["archivoRutaTarjetaPropiedad"];
            string rutaVehiculo = Request.Params["archivoRutaVehiculo"];

            Vehiculo obj = new Vehiculo();

            if (rowid != 0)
            {
                obj = db.Vehiculo.Where(f => f.RowID == rowid).FirstOrDefault();
            }

            if (Request.Params["activo"] == "on")
            {
                obj.Activo = true;
            }
            else
            {
                obj.Activo = false;
            }

            obj.AfiliadoID = int.Parse(Request.Params["propietario"]);
            obj.Placa = Request.Params["placa"];
            obj.Modelo = Request.Params["modelo"];
            obj.Linea = Request.Params["linea"];
            obj.NoPasajeros = int.Parse(Request.Params["nopasajeros"]);

            string sd = Request.Params["fecha_Tecnomecanica"];

            obj.Vencimiento_Tecnomecanica = DateTime.Parse(Request.Params["fecha_Tecnomecanica"]);
            obj.Vencimiento_Soat = DateTime.Parse(Request.Params["fecha_Soat"]);
            obj.Vencimiento_SeguroCivil = DateTime.Parse(Request.Params["fecha_SeguroCivil"]);
            obj.MarcaID = int.Parse(Request.Params["marca"]);
            obj.TipoID = int.Parse(Request.Params["tipoVehiculo"]);
            obj.CombustibleID = int.Parse(Request.Params["tipoCombustible"]);


            if (obj.RowID == 0)
            {
                obj.Img_TarjetaOperacion = GuardarImagen(TarjetaOperacion, "TarjetaOperacion");
                obj.Img_Tecnomecanica = GuardarImagen(Tecnicomecanica, "Tecnomecanica");
                obj.Img_Soat = GuardarImagen(Soat, "Soat");
                obj.Img_SeguroCivil = GuardarImagen(SeguroCivil, "SeguroCivil");
                obj.Img_TarjetaPropiedad = GuardarImagen(TarjetaPropiedad, "TarjetaPropiedad");
                obj.Img_Vehiculo = GuardarImagen(Vehiculo, "Vehiculo");

                obj.FechaCreacion = DateTime.Now;
                obj.UsuarioCreacion = ((Usuario)Session["Usuario"]).NombreUsuario;

                db.Vehiculo.Add(obj);
            }
            else
            {
                if (!string.IsNullOrEmpty(rutaTarjetaOperacion))
                {
                    EliminarImagen(obj.Img_TarjetaOperacion, "TarjetaOperacion");

                    obj.Img_TarjetaOperacion = GuardarImagen(TarjetaOperacion, "TarjetaOperacion");
                }
                if (!string.IsNullOrEmpty(rutaTecnicomecanica))
                {
                    EliminarImagen(obj.Img_Tecnomecanica, "Tecnomecanica");

                    obj.Img_Tecnomecanica = GuardarImagen(Tecnicomecanica, "Tecnomecanica");
                }
                if (!string.IsNullOrEmpty(rutaSoat))
                {
                    EliminarImagen(obj.Img_Soat, "Soat");

                    obj.Img_Soat = GuardarImagen(Soat, "Soat");
                }
                if (!string.IsNullOrEmpty(rutaSeguroCivil))
                {
                    EliminarImagen(obj.Img_SeguroCivil, "SeguroCivil");

                    obj.Img_SeguroCivil = GuardarImagen(SeguroCivil, "SeguroCivil");
                }
                if (!string.IsNullOrEmpty(rutaTarjetaPropiedad))
                {
                    EliminarImagen(obj.Img_TarjetaPropiedad, "TarjetaPropiedad");

                    obj.Img_TarjetaPropiedad = GuardarImagen(TarjetaPropiedad, "TarjetaPropiedad");
                }
                if (!string.IsNullOrEmpty(rutaVehiculo))
                {
                    EliminarImagen(obj.Img_Vehiculo, "Vehiculo");

                    obj.Img_Vehiculo = GuardarImagen(Vehiculo, "Vehiculo");
                }

                obj.FechaActualizacion = DateTime.Now;
                obj.UsuarioActualizacion = ((Usuario)Session["Usuario"]).NombreUsuario;
            }

            db.SaveChanges();

            return Json(obj.RowID.ToString());
        }

        [HttpPost]
        public JsonResult GuardarObservacionVehiculo()
        {
            Usuario usuarioLogeo = ((Usuario)Session["Usuario"]);

            int rowid = int.Parse(Request.Params["rowid"]);
            string descripcion = Request.Params["descripcion"];

            ObservacionVehiculo obj = new ObservacionVehiculo();

            //Si es una actualizacion consulto el objeto para asignar nuevos valores
            //if (rowid != 0)
            //{
            //    obj = db.Conductor.Where(f => f.RowID == rowid).FirstOrDefault();
            //}
            obj.VehiculoID = rowid;
            obj.Descripcion = descripcion;

            //if (obj.RowID == 0)
            //{
            obj.Activo = true;
            obj.FechaCreacion = DateTime.Now;
            obj.UsuarioCreacion = usuarioLogeo.NombreUsuario;
            db.ObservacionVehiculo.Add(obj);
            //}
            //else
            //{
            //    obj.FechaActualizacion = DateTime.Now;
            //    obj.UsuarioActualizacion = usuarioLogeo.NombreUsuario;
            //}

            db.SaveChanges();

            return Json(rowid);
        }

        #endregion

        #region Funcionalidad general

        public ActionResult modalImagen(string ruta)
        {
            ViewBag.ruta = ruta;
            return View();
        }
        public string GuardarImagen(HttpPostedFileBase archivo, String tipoImagen)
        {
            string result = "";
            if (archivo != null && archivo.ContentLength > 0)
            {
                string Guid_Img = Guid.NewGuid().ToString();
                Guid_Img = Guid_Img.Substring(1, 7);
                var nombreArchivo = Guid_Img.Trim() + "_" + Path.GetFileName(archivo.FileName);
                var path = Path.Combine(Server.MapPath("~/Adjuntos/" + tipoImagen), nombreArchivo);
                result = "/Adjuntos/" + tipoImagen + "/" + nombreArchivo;
                archivo.SaveAs(path);
            }

            return result;
        }

        public void EliminarImagen(string nombreArchivo, string tipoImagen)
        {
            string[] ruta = nombreArchivo.Split('/');

            int n = ruta.Count();

            var file = Path.Combine(HttpContext.Server.MapPath("/Adjuntos/" + tipoImagen), ruta[n - 1]);
            if (System.IO.File.Exists(file))
                System.IO.File.Delete(file);
        }
        #endregion

    }
}