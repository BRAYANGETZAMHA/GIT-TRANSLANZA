using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Translanza.Models;

namespace Translanza.Controllers
{
    public class CheckSessionOutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();

            HttpSessionStateBase session = filterContext.HttpContext.Session;

            //if (((Usuario)session["Usuario"]) == null)
            if(session.IsNewSession == true)
            {
                //send them off to the login page
                var url = new UrlHelper(filterContext.RequestContext);
                var loginUrl = url.Content("~/Inicio/Index");
                
                filterContext.HttpContext.Response.Redirect(loginUrl, true);
            }
        }
    }
}