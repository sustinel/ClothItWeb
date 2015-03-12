using ClothItWeb.Models;
using Mandrill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ClothItWeb
{
    public class MailHelper
    {

       private static  MandrillApi api = new MandrillApi("-EBXWnItZLoYKd9FCttjEQ");
        public static async Task<String> EnviarContactoAdmin(Contacto contacto)
        {

            var task = api.UserInfoAsync();


            EmailMessage message = new EmailMessage();

            message.subject = "Se ha realizado un nuevo contacto";
            string body = GetContactoTemplate();
            body = body.Replace("{{EMAIL}}", contacto.Email);
            body = body.Replace("{{MENSAJE}}", contacto.Mensaje);
            message.html = body;
            EmailAddress address = new EmailAddress("contacto.clothit@gmail.com");
            List<EmailAddress> listaEmail = new List<EmailAddress>();
            listaEmail.Add(address);
            message.to = listaEmail;
            message.from_name = "ClothIt";

            await task.ContinueWith(data =>
            {
                var userInfo = data.Result;
                message.from_email = userInfo.username;

            });


            List<EmailResult> resultados = await api.SendMessageAsync(message);
            return "Mensaje Enviado";

        }

        public static async Task<String> EnviarContactoUser(Contacto contacto)
        {

            var task = api.UserInfoAsync();


            EmailMessage message = new EmailMessage();

            message.subject = "Thanks for your message !";
            string body = GetContactoUserTemplate();
            body = body.Replace("{{NAME}}", contacto.Nombre);
            message.html = body;
            EmailAddress address = new EmailAddress(contacto.Email);
            List<EmailAddress> listaEmail = new List<EmailAddress>();
            listaEmail.Add(address);
            message.to = listaEmail;
            message.from_name = "ClothIt";

            await task.ContinueWith(data =>
            {
                var userInfo = data.Result;
                message.from_email = userInfo.username;

            });


            List<EmailResult> resultados = await api.SendMessageAsync(message);
            return "Mensaje Enviado";

        }

        public static async Task<String> EnviarNewsletterUser(Newsletter contacto)
        {

            var task = api.UserInfoAsync();


            EmailMessage message = new EmailMessage();

            message.subject = "You've been subscribed to ClothIt newsletter";
            string body = GetNewsletterConfirmTemplate();
            body = body.Replace("{{EMAIL}}", contacto.Email);
            message.html = body;
            EmailAddress address = new EmailAddress(contacto.Email);
            List<EmailAddress> listaEmail = new List<EmailAddress>();
            listaEmail.Add(address);
            message.to = listaEmail;
            message.from_name = "ClothIt";

            await task.ContinueWith(data =>
            {
                var userInfo = data.Result;
                message.from_email = userInfo.username;

            });


            List<EmailResult> resultados = await api.SendMessageAsync(message);
            return "Mensaje Enviado";

        }


        private static string GetContactoTemplate()
        {
            String template = "Se ha recibido el siguiente mensaje de {{EMAIL}} \n {{MENSAJE}}";

            return template;
        }

        private static string GetContactoUserTemplate()
        {
            String template = "Dear {{EMAIL}}, thanks for sending us your message \n The support team will contact you soon !";

            return template;
        }

        private static string GetNewsletterConfirmTemplate()
        {
            String template = "Dear {{EMAIL}}, thanks for subscribing to our Newsletter \n You will recieve news about ClothIt very soon  !!!";

            return template;
        }

    }
}