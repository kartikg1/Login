using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Description;
using Resume_Builder.Models;

namespace Resume_Builder.Controllers
{
    public class ContactsController : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        [Route("api/Contact")]
        public string ContactUs(Contact contact)
        {
            System.Diagnostics.Debug.WriteLine(contact.Bname);
            System.Diagnostics.Debug.WriteLine(contact.Bemail);
            System.Diagnostics.Debug.WriteLine(contact.Bsubject);
            System.Diagnostics.Debug.WriteLine(contact.Bmobile);
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("gupta.kartik3@gmail.com");
                mailMessage.To.Add(contact.Bemail);
                mailMessage.Subject = contact.Bname + " - " + contact.Bsubject;
                mailMessage.Body = "Hi " + contact.Bname + ",\n\n" + contact.Bmessage + "\n\nRegards,\nKartik Gupta";
                mailMessage.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp.gmail.com";
                NetworkCredential credentials = new NetworkCredential("gupta.kartik3@gmail.com", "qnvkafqktlryqyda");
                smtpClient.Credentials = credentials;
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
                return "OTP Sent Successfully...!!!";
            }
            catch (Exception)
            {
                return "Sending OTP Failed";
            }
        }
    }
}