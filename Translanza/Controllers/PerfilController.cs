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
                ViewBag.Tercero = db.Tercero.Where(f => f.RowID == usuarioLogeado.TerceroID).FirstOrDefault();
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

            Tercero obj_Tercero = db.Tercero.Where(f => f.RowID == rowid).FirstOrDefault();

            obj_Tercero.Identificacion = identificacion;
            obj_Tercero.Nombre = nombres;
            obj_Tercero.Apellido = apellidos;
            obj_Tercero.Telefono = telefono;
            obj_Tercero.Celular = celular;
            obj_Tercero.Correo = correo;
            obj_Tercero.FechaActualizacion = DateTime.Now;
            obj_Tercero.UsuarioActualizacion = usuarioLogeo.NombreUsuario;

            db.SaveChanges();

            return Json(obj_Tercero.RowID.ToString());
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
            ViewBag.listaTercero = db.Tercero.Where(f => f.Activo == true);
            //ViewBag.listaAfiliado = db.Afiliado.Where(f => f.Activo == true);
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
            
            Usuario obj = new Usuario();

            if (rowid != 0)
            {
                obj = db.Usuario.Where(f => f.RowID == rowid).FirstOrDefault();
            }

            obj.NombreUsuario = Request.Params["nombreusuario"];
            obj.TerceroID = int.Parse(Request.Params["tercero"]);
            obj.RolID = int.Parse(Request.Params["rol"]);

            if (Request.Params["contraseña"] != "")
                obj.Contraseña = Request.Params["contraseña"];

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
