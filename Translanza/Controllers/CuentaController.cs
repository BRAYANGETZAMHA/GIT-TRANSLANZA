using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Translanza.Models;

namespace Translanza.Controllers
{
    public class CuentaController : Controller
    {
        TranslanzaEntities db = new TranslanzaEntities();

        public ActionResult limpiarSession()
        {
            Session.Clear();
            Session.RemoveAll();
            Session["Usuario"] = null;
            return RedirectToAction("Index", "Inicio");
        }
    }
}