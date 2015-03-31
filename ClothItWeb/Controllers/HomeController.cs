using ClothItWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using ClothItWeb.Helpers;

namespace ClothItWeb.Controllers
{
    public class HomeController : BaseController
    {
        private ClothItEntities db = new ClothItEntities();

        // Cachea 1 semana en el cliente
        [OutputCache(Duration = 604800, Location = OutputCacheLocation.Client)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ContactUs(Contacto contacto)
        {
            contacto.FechaAlta = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Contactoes.Add(contacto);
                var result = await db.SaveChangesAsync();

                //ENVIO MAIL AL USUARIO Y AL ADMINISTRADOR
               var res = await MailHelper.EnviarContactoAdmin(contacto);
               var res2 = await MailHelper.EnviarContactoUser(contacto);

            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<ActionResult> Subscribe(Newsletter contacto)
        {
            contacto.FechaAlta = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Newsletters.Add(contacto);
                var result = await db.SaveChangesAsync();

                //ENVIO MAIL AL USUARIO Y AL ADMINISTRADOR
                var res = await MailHelper.EnviarNewsletterUser(contacto);
       

            }

            return RedirectToAction("Index");
        }
    }
}