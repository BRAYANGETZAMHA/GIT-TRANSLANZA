using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Translanza.Models;
using System.Net.Mail;
using System.Net;

namespace Translanza.Controllers
{
    public class InicioController : Controller
    {
        TranslanzaEntities db = new TranslanzaEntities();


        public ActionResult Index()
        {
            return View();
        }

        [CheckSessionOutAttribute]
        public ActionResult Grafica()
        {
            return View();
        }


        public JsonResult InicioSesion(string usuario, string contraseña)
        {
            if (ValidateLogin(usuario, contraseña))
            {
                return Json(new { resultado = true, ruta = "../Inicio/Grafica" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { resultado = false, ruta = "" });
            }
        }


        public void enviarCorreo()
        {
            MailMessage email = new MailMessage();
            email.To.Add(new MailAddress("sistemas@translanza.com"));
            email.From = new MailAddress("Contactenos Pagina Web");
            email.Subject = "";
            email.Body = "";
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.translanza.com";
            smtp.Port = 25;
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("sistemas@translanza.com", "translanza123");

            Utilidad.SendEmail(email, smtp);
        }

        private bool ValidateLogin(string username, string passwd)
        {
            Usuario usuarioLogeado = null;

            try
            {
                //List<Usuario> d = db.Usuario.ToList();
                usuarioLogeado = db.Usuario.FirstOrDefault(f => f.NombreUsuario == username && f.Contraseña == passwd);
            }
            catch (Exception e)
            {
                return false;
            }


            List<Tipo> tipoMenu = new List<Tipo>();
            List<Menu> menuUsuario = new List<Menu>();

            if (usuarioLogeado != null)
            {
                if (usuarioLogeado.Rol.Nombre != "administrador")
                {
                    List<RolMenu> menuxRol = db.RolMenu.Where(f => f.Rol.RowID == usuarioLogeado.Rol.RowID && f.Menu.Activo == true && f.Activo == true).ToList();//agregar condicion "activo" para rolmenu

                    foreach (var item in menuxRol)
                    {
                        menuUsuario.Add(item.Menu);

                        if (tipoMenu.Where(f => f.RowID == item.Menu.Tipo.RowID).Count() == 0)
                        {
                            tipoMenu.Add(item.Menu.Tipo);
                        }
                    }
                }
                else
                {
                    tipoMenu = db.Tipo.Where(f => f.Agrupacion.Nombre == "TipoMenu").ToList();
                    menuUsuario = db.Menu.Where(f => f.Activo == true).ToList();
                }

                Session["Usuario"] = usuarioLogeado;
                Session["ListaModulo"] = tipoMenu;
                Session["ListaMenu"] = menuUsuario;
                Session["Modulo"] = "";
                Session["SubModulo"] = "";
                Session.Timeout = 15;

                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
