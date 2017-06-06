using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Translanza.Models;

namespace Translanza.Controllers
{
    public class MaestroController : Controller
    {
        TranslanzaEntities db = new TranslanzaEntities();

        [CheckSessionOutAttribute]
        public ActionResult VistaTipo()
        {
            Session["Modulo"] = "Maestro";
            Session["SubModulo"] = "Vista Tipo";

            ViewBag.ListaTipo = db.Tipo.Where(f => f.Activo == true);

            return View();
        }

        [CheckSessionOutAttribute]
        public ActionResult Tipo(int? rowid)
        {
            Session["Modulo"] = "Maestro";
            Session["SubModulo"] = "Tipo";

            ViewBag.listaAgrupacion = db.Agrupacion.Where(f => f.Activo == true);

            Tipo obj = new Tipo();
            if (rowid != 0 && rowid != null)
                obj = db.Tipo.Where(f => f.RowID == rowid).FirstOrDefault();

            return View(obj);
        }

        [HttpPost]
        public JsonResult GuardarTipo()
        {
            Usuario usuarioLogeo = ((Usuario)Session["Usuario"]);

            int rowid = int.Parse(Request.Params["rowid"]);
            int agrupacion = int.Parse(Request.Params["agrupacion"]);
            string codigo = Request.Params["codigo"];
            string nombre = Request.Params["nombre"];
            string descripcion = Request.Params["descripcion"];
            string valor = Request.Params["valor"];
            int orden = int.Parse(Request.Params["orden"]);

            Tipo obj = new Tipo();

            if (rowid != 0)
            {
                obj = db.Tipo.Where(f => f.RowID == rowid).FirstOrDefault();
            }

            obj.AgrupacionID = agrupacion;
            obj.Codigo = codigo;
            obj.Nombre = nombre;
            obj.Descripcion = descripcion;
            obj.Valor = valor;
            obj.Orden = orden;

            if (obj.RowID == 0)
            {
                obj.Activo = true;
                obj.FechaCreacion = DateTime.Now;
                obj.UsuarioCreacion = usuarioLogeo.NombreUsuario;
                db.Tipo.Add(obj);
            }
            else
            {
                obj.FechaActualizacion = DateTime.Now;
                obj.UsuarioActualizacion = usuarioLogeo.NombreUsuario;
            }
            db.SaveChanges();

            return Json(obj.RowID.ToString());
        }

    }

}