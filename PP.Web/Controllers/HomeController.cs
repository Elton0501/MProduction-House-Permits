using PP.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace PP.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Permit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Permit(PermitModel model)
        {
            var result = "* All fields are required to fill.";
            if (!ModelState.IsValid)
            {
                string message = "<p>Thank you</p><p>We have recieved your request</p><p>Out team is reviewing you request and will get back to you a.s.a.p.</p>";
                string subject = "M Production: Request for permit received.";
                string Head = "Request for permit received";
                templateEmail(model.Email, subject, Head, message);

                string message1 = string.Format("Name: {0} <br> Email: {1} <br> Company Name: {2}" +
                    "<br> Phone Number: {3} <br>WhatsApp Number: {4} <br>Filmpermit services: {5}<br>Location hunting: {6}<br>General Permit: {7}<br>RTA Permit: {8}<br>additionalservices: {9}<br>Do You Want Catering: {10}<br>No. Of Crew For Catering: {11}" +
                    "<br>Assignment: {12}<br>Filming date: {13}<br>Filming City: {14}<br>Days: {15}<br>Crew: {16}<br>Do You Want Hotel Booking: {17}<br>shoot: {18}<br>remarks: {19}",
                                      model.Name, model.Email, model.CompanyName, model.PhoneNumber, model.WhatsAppNumber,
                                      model.Filmpermitservices, model.locationhunting == true ? "Yes" : "No", model.GeneralPermit == true ? "Yes" : "No", model.RTAPermit == true ? "Yes" : "No", model.additionalservices, model.Catering == true ? "Yes" : "No", model.CateringCrew.HasValue ? model.CateringCrew : 0, model.Assignment,
                                      model.Filmingdate, model.FILMINGCITY, model.days, model.crew, model.HotelBooking == true ? "Yes" : "No", model.shoot, model.remarks);

                string subject1 = "Permit Request by" + model.Name;
                string Head1 = "Permit Request";
                templateEmail(ConfigurationManager.AppSettings["email"].ToString(), subject1, Head1, message1);
                result = "true";
            }
            return Json(result);
        }
        public bool templateEmail(string email, string subject, string head, string body)
        {
            bool result = false;
            try
            {
                var mail = ConfigurationManager.AppSettings["email"];
                var pass = ConfigurationManager.AppSettings["pass"];
                var port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
                var stp = ConfigurationManager.AppSettings["smtp"];
                var ssl = Convert.ToBoolean(ConfigurationManager.AppSettings["ssl"]);

                string FilePath = HostingEnvironment.MapPath("~/EmailTemplate/") + "mailtemp" + ".html";
                StreamReader reader = new StreamReader(FilePath);
                string textMail = reader.ReadToEnd();
                reader.Close();

                textMail = textMail.Replace("[head]", head.Trim());
                textMail = textMail.Replace("[content]", body.Trim());

                //textMail = textMail.Replace("[phone]", phone.Trim());
                //textMail = textMail.Replace("[contact]", phone.Trim());
                //textMail = textMail.Replace("[address]", address.Trim());

                MailMessage message = new MailMessage();
                message.From = new MailAddress(mail);
                message.To.Add(email);
                message.Subject = subject;
                message.Body = textMail;

                message.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient(stp, port);
                smtp.Credentials = new NetworkCredential(mail, pass);
                smtp.EnableSsl = ssl;
                smtp.Send(message);
                smtp.Dispose();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}