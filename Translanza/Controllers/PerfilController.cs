using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Translanza.Models;

namespace Translanza.Controllers
{
    public class PerfilController : Controller
    {
        TranslanzaEntities db = new TranslanzaEntities();

        #region mi perfil
        [CheckSessionOutAttribute]
        public ActionResult MiPerfil()
        {
            Session["Modulo"] = "Configuración";
            Session["SubModulo"] = "Mi Perfil";
            Usuario usuarioLogeado = ((Usuario)Session["Usuario"]);

            if (usuarioLogeado != null)
            {
                ViewBag.Usuario = usuarioLogeado;
                ViewBag.Empleado = db.Empleado.Where(f => f.RowID == usuarioLogeado.EmpleadoID).FirstOrDefault();
            }

            return View();
        }

        [HttpPost]
        public JsonResult GuardarPerfil()
        {
            Usuario usuarioLogeo = ((Usuario)Session["Usuario"]);

            int rowid = int.Parse(Request.Params["rowid"]);
            int identificacion = int.Parse(Request.Params["identificacion"]);
            string nombres = Request.Params["nombres"];
            string apellidos = Request.Params["apellidos"];
            string telefono = Request.Params["telefono"];
            string celular = Request.Params["celular"];
            string correo = Request.Params["correo"];

            Empleado obj_Empleado = db.Empleado.Where(f => f.RowID == rowid).FirstOrDefault();

            obj_Empleado.Identificacion = identificacion;
            obj_Empleado.Nombre = nombres;
            obj_Empleado.Apellido = apellidos;
            obj_Empleado.Telefono = telefono;
            obj_Empleado.Celular = celular;
            obj_Empleado.Correo = correo;
            obj_Empleado.FechaActualizacion = DateTime.Now;
            obj_Empleado.UsuarioActualizacion = usuarioLogeo.NombreUsuario;

            db.SaveChanges();

            return Json(obj_Empleado.RowID.ToString());
        }

        [HttpPost]
        public JsonResult GuardarContraseña()
        {
            Usuario usuarioLogeo = ((Usuario)Session["Usuario"]);

            int rowid = int.Parse(Request.Params["rowid"]);
            string nombres = Request.Params["contraseña"];

            Usuario obj_Usuario = db.Usuario.Where(f => f.RowID == rowid).FirstOrDefault();

            obj_Usuario.Contraseña = nombres;
            obj_Usuario.FechaActualizacion = DateTime.Now;
            obj_Usuario.UsuarioActualizacion = usuarioLogeo.NombreUsuario;

            db.SaveChanges();

            return Json(obj_Usuario.RowID.ToString());
        }
        #endregion

        #region Usuarios

        [CheckSessionOutAttribute]
        public ActionResult VistaUsuario()
        {
            Session["Modulo"] = "Configuración";
            Session["SubModulo"] = "Vista Usuario";
            ViewBag.ListaUsuario = db.Usuario.Where(f => f.Activo == true);

            return View();
        }

        [CheckSessionOutAttribute]
        public ActionResult Usuario(int? rowid)
        {
            Session["Modulo"] = "Configuración";
            Session["SubModulo"] = "Usuario";
            ViewBag.listaEmpleado = db.Empleado.Where(f => f.Activo == true);
            ViewBag.listaAfiliado = db.Afiliado.Where(f => f.Activo == true);
            ViewBag.listaRol = db.Rol.Where(f => f.Activo == true);

            Usuario obj = new Usuario();
            if (rowid != 0 && rowid != null)
                obj = db.Usuario.Where(f => f.RowID == rowid).FirstOrDefault();

            return View(obj);
        }

        [HttpPost]
        public JsonResult GuardarUsuario()
        {
            Usuario usuarioLogeo = ((Usuario)Session["Usuario"]);

            int rowid = int.Parse(Request.Params["rowid"]);
            string nombreusuario = Request.Params["nombreusuario"];
            string contraseña = Request.Params["contraseña"];
            int empleado = 0;
            int afiliado = 0;

            string tipo = Request.Params["tipo"];

            if (tipo == "empleado")
            {
                empleado = int.Parse(Request.Params["empleado"]);
            }
            else
            {
                afiliado = int.Parse(Request.Params["afiliado"]);
            }

            int rol = int.Parse(Request.Params["rol"]);

            Usuario obj = new Usuario();

            if (rowid != 0)
            {
                obj = db.Usuario.Where(f => f.RowID == rowid).FirstOrDefault();
            }

            obj.NombreUsuario = nombreusuario;
            obj.Contraseña = contraseña;

            if (empleado > 0)
            {
                obj.EmpleadoID = empleado;
            }
            else
            {
                obj.AfiliadoID = afiliado;
            }

            obj.RolID = rol;

            if (contraseña != "")
                obj.Contraseña = contraseña;

            if (obj.RowID == 0)
            {
                obj.Activo = true;
                obj.FechaCreacion = DateTime.Now;
                obj.UsuarioCreacion = usuarioLogeo.NombreUsuario;
                db.Usuario.Add(obj);
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

    }
}
