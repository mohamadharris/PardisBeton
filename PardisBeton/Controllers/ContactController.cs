using Microsoft.AspNetCore.Mvc;
using Model.ViewModels.Contact;
using System.Net.Mail;

namespace PardisBeton.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(ContactFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Configure the email
                    var mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(model.Email);
                    mailMessage.To.Add("info@pardisbeton.com");
                    mailMessage.Subject = $"پیام فرم تماس از {model.Name}";
                    mailMessage.Body = model.Message;
                    mailMessage.IsBodyHtml = false;

                    // Send the email 
                    using (var smtpClient = new SmtpClient(""))
                    {
                        smtpClient.Credentials = new System.Net.NetworkCredential("","");
                        smtpClient.Port = 25;
                        smtpClient.EnableSsl = true;
                        smtpClient.Send(mailMessage);
                    }

                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, error = "خطایی در ارسال پیام رخ داده است. لطفا دوباره تلاش کنید." });
                }
            }

            return Json(new { success = false, error = "لطفا همه فیلدها را به درستی پر کنید." });
        }
    }
}
