using ClothItWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ClothItWeb.Controllers
{
    public class HomeController : Controller
    {
        private ClothItEntities db = new ClothItEntities();

        public ActionResult Index()
        {
            return View();
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
                var res = await MailHelper.EnviarNewsletter(contacto);
       

            }

            return RedirectToAction("Index");
        }
    }
}